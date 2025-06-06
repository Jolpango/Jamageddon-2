using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Jamageddon2.Entities.Path
{
    public class JPathComponent
    {
        public List<Vector2> Waypoints { get; private set; }
        public float PathWidth { get; set; } = 32f; // Width of the path for enemy placement
        public Vector2 SpawnPoint { get; set; } 

        public JPathComponent()
        {
            Waypoints = new List<Vector2>();
        }

        public void AddWaypoint(Vector2 position)
        {
            Waypoints.Add(position);
        }

        public Vector2 GetWaypoint(int index)
        {
            if (index < 0 || index >= Waypoints.Count)
                return Vector2.Zero;
            return Waypoints[index];
        }

        public void SetSpawnPoint(Vector2 position)
        {
            SpawnPoint = position;
        }

        public int WaypointCount => Waypoints.Count;
    }
}