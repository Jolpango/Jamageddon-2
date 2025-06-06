using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Jamageddon2.Entities.Towers
{
    internal class JDishWasherTower : JBaseTower
    {
        public JDishWasherTower() :
            base(
                spritePath: "Content/Animation/axe.json", //TODO change to dishwasher json 
                attackSpritePath: "Content/Animation/axe.json", //TODO change to dishwasher attack json 
                damage: 1f,
                range: 1000f,
                fireRate: 5.5f)
        {
            Name = "DishWasherTower";
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);  
        }

        // Override the FireProjectile method to implement a custom dishwasher-specific projectile behavior
       /* public override void attack()
        {
            // Implement your custom dishwasher-specific projectile behavior here
            // For example, create a new dishwasher projectile entity and add it to the game world
        }*/
    }





}

