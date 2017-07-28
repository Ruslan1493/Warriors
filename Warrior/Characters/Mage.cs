using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warrior
{
    public class Mage : Unit, IAttack
    {
        private static int health = 120;
        private static int damage = 280;
        private static int mana = 150;
        private int points = 0;
        public Mage(string name, Teams team, int x, int y):base(name, health, damage, mana, team, x, y)
        {

        }
        public void IAttack(Unit enemy)
        {
            enemy.Health -= this.Damage;
            this.points += 10;
        }
    }
}
