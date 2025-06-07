using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamageddon2
{
    public class Player
    {
        public int LivesLeft { get; set; } = 100;
        public int Gold { get; set; } = 100;

        public void TakeDamage(int damage)
        {
            LivesLeft -= damage;
        }

        public void AddGold(int gold)
        {
            Gold += gold;
        }
    }
}
