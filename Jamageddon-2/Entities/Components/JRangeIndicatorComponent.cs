using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.Utilities;

public class JRangeIndicatorComponent : JComponent, IJInjectable<JGameScene>, IJInjectable<JMouseInput>,  IJInjectable<Game>
{
    public float Range { get; set; }
    public Color Color { get; set; }
    public Texture2D circleTexture;


    private JTransformComponent transformComponent;
    private JGameScene scene;
    private JMouseInput mouseInput;
    private Game game;
    public void Inject(Game service)
    {
        game = service;
    }
    public void Inject(JGameScene service)
    {
        scene = service;
    }

    public void Inject(JMouseInput service)
    {
        mouseInput = service;
    }

    public override void LoadContent()
    {
        base.LoadContent();
        transformComponent = Parent.GetComponent<JTransformComponent>();

        circleTexture = game.Content.Load<Texture2D>("Sprites/Circular");
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (transformComponent == null) return;
        Vector2 position = transformComponent.Position;
        Vector2 size = Parent.GetComponent<JColliderComponent>().Size;

        Vector2 offset = new Vector2(Range , Range);
        offset -= size*0.5f;

        spriteBatch.Draw(circleTexture,
            new Rectangle((int)position.X-(int)offset.X, (int)position.Y - (int)offset.Y,
            (int)Range*2, (int)Range*2), Color);

    }
}