using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Tiled;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonoGame.Jolpango.ECS
{
    public class JEntityWorld
    {
        private List<JEntity> entities = new();
        private Queue<JEntity> entitiesToRemove = new();
        public JTileManager tileManager { get; private set; } = new();
        private JPhysicsSystem physicsSystem;

        public List<JEntity> Entities
        {
            get { return entities; }
        }

        public void SetPhysicsSystem(JPhysicsSystem physicsSystem)
        {
            this.physicsSystem = physicsSystem;
        }

        public void LoadMap(string mapPath)
        {
            tileManager.LoadMap(mapPath);
        }

        public void LoadContent()
        {
            foreach (JEntity entity in Entities)
            {
                entity.LoadContent();
            }
        }

        public void AddEntity(JEntity e)
        {
            entities.Add(e);
            e.OnDestroy += RemoveEntity;
            var collider = e.GetComponent<JColliderComponent>();
            if (collider is null)
                return;
            try
            {
                physicsSystem?.RegisterEntity(e);
            }
            catch
            {
                Debug.WriteLine("Unable to add entity to physics system: " + e.ToString());
            }
        }

        public void RemoveEntity(JEntity e)
        {
            e.OnDestroy -= RemoveEntity;
            entitiesToRemove.Enqueue(e);
        }

        private void ProcessRemovals()
        {
            while (entitiesToRemove.Count > 0)
            {
                var entity = entitiesToRemove.Dequeue();
                var collider = entity.GetComponent<JColliderComponent>();
                if (collider is not null && physicsSystem is not null)
                {
                    physicsSystem.UnregisterEntity(entity);
                }
                entities.Remove(entity);
            }
        }

        public void UnloadContent()
        {
        }

        public void Update(GameTime gameTime)
        {
            foreach (var e in entities) e.Update(gameTime);
            if (physicsSystem is not null)
            {
                physicsSystem.Update(gameTime);
            }
            ProcessRemovals();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            tileManager.Draw(spriteBatch);
            foreach (var e in entities) e.Draw(spriteBatch);
        }
    }
}
