using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Artemis.Core;
using Artemis.Core.Modules;
using Artermis.Modules.Spice2xHook.DataModels;
using Artermis.Modules.Spice2xHook.GameData;
using Newtonsoft.Json;
using SkiaSharp;
using Splat;

namespace Artermis.Modules.Spice2xHook;
[PluginFeature(Name = "Spice2x Light Hook")]
public class Spice2xLightHookModule : Module<Spice2xLightHookDataModel>
{
        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new();

        private TcpClient _client;
        private PluginSettings _settings;
        private ILogger _logger;
        private TimedUpdateRegistration _connectionUpdate;
        private NetworkStream _networkStream;
        
        private SpiceAPITypes.Command readCommand = new SpiceAPITypes.Command()
        {
                id = 2,
                module = "lights",
                function = "read",
                parameters = new dynamic[]
                        { }
        };
        public Spice2xLightHookModule(PluginSettings pluginSettings, ILogger logger)
        {
                _settings = pluginSettings;
                _logger = logger;
        }
        public override void ModuleActivated(bool isOverride)
        {

        }

        public override void ModuleDeactivated(bool isOverride)
        {

        }

        public override void Enable()
        {
                DataModel.MonoLights = new LightsDataModel();
                DataModel.RGBLights = new RGBDataModel();
                _connectionUpdate?.Dispose();
                _connectionUpdate = AddTimedUpdate(TimeSpan.FromSeconds(5), ConnectToSpiceTools, nameof(ConnectToSpiceTools));
        }

        private async Task ConnectToSpiceTools(double deltatime)
        {
                var ipSetting = _settings.GetSetting("SpiceAPIUrl", "127.0.0.1");
                var ipPort = _settings.GetSetting("SpiceAPIPort", 1337); 
                var ipEndPoint = new IPEndPoint(IPAddress.Parse(ipSetting.Value), ipPort.Value);
                TcpClient  client = new TcpClient();
                try
                {
                        await client.ConnectAsync(ipEndPoint);
                        _networkStream = client.GetStream();
                        await PopulateDataModel();
                        _client = client;
                        _connectionUpdate.Stop();
                        _connectionUpdate.Dispose();
                }
                catch (Exception e)
                {
                        _logger.Write($"spice2x:no connection {e.Message}", LogLevel.Info);
                        throw;
                }

        }

        private async Task PopulateDataModel()
        {
                var lights =  GetLights();
                var data = lights.data;
                for (int i = 0; i < data.Length; i++)
                {
                        if (data[i].name.EndsWith(" R"))
                        {
                         //RGB Handling
                                var name =  data[i].name.Substring(0, data[i].name.Length - 2);
                                name = name + " RGB";
                                if (!DataModel.RGBLights.TryGetDynamicChild(name, out _))
                                {
                                        var RGBLight = DataModel.RGBLights.AddDynamicChild(name, SKfromRGB(data[i].value, data[i+1].value,  data[i+2].value));
                                }
                                i += 2;
                        }
                        else
                        {
                                if (!DataModel.MonoLights.TryGetDynamicChild(data[i].name, out _))
                                {
                                        var light = DataModel.MonoLights.AddDynamicChild(data[i].name, data[i].value);
                                }
                        }
                }

                DataModel.Connected = true;
        }
        public override void Disable() 
        { 

        }

        public override void Update(double deltaTime)
        {
                if (!DataModel.Connected) return;
                if (!_client.Connected)
                {
                        DataModel.Connected = false;
                        _connectionUpdate = AddTimedUpdate(TimeSpan.FromSeconds(5), ConnectToSpiceTools, nameof(ConnectToSpiceTools));
                        return;
                }
                var lights =  GetLights();
                var data = lights.data;
                for (var i = 0; i < data.Length; i++)
                {
                        if (data[i].name.EndsWith(" R"))
                        {
                                //RGB Handling
                                var name =  data[i].name.Substring(0, data[i].name.Length - 2);
                                name = name + " RGB";

                                var RGBLight = DataModel.RGBLights.GetDynamicChild<SKColor>(name);
                                RGBLight.Value = SKfromRGB(data[i].value, data[i + 1].value, data[i + 2].value);
                                i += 2;
                        }
                        else
                        {
                                var RGBLight = DataModel.MonoLights.GetDynamicChild<float>(data[i].name);
                                RGBLight.Value = data[i].value;
                        }
                }
        }

        public void Restart()
        {
                
        }

        private SpiceAPITypes.LightReadResult GetLights()
        {
                var commandData = readCommand.GetCommandData(); 
                _networkStream.Write(commandData, 0, commandData.Length);
                var buffer = new byte[8192*2];
                int received = _networkStream.Read(buffer, 0, buffer.Length);
                var message = Encoding.UTF8.GetString(buffer, 0, received);
                var res = JsonConvert.DeserializeObject<SpiceAPITypes.LightReadResult>(message);
                return res;
        }
        private static SKColor SKfromRGB(float r, float g, float b)
        {
                return new SKColor(
                        (byte)(r * 255.0f + 0.5f),
                        (byte)(g * 255.0f + 0.5f),
                        (byte)(b * 255.0f + 0.5f)
                );
                
        }
}