using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.ECS
{
    public class JEntity
    {
        private Dictionary<Type, JComponent> components = new();
        public string Name { get; set; } = "Unnamed Entity";
        public HashSet<string> Tags { get; set; } = new HashSet<string>();
        public event Action<JEntity> OnDestroy;


        public List<JComponent> ComponentsList
        {
            get
            {
                return components.Values.ToList();
            }
        }
        public void AddComponent<T>(T component) where T : JComponent
        {
            component.Parent = this;
            components[typeof(T)] = component;
        }

        public T GetComponent<T>() where T : JComponent
        {
            if (components.TryGetValue(typeof(T), out var c))
                return (T)c;

            // Fallback: search all components to find derived matches
            foreach (var comp in components.Values)
            {
                if (comp is T tComp)
                    return tComp;
            }
            return null;
        }

        public IEnumerable<T> GetComponentsOf<T>() where T : JComponent
        {
            return components.Values.Where(c => c is T).Cast<T>();
        }

        public bool HasComponent<T>() where T : JComponent
        {
            if (components.ContainsKey(typeof(T)))
                return true;

            foreach (var comp in components.Values)
                if (comp is T)
                    return true;

            return false;
        }

        public bool TryGetComponent<T>(out T component) where T : JComponent
        {
            if (components.TryGetValue(typeof(T), out var compObj) && compObj is T comp)
            {
                component = comp;
                return true;
            }

            foreach (var c in components.Values)
            {
                if (c is T tComp)
                {
                    component = tComp;
                    return true;
                }
            }

            component = null;
            return false;
        }

        public virtual void DestroyEntity()
        {
            OnDestroy?.Invoke(this);
        }

        public virtual void LoadContent()
        {
            foreach (var comp in components.Values)
            {
                comp.LoadContent();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var c in components.Values) c.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var c in components.Values) c.Draw(spriteBatch);
        }
    }

}
