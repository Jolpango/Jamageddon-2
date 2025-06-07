using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Jamageddon2.Entities.Components
{
    public class JPathComponent
    {
        public List<Vector2> Waypoints { get; set; } = new();
        public Vector2 SpawnPoint { get; set; }
    }
}