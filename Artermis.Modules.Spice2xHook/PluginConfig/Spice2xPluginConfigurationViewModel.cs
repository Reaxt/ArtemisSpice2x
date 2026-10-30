
//this is pretty much ripped from diogotr7's work here. https://github.com/diogotr7/Artemis.Plugins/blob/3f3e56af4ca089e983d85e9903ee12555e607585/src/Artemis.Plugins.Modules.Discord/DiscordPluginConfiguration/DiscordPluginConfigurationViewModel.cs
//i rlly wish this thing had documentation man..

﻿using System;
using System.Collections.ObjectModel;
using Artemis.Core;
using Artemis.UI.Shared;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Threading.Tasks;


namespace Artermis.Modules.Spice2xHook.PluginConfig;

public class Spice2xPluginConfigurationViewModel : PluginConfigurationViewModel
{

    private readonly PluginSettings _pluginSettings;
    private readonly Plugin _plugin;
    
    public Spice2xPluginConfigurationViewModel(Plugin plugin, PluginSettings pluginSettings) : base(plugin)
    {
        _pluginSettings = pluginSettings;
        _plugin = plugin;
        
        this.WhenActivated((CompositeDisposable d) =>
        {
            Disposable.Create(() =>
            {
                Task.Run(async () =>
                {
                    _pluginSettings.SaveAllSettings();

                    var feature = _plugin.GetFeature<Spice2xLightHookModule>();
                    
                    feature?.Restart();
                });
            }).DisposeWith(d);
        });
    }
    
    public PluginSetting<int> SpiceAPIPort => _pluginSettings.GetSetting("SpiceAPIPort", 1337);
    public PluginSetting<string> SpiceAPIUrl => _pluginSettings.GetSetting("SpiceAPIUrl", "127.0.0.1");
    public PluginSetting<string> SpiceAPIPassword => _pluginSettings.GetSetting("SpiceAPIPassword", string.Empty);
}