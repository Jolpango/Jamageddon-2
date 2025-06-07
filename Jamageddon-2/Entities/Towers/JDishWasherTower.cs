using Microsoft.Xna.Framework;

namespace Jamageddon2.Entities.Towers
{
    internal class JDishWasherTower : JBaseTower
    {
        public JDishWasherTower() :
            base(
                spritePath: "Content/Animation/busboy.json", //TODO change to dishwasher json 
                attackSpritePath: "Content/Animation/axe.json", //TODO change to dishwasher attack json 
                damage: 25f,
                range: 100f,
                fireRate: 0.5f)
        {
            Name = "DishWasherTower";
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);  
        }
    }
}

