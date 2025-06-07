using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS;
using Jamageddon2.Entities.Enemies;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;

namespace Jamageddon2.Entities.Level
{
    public class JLevelParser
    {

        public static List<JLevelConfig> LoadLevelConfigs()
        {
            // read all files in the Content/Levels folder
            var levelFiles = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Content", "Levels"), "*.json");
            var levels = new List<JLevelConfig>();
            foreach (var file in levelFiles)
            {
                string jsonContent = File.ReadAllText(file);
                var config = JsonConvert.DeserializeObject<JLevelConfig>(jsonContent);
                levels.Add(config);
            }
            return levels.OrderBy(level => level.LevelNumber).ToList();
        }

        public static JBaseEnemy CreateEnemy(string enemyType)
        {
            string namespaceName = "Jamageddon2.Entities.Enemies."; // TODO: Add to some constant
            Type type = Type.GetType($"{namespaceName}{enemyType}"); // can this throw an exception?

            if (type != null)
            {
                object instance = Activator.CreateInstance(type);
                return instance as JBaseEnemy;
            }
            else
            {
                Console.WriteLine($"Unknown enemy type: {enemyType}");
            }
            return null;
        }
    }
}