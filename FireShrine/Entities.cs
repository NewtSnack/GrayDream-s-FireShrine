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
        public int Defense = 5;
        public int DodgeRating = 2;
        public string[] movelist = new string[] { "Attack1", "Attack2" };
        public int BaseDamage = 2;

        public bool isFlying = false;
        public bool isDodgeing = false;
    }
    public class AttackList
    {
        public static void Bulwark(Entities Enemy)
        {
            Console.WriteLine($"{Enemy.Name} Uses Bulwark!");
            Story.Continue(0);
            Enemy.Defense = Enemy.Defense + 3;
            Console.WriteLine($"{Enemy.Name} increases its defense!");
        }
        public static void Bite(Entities Enemy)
        {
            Console.WriteLine($"{Enemy.Name} Uses Bite!");
            Story.Continue(0);
            Random _rand = new Random();//dmgs
            int modifier = _rand.Next(-2, 2);
            var damageDealt = Enemy.BaseDamage + modifier - Character.armor;
            Character.currentHealth = Character.currentHealth - damageDealt;
            Story.ColorChanger(ConsoleColor.Red, $"Bite Deals {damageDealt} to you.");
        }
        public static void Hunt(Entities Enemy)
        {
            Console.WriteLine($"{Enemy.Name} Begins the Hunt!");
            Story.Continue(0);
            Enemy.isDodgeing = true;
            Enemy.DodgeRating = Enemy.DodgeRating + 2;
            Console.WriteLine($"{Enemy.Name} Becomes faster!");
        }
        public static void Overwhelm(Entities Enemy)
        {
            Console.WriteLine($"{Enemy.Name} jumps back and is preparing to leap..");
            //prep for large damage next turn
        }
    }

}

