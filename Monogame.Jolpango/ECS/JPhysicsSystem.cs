using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace MonoGame.Jolpango.ECS
{
    public abstract class JPhysicsSystem
    {
        protected List<JColliderComponent> colliders = new();

        public virtual void RegisterEntity(JEntity entity)
        {
            var collider = entity.GetComponent<JColliderComponent>();
            if (collider is null)
                throw new Exception("Entity must have collider component");
            if (!colliders.Contains(collider))
                colliders.Add(collider);
        }

        public virtual void UnregisterEntity(JEntity entity)
        {
            var collider = entity.GetComponent<JColliderComponent>();
            if (collider is null)
                throw new Exception("Entity must have collider component");
            colliders.Remove(collider);
        }

        public abstract void Update(GameTime gameTime);

        protected abstract bool CheckCollision(JColliderComponent a, JColliderComponent b);

        protected virtual void HandleCollision(JColliderComponent a, JColliderComponent b)
        {
            Debug.WriteLine($"Collision between [{a.Parent.Name}] and [{b.Parent.Name}]");
            a.TriggerCollision(b);
            b.TriggerCollision(a);
        }
    }
}
