using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Artermis.Modules.Spice2xHook.GameData;

public class SpiceAPITypes
{
    public struct Command
    {
        public int id;
        public string module;
        public string function;
        [JsonProperty("params")]
        public dynamic[] parameters {get; set;}

        public byte[] GetCommandData()
        {
            string commandString = JsonConvert.SerializeObject(this);
            commandString = commandString + "\0";
            byte[] commandData = Encoding.UTF8.GetBytes(commandString);
            return commandData;
            
        }
    }

    public struct LightReadResult
    {
        public int id;
        public string[] errors;
        public LightResult[] data;
    }

    [JsonConverter(typeof(LightResultConverter))]
    public struct LightResult
    {
        public string name;
        public float value;
        public bool active;
    }

    private class LightResultConverter : JsonConverter<LightResult>
    {
        public override void WriteJson(JsonWriter writer, LightResult value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteValue(value.name);
            writer.WriteValue(value.value);
            writer.WriteValue(value.active);
            writer.WriteEndArray();
            
        }

        public override LightResult ReadJson(JsonReader reader, Type objectType, LightResult existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            return new LightResult()
            {
                name = array[0].ToString(),
                value = Convert.ToSingle(array[1]),
                active = Convert.ToBoolean(array[2]),
            };
        }
    }
}

