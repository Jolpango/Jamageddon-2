using System.Collections.Generic;

namespace MonoGame.Jolpango.Tiled
{
    public class ObjectLayer : BaseLayer
    {
        public string DrawOrder { get; set; }
        public List<MapObject> Objects { get; set; }
    }
}

namespace MonoGame.Jolpango.Tiled
{
    public class MapObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rotation { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}