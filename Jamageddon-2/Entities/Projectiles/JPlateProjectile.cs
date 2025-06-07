using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Enemies;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS.Components;
using System.Collections.Generic;

namespace Jamageddon2.Entities.Projectiles
{
    public class JPlateProjectile : JBaseProjectile
    {
        private const float PLATE_SIZE = 10f;
        private const float PLATE_DAMAGE_MULTIPLIER = 1f;
        private const string ATTACK_SPRITE_PATH = "Content/Animation/plate.json";

        public JPlateProjectile()
            : base(ATTACK_SPRITE_PATH)
        {
            Damage = Damage * PLATE_DAMAGE_MULTIPLIER;
            Size = new Vector2(PLATE_SIZE, PLATE_SIZE);
        }
    }
}