using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FireShrine
{
    public static class Character
    {
        public static int currentHealth = 8;
        public static double vitality = Math.Round((10 + (strength + 2.00) / 5.00));
        public static int armor = 3;
        public static int strength = 5;
        public static int finesse = 4;
        public static int initiative = 3;
        public static int mental = 5;
        public static int MaxSanity = 10;
        public static Unarmed Hands = new Unarmed("Fists");
        
        //getters
        public static string HpFraction { get { return (currentHealth.ToString() + "/" + vitality.ToString()); } }
        public static string MindFraction { get { return (mental.ToString() + "/" + MaxSanity.ToString()); } }
        public static float hpfractionActual { get { return (currentHealth / (float)vitality); } }
        public static IEquippable EquippedItem { get; set; } //The Item Currently Equipped
        public static IEquippable Equipped { get; set; }



        public static List<IItems> Inventory2 = new List<IItems>();
        public static string[] battleActs = new string[] {"Punch", "Grab", "Counter" };

        //survival stats
        static double maxHunger = 20;
        public static double currentHunger = 10;
        static double maxThirst = 20;
        public static double currentThirst = 10;

        //getters
        static double hungerfraction { get { return (currentHunger / maxHunger); } }
        static double thirstfraction { get { return (currentThirst / maxThirst); } }

        public static void DisplayStats()
        {
            //stat logic 
            if (mental > MaxSanity)
            {
                mental = MaxSanity;
            }

            //later may want to implement different message for the same threshold based on a rand.

            //HEALTH
            Console.WriteLine("Health: {0}", HpFraction);
            if (hpfractionActual < .2)
            {
                ConsoleColor currentcolor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("        These wounds are mortal. Without help you will not last long.");
                Console.ForegroundColor = currentcolor;
            }
            if (hpfractionActual >= .2 & hpfractionActual < .6)
            {
                Console.WriteLine("        A few scratches");
            }
            if (hpfractionActual >= .6)
            {
                Console.WriteLine("        Feeling Good");
            }

            //SANITY
            Console.WriteLine();
            Console.WriteLine("Sanity: {0}", MindFraction);
            if (mental < 4)
            {
                ConsoleColor currentcolor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("        ...Make it stop.");
                Console.ForegroundColor = currentcolor;
            }
            if (mental >= 4 & mental < 6)
            {
                Console.WriteLine("        Your head throbs, a chill runs down your spine");
            }
            if (mental >= 6 & mental < 8)
            {
                Console.WriteLine("        A Slight Headache");
            }
            if (mental >= 8)
            {
                Console.WriteLine("        Clear as a bell");
            }
            Console.WriteLine("-------------------");
            Console.WriteLine();
            //Hunger
            if (hungerfraction > 1.5)
            {
                currentHunger = maxHunger * 1.5; //handle overflow
            }
            Console.WriteLine("Hunger: {0} / {1}", Math.Round(currentHunger), maxHunger);
            if (hungerfraction < .2)
            {
                ConsoleColor currentcolor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("        Hunger gnaws at your innards as you feel weaker.");
                strength = -4;
                strength = (strength < 0) ? strength = 0 : strength;
                Console.ForegroundColor = currentcolor;
            }
            if (hungerfraction >= .2 & hungerfraction < .5)
            {
                Console.WriteLine("        You need to look for something to eat.");
            }
            if (hungerfraction >= .5 & hungerfraction < .9)
            {
                Console.WriteLine("        Slightly peckish ...");
            }
            if (hungerfraction >= .9 & hungerfraction <= 1.05)
            {
                Console.WriteLine("        Satiated");
            }
            if (hungerfraction > 1.05)
            {
                Console.WriteLine("        You shouldnt eat anymore");
            }
            //Thirst
            if (thirstfraction > 1.5)
            {
                currentThirst = maxThirst * 1.5; //handle overflow
            }
            Console.WriteLine("Thirst: {0} / {1}", Math.Round(currentThirst), Math.Round(maxThirst));
            if (thirstfraction < .2)
            {
                ConsoleColor currentcolor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("        You are dehydrated");
                finesse = -4;
                initiative = -1;
                mental = (mental < 0) ? mental = 0 : mental;
                Console.ForegroundColor = currentcolor;
            }
            if (thirstfraction >= .2 & thirstfraction < .5)
            {
                Console.WriteLine("        You feel thirsty");
            }
            if (thirstfraction >= .5 & thirstfraction < .9)
            {
                Console.WriteLine("        Your throat feels a bit dry");
            }
            if (thirstfraction >= .9 & thirstfraction <= 1.05)
            {
                Console.WriteLine("        Quenched");
            }
            if (thirstfraction > 1.05)
            {
                Console.WriteLine("        You shouldnt drink anymore");
            }
            Console.WriteLine("Armor Rating: {0}", armor);
            Console.WriteLine("Initiative: {0}", initiative);
            //later put dex, str, ini,
        }

    }    
    class Program
    {
        static string playerName;
        public static string[] attrilist = { "Container", "Melee Weapon", "Ranged Weapon", "Can Hold Liquid", "Flammable", "Toxic", "Sharp", "Blunt", "Edible", "Drink", "Cursed" };
        public static int maxInventory = 2; //effectively 3
        static int nextSlot = 0;
        public static bool isInBattle = false;




        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 25);
            Title();

            GetPlayerName();
            StartGame();
            Console.WriteLine("End of Demo.");
            Story.Continue(0);

        }
        public static void GetPlayerName()
        {
            Console.WriteLine($"Declare your name: ");
            Character.Hands.Equip();
            playerName = Console.ReadLine();
            if (playerName == "dev")
            {
                DevActions();
            }
            Console.Clear();
        }
        public static void Title()
        {
            Console.WriteLine(@"                                         ______ _          _____ _          _            ");
            Console.WriteLine(@"                                         |  ___(_)        /  ___| |        (_)           ");
            Console.WriteLine(@"                                         | |_   _ _ __ ___\ `--.| |__  _ __ _ _ __   ___ ");
            Console.WriteLine(@"                                         |  _| | | '__/ _ \`--. \ '_ \| '__| | '_ \ / _ \");
            Console.WriteLine(@"                                         | |   | | | |  __/\__/ / | | | |  | | | | |  __/");
            Console.WriteLine(@"                                         \_|   |_|_|  \___\____/|_| |_|_|  |_|_| |_|\___|");
            Console.WriteLine();
            Console.WriteLine("                                                                                                 -Darron C.");
            Story.Continue(1);

        }
        public static void StartGame()
        {
            Console.WriteLine("                                    --------------Your Journey Begins, {0}--------------", playerName);
            Thread.Sleep(2000);
            Console.Clear();
            Story.Story01();

            //try threading
        }
        //custom adding of items to inventory with Name, Description, Damage, Attributes, and Durability        
        public static void NuAdd(IItems Item)
        {
            if (nextSlot > maxInventory)
            {
                Console.WriteLine("Inventory is Full");
                Console.WriteLine();
            }
            else
            {
                Character.Inventory2.Add(Item);
                nextSlot++;
                Story.ColorChanger(ConsoleColor.Blue, $"{Item.Name} added to inventory");
            }
        }
        public static void NuDrop(IItems Item)
        {
            Story.ColorChanger(ConsoleColor.Blue, $"{Item.Name} Dropped from inventory");
            Character.Inventory2.Remove(Item);
        }
        public static void NuUse(IItems Item)
        {
            Story.ColorChanger(ConsoleColor.Blue, $"{Item.Name} used.");

        }

        public static void StatusTimeTicker(int tick)
        {
            //surival
            Character.currentHunger = Character.currentHunger - tick;
            Character.currentThirst = Character.currentThirst - tick * 1.7;
        }
        public static void DevActions()
        {
            Console.WriteLine("Here be the testing envoirnment");
            Blade Knife = new Blade("Dull Knife", "A small rusted carving knife.", 3);
            Blade dullknife = new Blade("Worn Knife", "A small rusted carving knife.", 6);
            dullknife.ToInv();
            Knife.ToInv();
            Menus.ChoiceSelection(new string[] { "A", "B" });
            Story.Continue(0);
            maxInventory = 4;
            dullknife.Equip();
            Entities ManBat = new Entities
            {
                Name = "Grotesque Bat",
                movelist = new string[] { "Hunt", "Bite", "Overwhelm", "Bulwark" }            
            };

            isInBattle = true;
            Battle(ManBat);
        }

        public static void Battle(Entities Enemy) //give a entity when calling
        {
            Console.WriteLine($"You've engaged in combat with the {Enemy.Name}");
            Console.WriteLine("                        Kill Or Be Killed.");
            int turncounter = 0; //use this to control buffs that last longer than 1 turn
            int skillstateG = -3;//guard will last 1 turn
            int skillstateP = -3;//parry will last 1 turn
            int skillstateA = -3;//Aiming will last 1 turns
            int FinalDamage;
            while (isInBattle == true)
            {
                Console.WriteLine();
                Console.WriteLine("Player Health: {0}", Character.HpFraction);
                Console.WriteLine();
                Console.WriteLine();
                Random diceroll = new Random();
                //put in Random battle messages here
                //Player Turn
                var i = 0;
                foreach (var item in Character.Equipped.BattleActs)
                {
                    i++;
                    Console.WriteLine($"({i}) {item}");
                }
                Console.WriteLine();
                var keypress = Console.ReadKey(true).Key;

                while (keypress != ConsoleKey.D1 & keypress != ConsoleKey.D2 & keypress != ConsoleKey.D3 & keypress
                            != ConsoleKey.H & keypress != ConsoleKey.I & keypress != ConsoleKey.P & keypress != ConsoleKey.Enter)
                {
                    keypress = Console.ReadKey(true).Key;
                }
                switch (keypress)//add logic to add move acceptable keypresses if there are more than 3 actions for a weapon
                {
                    case ConsoleKey.D1:
                        FinalDamage = Character.Equipped.Attack1();
                        break;
                    case ConsoleKey.D2:
                        FinalDamage = Character.Equipped.Attack2();
                        break;
                    case ConsoleKey.D3:
                        FinalDamage = Character.Equipped.Attack3();
                        break;
                    default:
                        Menus.MenuSelection(keypress);
                        Console.Clear();
                        continue;                        

                }
                FinalDamage = (int)(FinalDamage - (Enemy.Defense  / 3));
                if (FinalDamage < 0)
                {
                    FinalDamage = 0;
                }
                if (Acts.IsQuick == false & Enemy.IsDodgeing == true & Acts.IsParrying == false & Acts.IsGuarding == false)
                {
                    Console.WriteLine("The attack was too slow to land!");
                    FinalDamage = 0;
                }
                if (Acts.IsShattering == true & Enemy.IsBulwarked)
                {
                    Console.WriteLine("You break your enemy's defenses!");
                    AttackList.DeBulwark(Enemy);
                }                
                if (FinalDamage == 0 & Acts.IsParrying == false & Acts.IsGuarding == false & Acts.IsAiming == false)
                {
                    Console.WriteLine("The {0} resists your attack.",Enemy.Name);                    
                }
                if (FinalDamage != 0)
                {
                    Enemy.HealthPoints = (int)(Enemy.HealthPoints - (FinalDamage));

                    Story.ColorChanger(ConsoleColor.Green, $"{Enemy.Name} takes {FinalDamage} Damage!");
                }

                //remove enemy buffs from last turn
                Enemy.IsDodgeing = false;
                if (Enemy.IsBulwarked == true)
                {
                    AttackList.DeBulwark(Enemy);

                }

                //AttackList.RemoveAllEnemyBuffs(Enemy);
                if (Enemy.HealthPoints <= 0)
                {
                    isInBattle = false;
                    Console.WriteLine($"{Enemy.Name} has been killed.");
                    Story.Continue(0);
                    break;
                }


                Thread.Sleep(1000);
                //Enemy Turn
                if (Acts.IsGuarding)
                {
                    Character.armor = Character.armor + 5;
                    skillstateG = turncounter; 
                }
                if (Acts.IsAiming)
                {
                    Character.finesse = Character.finesse + 5;
                    skillstateA = turncounter;
                }

                if (Acts.IsParrying)
                {
                    Character.armor = Character.armor + 2;
                    skillstateP = turncounter;
                }
                //turn debuffing
                if (turncounter - skillstateG == 1)
                {
                    Character.armor = Character.armor - 5;
                }
                if (turncounter - skillstateA == 2)
                {
                    Character.finesse = Character.finesse - 5;
                    Acts.IsAiming = false;
                }
                if (turncounter - skillstateP == 1)
                {
                    Character.armor = Character.armor - 2;
                }
                var EnemyAttack = Enemy.movelist[diceroll.Next(0, Enemy.movelist.Count())]; //every skill has equal chance, change this logic to make certain attacks more weighted.

                //AttackList
                if (Enemy.IsOverwhelming == true)
                {
                    AttackList.Overwhelm2(Enemy);
                    if (Acts.IsParrying == true)
                    {
                        Console.WriteLine("You manage to return some damage.");
                        int parryDamage = Character.finesse - 2 + diceroll.Next(0,1);
                        Enemy.HealthPoints = Enemy.HealthPoints - parryDamage;
                        Story.ColorChanger(ConsoleColor.Green, $"Parry Returns {parryDamage} Damage to {Enemy.Name}");
                    }
                    Enemy.IsOverwhelming = false;
                }
                else
                {
                    if (EnemyAttack == "Bite")
                    {
                        AttackList.Bite(Enemy);
                        if (Acts.IsParrying == true)
                        {
                            Console.WriteLine("You manage to return some damage.");
                            int parryDamage = 2;
                            Enemy.HealthPoints = Enemy.HealthPoints - parryDamage;
                            Story.ColorChanger(ConsoleColor.Green, $"Parry Returns {parryDamage} Damage to {Enemy.Name}");
                        }
                    }
                    if (EnemyAttack == "Bulwark")
                    {
                        int skillcounter1 = turncounter;
                        AttackList.Bulwark(Enemy);
                    }
                    if (EnemyAttack == "Hunt")
                    {
                        AttackList.Hunt(Enemy);
                    }
                    if (EnemyAttack == "Overwhelm")
                    {
                        AttackList.Overwhelm(Enemy);
                        Enemy.IsOverwhelming = true;
                    }
                    Story.Continue(0);
                }
                



                //remove character attack buffs
                Acts.IsQuick = false;
                Acts.IsShattering = false;
                Acts.IsGuarding = false;
                Acts.IsParrying = false;
                if (Character.currentHealth <= 0)
                {
                    isInBattle = false;
                    Console.WriteLine("You Are Dead.");
                    Story.Continue(0);
                    Environment.Exit(1);
                    //Trigger PlayDeath()
                }
                turncounter++;
            }

        }        
    }

}
