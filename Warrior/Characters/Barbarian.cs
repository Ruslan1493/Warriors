using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warrior
{
    public class Barbarian:Unit, IAttack
    {
        private static int health = 300;
        private static int damage = 200;
        private static int mana = 50;
        private int points = 0;

        public Barbarian(string name, Teams team, int x, int y):base(name, health, damage, mana, team, x, y)
        {
        }
        
        public void IAttack(Unit enemy)
        {
            enemy.Health -= this.Damage;
            this.points += 10;
        }
    }
}
