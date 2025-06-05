using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Content;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.UI.Elements;
using MonoGame.Jolpango.UI.Elements.Containers;
using MonoGame.Jolpango.Utilities;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Jamageddon2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private JGameScene scene;
        private JEntity playerEntity = new JEntity();
        private JEntity aiEntity = new JEntity();
        private JEntity aiEntityNonSolid = new JEntity();
        private JEntity destructableEntity = new JEntity();

        public JTimerHandler TimerHandler { get; set; }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            IsMouseVisible = true;
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TimerHandler = new JTimerHandler(this);
            Components.Add(TimerHandler);

            JTextureCache.Initialize(GraphicsDevice);
            scene = new JGameScene(this);

            scene.SetPhysicsSystem(new JTopDownPhysicsSystem());
            playerEntity.Name = "Player";
            playerEntity.AddComponent(new JSpriteComponent("Content/Animation/axe.json"));
            playerEntity.AddComponent(new JTransformComponent());
            playerEntity.AddComponent(new JMovementComponent() { Speed = 100f });
            playerEntity.AddComponent(new JTopDownPlayerInputComponent());
            playerEntity.AddComponent(new JColliderComponent() { Size = new Vector2(32, 32), IsSolid = true });

            aiEntity.Name = "Enemy";
            aiEntity.AddComponent(new JSpriteComponent("Content/Animation/axe.json"));
            aiEntity.AddComponent(new JTransformComponent());
            aiEntity.AddComponent(new JColliderComponent() { Size = new Vector2(32, 32), IsSolid = true });


            aiEntityNonSolid.Name = "Non solid Enemy";
            aiEntityNonSolid.AddComponent(new JSpriteComponent("Content/Animation/axe.json"));
            aiEntityNonSolid.AddComponent(new JTransformComponent());
            aiEntityNonSolid.AddComponent(new JColliderComponent() { Size = new Vector2(32, 32) });

            destructableEntity.Name = "Desctructable Entity";
            destructableEntity.AddComponent(new JSpriteComponent("Content/Animation/axe.json"));
            destructableEntity.AddComponent(new JTransformComponent());
            destructableEntity.AddComponent(new JColliderComponent() { Size = new Vector2(32, 32) });

            UIStackPanel outerPanel = new UIStackPanel()
            {
                Gap = 10,
                Position = new Vector2(300, 0),
                Orientation = Orientation.Vertical,
                Padding = new Vector2(10, 10),
            };
            for (int i = 0; i < 4; i++)
            {
                UIStackPanel innerPanel = new UIStackPanel()
                {
                    Gap = 10,
                    Orientation = Orientation.Horizontal,
                    Padding = new Vector2(10 * i, 10 * i),
                };
                for (int j = 0; j < 10; j++)
                {
                    UIButton button = new UIButton()
                    {
                        Size = new Vector2(32, 32)
                    };
                    button.OnClick += buttonclick;
                    innerPanel.AddChild(button);
                }
                outerPanel.AddChild(innerPanel);
            }
            scene.AddUIElement(outerPanel);
            scene.AddEntity(playerEntity);
            scene.AddEntity(aiEntityNonSolid);
            scene.AddEntity(destructableEntity);
            scene.LoadContent();
            scene.AddEntity(aiEntity);

            aiEntity.GetComponent<JTransformComponent>().Position = new Vector2(200, 200);
            aiEntityNonSolid.GetComponent<JTransformComponent>().Position = new Vector2(100, 200);
            destructableEntity.GetComponent<JTransformComponent>().Position = new Vector2(10, 200);
            destructableEntity.GetComponent<JColliderComponent>().OnCollision += OnCollisionDestructable;
        }

        private void OnCollisionDestructable(JColliderComponent self, JColliderComponent other)
        {
            if (other.Parent.Name == "Player")
            {
                self.Parent.DestroyEntity();
            }
        }

        public void buttonclick(UIButton button)
        {
            Debug.WriteLine("hello from button:" + button.ToString());
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            scene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(50, 50, 50));
            scene.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}