using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Warrior
{
    public class Engine
    {
        private List<Unit> listofUnits = new List<Unit>();
        private const int turns = 5;

        internal void Run()
        {
            for (int i = 0; i < turns; i++)
            {
                TakeTheInput();
                //TeamSeeding();
                SimulateBattle();
            }
        }

        private void SimulateBattle()
        {
            Random rnd = new Random();
            int firstUnitIndex = rnd.Next(0, this.listofUnits.Count - 1);
            int secondUnitIndex = rnd.Next(0, this.listofUnits.Count - 1);
            while (secondUnitIndex == firstUnitIndex)
            {
                secondUnitIndex = rnd.Next(0, this.listofUnits.Count - 1);
            }

            Unit firstUnit = this.listofUnits[firstUnitIndex];
            Unit secondUnit = this.listofUnits[secondUnitIndex];
            if (!isInRange(firstUnit, secondUnit))
            {
                Console.WriteLine("Unit {0} couldnt reach unit {1}", firstUnit.Name, secondUnit.Name);
                return;
            }
            if (firstUnit.Team == secondUnit.Team)
            {
                secondUnit.Health += 10;
                firstUnit.Mana -= 10;
            }
            else
            {
                if (firstUnit is IAttack)
                {
                    firstUnit.Mana += 100;
                    secondUnit.Health -= firstUnit.Damage;
                }
            }
            if (!HasEnoughHealth(secondUnit))
            {
                UnitDestroyed(firstUnit, secondUnit);
            }
            else
            {
                Console.WriteLine(new string('-', 60));
                Console.WriteLine(firstUnit.Name + " has an interaction with " + secondUnit.Name
                    + " with the result:\n");
            }
            Console.WriteLine(firstUnit + "\n" + secondUnit);
        }
        private void PlayersSeeding(Unit unit)
        {
            this.listofUnits.Add(unit);

            //this.listofUnits.Add(new Barbarian("Alex", Teams.Blue, 8, 4));
            //this.listofUnits.Add(new Barbarian("Pesho", Teams.Red, 4, 8));
            //this.listofUnits.Add(new Barbarian("Vasko", Teams.Blue, 3, 4));
            //this.listofUnits.Add(new Barbarian("Golem", Teams.Red, 1, 1));
        }
        private void UnitDestroyed(Unit attacker, Unit defender)
        {
            Console.WriteLine(new string('-', 60));
            Console.WriteLine(attacker.Name + " has an interaction with " + defender.Name
                + " with the result:  \nThe {0} was destroyed", defender.Name);
            listofUnits.Remove(defender);
        }

        private bool HasEnoughHealth(Unit defender)
        {
            if (defender.Health <= 0)
            {
                return false;
            }
            return true;
        }

        private bool isInRange(Unit firstCharacter, Unit secondCharacter)
        {
            bool result = Math.Sqrt(Math.Pow((secondCharacter.X - firstCharacter.X), 2) +
                Math.Pow((secondCharacter.Y - firstCharacter.Y), 2)) <= 5;

            return result;
        }

        private void TakeTheInput()
        {
            Console.WriteLine(new string('-', 60) + "\nPlease enter the player's name, team, and X and Y positions");
            string command = Console.ReadLine();
            if (command == null)
            {
                while (command == null)
                {
                    inputString(command);
                    command = Console.ReadLine();
                }
            }
            else
            {
                inputString(command);
                //Console.WriteLine(isCorrectCommand);
                command = Console.ReadLine();
            }
        }
        private void inputString(string command)
        {

            string red = "red".ToUpper();
            string blue = "blue".ToUpper();
            Regex regex = new Regex(@"(\D+?),\s*(\D+?),\s*(\D+?),\s*(\d+),\s*(\d+)");
            MatchCollection match = regex.Matches(command);
            bool isSuccess = match.Count > 0;
            //Console.WriteLine(match[0].Groups[2].Value);
            //Console.WriteLine(match[0].Groups.Count);

            if (isSuccess == true)
            {
                if (match[0].Groups[2].Value.ToString().ToLower() == "red" ||
                    match[0].Groups[2].Value.ToString().ToLower() == "blue" && match[0].Groups[3].Value.ToString().ToLower() == "mage"
            || match[0].Groups[3].Value.ToString().ToLower() == "barbarian")
                {
                    Console.WriteLine(match[0].Groups[2].Value);
                    Console.WriteLine(match[0].Groups[3].Value);
                    string name = match[0].Groups[1].Value;
                    Teams team = match[0].Groups[2].Value.ToString().ToLower() == blue ? Teams.Blue : Teams.Red;
                    int x = int.Parse(match[0].Groups[4].Value);
                    int y = int.Parse(match[0].Groups[5].Value);
                    string unit = match[0].Groups[3].Value.ToString().Substring(0, 1).ToUpper() + 
                        match[0].Groups[3].Value.ToString().Substring(1, match[0].Groups[3].Value.ToString().Length - 1);
                    Console.WriteLine(unit);
                    switch(unit)
                    {
                        case "Barbarian":
                            PlayersSeeding(new Barbarian(name, team, x, y));
                            ComputersSeeding();
                            break;
                        case "Mage":
                            PlayersSeeding(new Mage(name, team, x, y));
                            ComputersSeeding();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void ComputersSeeding()
        {
            Random rnd = new Random();
            int x = rnd.Next(0, 50);
            int y = rnd.Next(0, 50);

            foreach(var unit in listofUnits)
            {
                while(unit.X == x && unit.Y == y)
                {
                    x = rnd.Next(0, 50);
                    y = rnd.Next(0, 50);
                }
            }
            int unitIndex = rnd.Next(0, 1);
            switch(unitIndex)
            {
                case 0:
                    if(x > 25)
                    {
                        listofUnits.Add(new Barbarian("Enemy Unit " + listofUnits.Count + 1, Teams.Red, x, y));
                    }
                    else
                    {
                        listofUnits.Add(new Barbarian("Enemy Unit " + listofUnits.Count + 1, Teams.Blue, x, y));
                    }
                    break;
                case 1:
                    if (x > 25)
                    {
                        listofUnits.Add(new Mage("Enemy Unit " + listofUnits.Count + 1, Teams.Red, x, y));
                    }
                    else
                    {
                        listofUnits.Add(new Mage("Enemy Unit " + listofUnits.Count + 1, Teams.Blue, x, y));
                    }
                    break;
            }

        }

      
    }
}
