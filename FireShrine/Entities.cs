using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireShrine
{
    public class Entities
    {
        public string Name = "Beast";
        public int HealthPoints = 10;
        public int Defense = 3;
        public int DodgeRating = 2;
        public string[] movelist = new string[] { "Attack1", "Attack2" };
        public int BaseDamage = 3;

        public bool isFlying = false;
        public bool isDodgeing = false;
        public bool isBulwarked = false;
        public bool isOverwhelming = false;
    }
    public class AttackList
    {
        public static void Bulwark(Entities Enemy)
        {
            Console.WriteLine($"{Enemy.Name} Uses Bulwark!");
            Story.Continue(0);
            Enemy.isBulwarked = true;
            Enemy.Defense = Enemy.Defense + 3;
            Console.WriteLine($"{Enemy.Name} increases its defense!");
        }
        public static void Bite(Entities Enemy)
        {
            Console.WriteLine($"{Enemy.Name} Uses Bite!");
            Story.Continue(0);
            Random _rand = new Random();//dmgs
            int modifier = _rand.Next(-1, 2);
            var damageDealt = Enemy.BaseDamage + modifier - Character.armor;
            if (damageDealt < 0)
            {
                damageDealt = 0;
            }
            Character.currentHealth = Character.currentHealth - damageDealt;
            if (damageDealt != 0)
            {
                Story.ColorChanger(ConsoleColor.Red, $"Bite Deals {damageDealt} to you.");
            }
            else
            {
                Story.ColorChanger(ConsoleColor.Green, $"{Enemy.Name} Missed!");
            }
        }
        public static void Hunt(Entities Enemy) //if enemy uses this you can only hit them if the attack isQuick.
        {
            Console.WriteLine($"{Enemy.Name} Begins the Hunt!");
            Story.Continue(0);
            Enemy.isDodgeing = true;
            Enemy.DodgeRating = Enemy.DodgeRating + 2; //if going to use dodge stat
            Console.WriteLine($"{Enemy.Name} Becomes faster!");
        }
        public static void Overwhelm(Entities Enemy)
        {
            Console.WriteLine($"{Enemy.Name} jumps back and is preparing to leap..");
            //prep for large damage next turn
        }
        public static void RemoveAllEnemyBuffs(Entities Enemy)
        {
            Enemy.isDodgeing = false;
            Enemy.isBulwarked = false;
        }
        public static void DeBulwark(Entities Enemy)
        {
            Enemy.isBulwarked = false;
            Enemy.Defense = Enemy.Defense - 3;
            Console.WriteLine($"{Enemy.Name} has lost bulwark.");
        }
        public static void Overwhelm2(Entities Enemy)
        {
            Console.WriteLine($"{Enemy.Name} Uses Overwhelm!");
            Story.Continue(0);
            Random _rand = new Random();//dmgs
            int modifier = _rand.Next(1, 2);
            var damageDealt = Enemy.BaseDamage + 4 + modifier - Character.armor;
            if (damageDealt < 0)
            {
                damageDealt = 0;
            }
            if (damageDealt == 0)
            {
                Story.ColorChanger(ConsoleColor.Green, $"{Enemy.Name} Missed!");
            }
            else
            {
                Character.currentHealth = Character.currentHealth - damageDealt;

                Story.ColorChanger(ConsoleColor.Red, $"{Enemy.Name} tears into you and deals {damageDealt} to you.");
            }

        }
    }

}

