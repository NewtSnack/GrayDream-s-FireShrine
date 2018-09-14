using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FireShrine
{
    //the call would then be Knife.Stab() which is not modular
    //function must have nothing to do with IEquippable
    //Attack method that chooses attack from IEquippable attack list
    class Acts
    {
        //Status Getters Setters
        public static bool IsParrying { get; set; }
        public static bool IsQuick { get; set; }
        public static bool IsShattering { get; set; }
        public static bool IsGuarding { get; set; }
        public static bool IsAiming { get; set; }

        public static Random diceroll = new Random();

        //Blade
        public static int Stab(IEquippable Weapon)
        {
            int weapondam = diceroll.Next(-2,2) + Weapon.Damage;
            int Damage = weapondam + diceroll.Next(0, Character.strength / 2);
            Console.WriteLine($"You rush foward to plunge the {Character.equipped} into the enemy.");
            Story.Continue(0);
            return Damage;
        }
        public static int Slash(IEquippable Weapon)
        {
            IsQuick = true;
            int weapondam = diceroll.Next(-1,1) + Weapon.Damage;
            int Damage = weapondam + diceroll.Next(0, Character.finesse);
            Console.WriteLine("You unleash a flurry of quick strikes.");
            Story.Continue(0);
            return Damage;
        }
        public static int Parry()
        {
            IsParrying = true;
            Console.WriteLine("You stand ready to counter.");
            Story.Continue(0);
            return 0;
            //deal dmg if attacked next turn
        }
        //Blunt
        public static int Swing(IEquippable Weapon)
        {
            int weapondam = diceroll.Next(0, 2) + Weapon.Damage;
            int Damage = diceroll.Next(Character.strength - 3, Character.strength);      
            Console.WriteLine($"You swing the {Character.equipped} in a wide arc.");
            Story.Continue(0);
            return Damage;
        }
        public static int Smash(IEquippable Weapon)
        {
            IsShattering = true;
            int weapondam = diceroll.Next(3, 4) + Weapon.Damage;
            int Damage = weapondam + diceroll.Next(Character.strength - 2, Character.strength);
            Console.WriteLine($"You raise the {Character.equipped} high above your head.");
            Thread.Sleep(1000); Console.Write(". "); Thread.Sleep(1000); Console.Write(". "); Thread.Sleep(1000); Console.Write(". ");
            int roll = diceroll.Next(100);
            if (roll < 40) //40% chance to miss
            {
                Console.WriteLine($"The enemy dodges and the {Character.equipped} smashes into the ground.");
                Damage = 0;
            }
            Story.Continue(0);

            return Damage;

        }

        public static int Guard()
        {
            Console.WriteLine("You bolster your defense");
            IsGuarding = true;
            Story.Continue(0);

            return 0;
        }
        //Ranged
        public static int Draw_A_Bead()
        {
            IsAiming = true;
            Console.WriteLine($"You line up the enemy in your sights. Chance to hit increased!");
            Story.Continue(0);
            return 0;

        }
        public static int Shoot(Ranged Weapon)
        {
            if (Weapon.AmmoCount <= 0)
            {
                Console.WriteLine("You are out of ammo!");
                Story.Continue(0);
                return 0;
            }
            int weapondmg = diceroll.Next(Damvaluelow, Damvaluehigh);
            int damageDealt = weapondmg;
            int roll = diceroll.Next(0, 5) + Character.finesse / 2; //chancetohit
            Console.WriteLine("You pull the trigger...");
            if (roll < 2)
            {
                damageDealt = 0;
            }
            bulletsleft--;
            Character.Inventory[invslot][5][0] = bulletsleft.ToString(); //update the inventory
            Story.Continue(0);
            return damageDealt;
        }
        public static int DoubleTap()
        {
            if (bulletsleft <= 0)
            {
                isQuick = true;
                Console.WriteLine("You are out of ammo!");
                Story.Continue(0);
                return 0;
            }
            //chance to do dmg twice,
            double weapondmg1 = Damvaluehigh * .6;
            double weapondmg2 = Damvaluehigh * .7;
            Console.WriteLine("Deftly, you let loose two controlled shots!");

            int roll1 = diceroll.Next(0, 10) + (Character.finesse / 3);
            int roll2 = diceroll.Next(0, 10) + (Character.finesse / 3);
            if (roll1 < 3)
            {
                Console.WriteLine("The first shot missed!");
                weapondmg1 = 0;
                bulletsleft--;

            }
            if (bulletsleft <= 0)
            {
                Console.WriteLine("You are out of ammo!");
            }
            else
            {
                if (roll2 < 2)
                {
                    Console.WriteLine("The second shot missed!");
                    weapondmg2 = 0;
                }
            }

            int damageDealt = (int)(weapondmg1 + weapondmg2);
            bulletsleft--;
            Character.Inventory[invslot][5][0] = bulletsleft.ToString(); //update the inventory
            Story.Continue(0);
            return damageDealt;
        }
        //Monk
        public static int Punch()
        {
            int damageDealt = diceroll.Next(Character.finesse - 2, Character.finesse);
            Console.WriteLine("You find an oppertunity to strike!");
            Story.Continue(0);

            return damageDealt;
        }
        public static int Grab()
        {
            int damageDealt = diceroll.Next(Character.strength - 2, Character.strength);
            Console.WriteLine("You manage to strangle and slam the enemy!");
            Story.Continue(0);

            return damageDealt;
        }
    }
}
