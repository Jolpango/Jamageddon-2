using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI;
using MonoGame.Jolpango.UI.Elements;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonoGame.Jolpango.ECS
{
    public class JGameScene
    {
        private UIManager uiManager;
        private Queue<JEntity> entitiesToRemove = new();
        private Queue<JEntity> entitiesToAdd = new();

        protected JEntityWorld entityWorld;
        protected JServiceInjector serviceInjector;
        protected JKeyboardInput keyboardInput;
        protected JMouseInput mouseInput;
        protected Game game;
        public bool IsLoaded { get; private set; } = false;
        public bool IsInjected { get; private set; } = false;
        public JGameScene(Game game, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null)
        {
            entityWorld = new JEntityWorld();
            uiManager = new UIManager();
            serviceInjector = new JServiceInjector();
            this.mouseInput = mouseInput ?? new JMouseInput();
            this.keyboardInput = keyboardInput ?? new JKeyboardInput();
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            RegisterService(this.mouseInput);
            RegisterService(this.keyboardInput);
            RegisterService(this.game.Content);
            RegisterService(this);
        }
        public virtual void RegisterService<T>(T service)
        {
            serviceInjector.RegisterService(service);
        }
        public virtual void SetPhysicsSystem(JPhysicsSystem physicsSystem)
        {
            entityWorld.SetPhysicsSystem(physicsSystem);
        }
        public virtual void AddEntity(JEntity entity)
        {
            if (IsInjected)
                serviceInjector.InjectAll(entity);
            if (IsLoaded)
                entity.LoadContent();
            if(IsInjected && IsLoaded)
                entitiesToAdd.Enqueue(entity);
            else
                entityWorld.AddEntity(entity);
            
        }
        public virtual void AddUIElement(UIElement element)
        {
            uiManager.AddElement(element);
        }
        public virtual void LoadContent()
        {
            IsLoaded = true;
            InjectAllServices();
            entityWorld.LoadContent();
            uiManager.LoadContent();
        }
        public virtual void Update(GameTime gameTime)
        {
            keyboardInput.Update();
            mouseInput.Update();
            uiManager.Update(gameTime);
            entityWorld.Update(gameTime);
            ProcessAddings();
            
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            DrawEntityWorld(spriteBatch);
            DrawUI(spriteBatch);
        }
        public virtual void UnloadContent()
        {
            entityWorld.UnloadContent();
        }

        protected virtual void InjectAllServices()
        {
            IsInjected = true;
            serviceInjector.InjectAll(entityWorld.Entities);
            serviceInjector.Inject(entityWorld.tileManager);
            serviceInjector.Inject(uiManager);
        }

        private void DrawUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            uiManager.Draw(spriteBatch);
            spriteBatch.End();
        }
        private void DrawEntityWorld(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            entityWorld.Draw(spriteBatch);
            spriteBatch.End();
        }


        private void ProcessAddings()
        {
            while (entitiesToAdd.Count > 0)
            {
                entityWorld.AddEntity(entitiesToAdd.Dequeue());
            }
        }
    }
}
