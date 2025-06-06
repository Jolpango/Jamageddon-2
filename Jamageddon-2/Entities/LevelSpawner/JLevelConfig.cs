using Microsoft.Xna.Framework;
using Jamageddon2.Entities.Enemies;
using System.Collections.Generic;

namespace Jamageddon2.Entities.Level
{
    public class JEnemyCluster
    {
        public string EnemyType { get; set; }
        public float SpawnSpeed { get; set; }
        public int Amount { get; set; }
        public float SpawnDelay { get; set; }
    }

    public class JLevelConfig
    {
        public int LevelNumber { get; set; }
        public List<JEnemyCluster> EnemyClusters { get; set; }
        
    }
}