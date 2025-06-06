using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.Utilities;
using Jamageddon2.Entities.Enemies;
using Jamageddon2.Entities.Components;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;

namespace Jamageddon2.Entities.Level
{
    public class JLevelSpawner
    {
        private JLevelConfig currentLevel;
        private JPathComponent path;
        private JGameScene parentScene;
        private List<JLevelConfig> levelConfigs;
        private JTimerHandler spawnTimer;
        private JLevelParser levelParser;
        private int enemiesSpawned;
        public JLevelSpawner(Game game, JGameScene parentScene, JPathComponent path)
        {
            this.parentScene = parentScene;
            this.path = path;
            this.spawnTimer = new JTimerHandler(game);
            this.levelParser = new JLevelParser();
            LoadLevelConfigs();
        }

        public void Update(GameTime gameTime)
        {
            spawnTimer?.Update(gameTime);
        }

        private void LoadLevelConfigs()
        {
            levelConfigs = levelParser.LoadLevelConfigs();
        }

        public void StartLevel(int levelNumber)
        {
            if (levelNumber <= 0 || levelNumber > levelConfigs.Count)
            {
                throw new System.ArgumentException($"Invalid level number: {levelNumber}");
            }

            currentLevel = levelConfigs[levelNumber - 1];
            enemiesSpawned = 0;
            SpawnEnemies();
        }

        public void SpawnEnemies()
        {
            var spawnTime = 0f;
            currentLevel.EnemyClusters.ForEach(cluster => {
                spawnTime += cluster.SpawnDelay;
                for (int i = 0; i < cluster.Amount; i++)
                {
                    spawnTime += cluster.SpawnSpeed;
                    StartSpawnTimer(spawnTime, cluster.EnemyType);
                }
            });
        }

        private void StartSpawnTimer(float spawnTime, string enemyType)
        {
            spawnTimer.AddTimer(new JTimer(spawnTime, ()=> {
                SpawnEnemy(enemyType);
            }));
        }

        private void SpawnEnemy(string enemyType) 
        {
            var enemy = levelParser.CreateEnemy(enemyType);
            enemy.SetPath(path);
            parentScene.AddEntity(enemy);
            enemy.StartMovement();
            enemiesSpawned++;
        }

        public bool IsLevelComplete => enemiesSpawned >= currentLevel.EnemyClusters.Sum(cluster => cluster.Amount);
        
        public int CurrentLevel => currentLevel == null ? 0 : currentLevel.LevelNumber;
    }
}