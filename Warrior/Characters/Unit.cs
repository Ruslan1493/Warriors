using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warrior
{
    public abstract class Unit
    {
        private int health;
        private int damage;
        private int mana;
        private string name;
        private int x;
        private int y;
        private Teams team;
        public Unit(string name, int health, int damage, int mana, Teams team, int x, int y)
        {
            this.Health = health;
            this.Damage = damage;
            this.Mana = mana;
            this.X = x;
            this.Y = y;
            this.Name = name;
        }
        public Teams Team
        {
            get
            {
                return this.team;
            }
            set
            {
                if(value == null || value != Teams.Red && value != Teams.Blue)
                {
                    throw new ArgumentNullException("The input is not a team");
                }
                this.team = value;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The name was empty");
                }
                this.name = value;
            }
        }
        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                if(value > 400)
                {
                    value = 400;
                }
                else if(value < 0)
                {
                    value = 0;
                }
                this.health = value;
            }
        }
        public int Damage
        {
            get
            {
                return this.damage;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Damage cannot be less than null");
                }
                this.damage = value;
            }
        }
        public int Mana
        {
            get
            {
                return this.mana;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Mana cannot be less than null");
                }
                this.mana = value;
            }
        }
        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                if(value < 0 || value >= Console.BufferWidth)
                {
                    throw new ArgumentOutOfRangeException("The position cannot be less than 0 and more than the window width");
                }
                this.x = value;
            }
        }
        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                if (value < 0 || value >= Console.BufferHeight)
                {
                    throw new ArgumentOutOfRangeException("The position cannot be less than 0 and more than the window height");
                }
                this.y = value;
            }
        }
        public override string ToString()
        {
            return string.Join("{0}\n", string.Format("Name: {3} \nHealth: {0} \nDamage: {1} \nMana: {2} \nTeam: {4}",
                this.Health, this.Damage, this.Mana, this.Name, this.Team));
        }
    }
}
