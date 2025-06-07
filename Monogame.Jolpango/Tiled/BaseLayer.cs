using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MonoGame.Jolpango.Tiled
{
    public class BaseLayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Opacity { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class LayerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(BaseLayer);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            var type = jo["type"]?.ToString();

            BaseLayer layer;

            switch (type)
            {
                case "tilelayer": layer = new TileLayer(); break;
                case "objectgroup": layer = new ObjectLayer(); break;
                default: throw new Exception($"Unknown layer type: {type}");
            }

            serializer.Populate(jo.CreateReader(), layer);
            return layer;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject jo = JObject.FromObject(value, serializer);
            jo.WriteTo(writer);
        }
    }
}