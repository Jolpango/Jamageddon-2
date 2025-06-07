using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.Utilities;
using Jamageddon2.Entities.Components;
using System.Collections.Generic;
using System;
using Jamageddon2.Scenes;
using MonoGame.Jolpango.Core;

namespace Jamageddon2.Entities.Level
{
    public class JLevelSpawner
    {
        private JLevelConfig currentLevel;
        private JPathComponent path;
        private PlayScene scene;
        private List<JLevelConfig> levelConfigs;
        private JTimerHandler spawnTimer;
        public JLevelSpawner(Game game, PlayScene scene, JPathComponent path)
        {
            this.path = path;
            this.scene = scene;

            spawnTimer = new JTimerHandler(game);
            game.Components.Add(spawnTimer);

            levelConfigs = JLevelParser.LoadLevelConfigs();
            currentLevel = levelConfigs[0];
        }

        public void StartLevel(int levelNumber)
        {
            if (levelNumber < 0 || levelNumber >= levelConfigs.Count)
                throw new ArgumentException($"Invalid level number: {levelNumber}");

            currentLevel = levelConfigs[levelNumber];
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
                    spawnTimer.AddTimer(new JTimer(spawnTime, () =>
                    {
                        var enemy = JLevelParser.CreateEnemy(cluster.EnemyType);
                        enemy.SetPath(path);
                        scene.AddEntity(enemy);
                        enemy.StartMovement();
                    }));
                }
            });
        }

        public bool IsAllEnemiesDestroyed => scene.GetEntitiesByTag("Enemy").Count == 0;
        public bool IsLastLevel => currentLevel.LevelNumber == levelConfigs.Count;
        public int NextLevel => currentLevel.LevelNumber;
    }
}