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
        //getters
        public static string HpFraction { get { return (currentHealth.ToString() + "/" + vitality.ToString()); } }
        public static string MindFraction { get { return (mental.ToString() + "/" + MaxSanity.ToString()); } }
        public static float hpfractionActual { get { return (currentHealth / (float)vitality); } }
        
        public static List<string[][]> Inventory = new List<string[][]>();
        public static List<IItems> Inventory2 = new List<IItems>();
        public static string equipped = "Fists";
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
    public class BattleActions
    {
        //BattleAction States/Buffs
        public static bool isParrying = false;
        public static bool isQuick = false;
        public static bool isShattering = false;
        public static bool isGuarding = false;
        public static bool isAiming = false;
        public static int invslot; // value is set when when MoveList is called. MoveList must be called when switching equipped item.

        static Random diceroll = new Random();
        static int Damvaluelow { get { return Int32.Parse(Character.Inventory[invslot][2][0]); } }
        static int Damvaluehigh { get { return Int32.Parse(Character.Inventory[invslot][2][1]); ; } }
        public static int bulletsleft;


        public static void MoveList()
        {
            if (Character.equipped != "Fists")
            {
                var num = Character.Inventory.Count();
                for (int i = 0; i < Character.Inventory.Count(); i++)
                {
                    if (Character.Inventory[i][0][0] == Character.equipped)
                    {
                        for (var k = 0; k < (Character.Inventory[i][3]).Count(); k++)
                        {
                            int num2 = Character.Inventory[i][3].Count();
                            var itemAttri = Character.Inventory[i][3][k];
                            if (itemAttri == "Sharp")
                            {
                                Character.battleActs = new string[] { "Stab", "Slash", "Parry" };
                                invslot = i;
                                break;
                            }
                            if (itemAttri == "Ranged Weapon")
                            {
                                Character.battleActs = new string[] { "Shoot", "Draw A Bead", "Double Tap" };
                                invslot = i;
                                break;

                            }
                            if (itemAttri == "Blunt")
                            {
                                Character.battleActs = new string[] { "Swing", "Smash", "Block" };
                                invslot = i;
                                break;
                            }
                        }
                    }
                }
                if (Character.Inventory[invslot][3][0] == "Ranged Weapon")
                {
                    bulletsleft = Int32.Parse(Character.Inventory[invslot][5][0]);
                }
            }
            else
            {
                Character.battleActs = new string[] { "Punch", "Grab", "Block" };
            }



            //var invslot = Character.Inventory.IndexOf(Character.equipped); //the equipped item searches the inventory list for the attrib and returns a movelist array that can be added to with .Add()
        }
        public static int ActstoActions(string Acts)
        {
            switch (Acts)
            {
                case "Stab":
                    return Stab();
                case "Slash":
                    return Slash();
                case "Parry":
                    return Parry();
                case "Smash":
                    return Smash();
                case "Swing":
                    return Swing();
                case "Block":
                    return Block();
                case "Punch":
                    return Punch();
                case "Grab":
                    return Grab();
                case "Counter":
                    return Parry();//until i think of something cool for countering or want to change whats its based on.
                case "Shoot":
                    return Shoot();
                case "Draw A Bead":
                    return Draw_A_Bead();
                case "Double Tap":
                    return DoubleTap();
                default:
                    return 1;//this should never happen but now it will always return something
            }
        }
        //sharp
        public static int Stab()
        {
            int weapondmg = diceroll.Next(Damvaluelow, Damvaluehigh);
            int damageDealt = weapondmg + diceroll.Next(0, Character.strength / 2);
            Console.WriteLine($"You rush foward to plunge the {Character.equipped} into the enemy.");
            Story.Continue(0);
            return damageDealt;

            //later add chance to bleed
            //Get dmg of equipped weapon for all these actions
        }
        public static int Slash()
        {
            isQuick = true;
            int weapondmg = diceroll.Next(Damvaluelow - 1, Damvaluehigh + 1);
            int damageDealt = weapondmg + diceroll.Next(0, Character.strength);
            Console.WriteLine("You unleash a flurry of quick strikes");
            Story.Continue(0);
            return damageDealt;
        }
        public static int Parry()
        {
            isParrying = true;
            Console.WriteLine("You stand ready to counter.");
            Story.Continue(0);
            return 0;
            //deal dmg if attacked next turn
        }
        //blunt
        public static int Swing()
        {
            int weapondmg = diceroll.Next(Damvaluelow, Damvaluehigh);
            int damageDealt = weapondmg + diceroll.Next(Character.strength - 3, Character.strength);
            Console.WriteLine($"You swing the {Character.equipped} in a wide arc.");
            Story.Continue(0);
            return damageDealt;
        }
        public static int Smash()
        {
            isShattering = true;
            int weapondmg = diceroll.Next(Damvaluelow, Damvaluehigh);
            int damageDealt = weapondmg + diceroll.Next(Character.strength - 2, Character.strength);
            Console.WriteLine($"You raise the {Character.equipped} high above your head.");
            int roll = diceroll.Next(100);
            if (roll < 30) //30% chance to miss
            {
                Console.WriteLine($"The enemy dodges and the {Character.equipped} smashes into the ground.");
                damageDealt = 0;
            }
            Story.Continue(0);

            return damageDealt;

        }

        public static int Block()
        {
            Console.WriteLine("You bolster your defense");
            isGuarding = true;
            Story.Continue(0);

            return 0;
        }
        //Gun
        public static int Draw_A_Bead()
        {
            isAiming = true;
            Console.WriteLine($"You line up the enemy in your sights. Chance to hit increased!");
            Story.Continue(0);
            return 0;
            
        }
        public static int Shoot()
        {
            if (bulletsleft <= 0) 
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
            if (roll1 < 3 )
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
        //public static int Counter()
        //{
        //    int damageDealt = diceroll.Next(Character.strength - 2, Character.strength);
        //    return damageDealt;
        //}
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
        public static void BeliAdd(string[] itemname, string[] descript, string[] damrange, string[] attrib, string[] dura, string[] ammo)
        {
            if (nextSlot > maxInventory)
            {
                Console.WriteLine("Inventory is Full");
                Console.WriteLine();
            }
            else
            {
                Character.Inventory.Insert(nextSlot, new string[6][] { itemname, descript, damrange, attrib, dura, ammo });
                nextSlot++;
                Story.ColorChanger(ConsoleColor.Blue, $"{itemname[0]} added to inventory");
            }
        }
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

        public static void BeliDrop(int item)
        {
            //this should be called in the MenuSelection menu
            //later can implement a system to have the dropped item exist in game world.
        }
        public static void BeliUse(int item)
        {
            Story.ColorChanger(ConsoleColor.Blue, $"{Character.Inventory[item][0]} used.");
            Character.Inventory.RemoveAt(item);
        }
        public static void StatusTimeTicker(int tick)
        {
            //surival
            Character.currentHunger = Character.currentHunger - tick;
            Character.currentThirst = Character.currentThirst - tick * 1.7;
        }
        public static string ItemGetByAttribute(string Attribute) //Searches inventory by attribute and returns the name string
        {
            List<string> returnSearch = new List<string>();

            foreach (string[][] item in Character.Inventory) //Check For a "Weapon Attribute" 
            {
                if (Array.IndexOf(item[3], Attribute) >= 0)
                {
                    returnSearch.Add(item[0][0]);
                    string[] ArraySearch = returnSearch.ToArray();
                    return ArraySearch[0];
                }
            }
            return null;
        }
        public static void DevActions()
        {
            Console.WriteLine("Here be the testing envoirnment");
            Blade Knife = new Blade("Dull Knife", "A small rusted carving knife.", 3);
            Blade Knife2 = new Blade("Worn Knife", "A small rusted carving knife.", 3);
            Knife2.ToInv();
            Knife.ToInv();
            string[] choicetable = { "A", "B" };
            Menus.ChoiceSelection(choicetable);
            Story.Continue(0);
            maxInventory = 4;
            Character.equipped = "Dull Knife";
            Entities ManBat = new Entities
            {
                Name = "Grotesque Bat",
                movelist = new string[] { "Hunt", "Bite", "Overwhelm", "Bulwark" }
            
            };

            isInBattle = true;
            BattleActions.MoveList();
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
                foreach (var item in Character.battleActs)
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
                        FinalDamage = BattleActions.ActstoActions(Character.battleActs[0]);//return dmgdealt
                        break;                        
                    case ConsoleKey.D2:
                        FinalDamage = BattleActions.ActstoActions(Character.battleActs[1]);
                        break;
                    case ConsoleKey.D3:
                        FinalDamage = BattleActions.ActstoActions(Character.battleActs[2]);
                        break;
                    default:
                        Menus.MenuSelection(keypress);
                        continue;                        

                }
                FinalDamage = (int)(FinalDamage - Enemy.Defense  /3);
                if (FinalDamage < 0)
                {
                    FinalDamage = 0;
                }
                if (BattleActions.isQuick == false & Enemy.isDodgeing == true & BattleActions.isParrying == false & BattleActions.isGuarding == false)
                {
                    Console.WriteLine("The attack was too slow to land!");
                    FinalDamage = 0;
                }
                if (BattleActions.isShattering == true & Enemy.isBulwarked)
                {
                    Console.WriteLine("You break your enemy's defenses!");
                    AttackList.DeBulwark(Enemy);
                }
                if (FinalDamage != 0)
                {
                    Enemy.HealthPoints = (int)(Enemy.HealthPoints - (FinalDamage));
                    
                    Story.ColorChanger(ConsoleColor.Green, $"{Enemy.Name} takes {FinalDamage} Damage!");
                }
                if (FinalDamage == 0 & BattleActions.isParrying == false & BattleActions.isGuarding == false & BattleActions.isAiming == false)
                {
                    Console.WriteLine("You missed...");
                }



                //remove enemy buffs from last turn
                Enemy.isDodgeing = false;
                if (Enemy.isBulwarked == true)
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
                if (BattleActions.isGuarding)
                {
                    Character.armor = Character.armor + 5;
                    skillstateG = turncounter; 
                }
                if (BattleActions.isAiming)
                {
                    Character.finesse = Character.finesse + 5;
                    skillstateA = turncounter;
                }

                if (BattleActions.isParrying)
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
                    BattleActions.isAiming = false;
                }
                if (turncounter - skillstateP == 1)
                {
                    Character.armor = Character.armor - 2;
                }
                var EnemyAttack = Enemy.movelist[diceroll.Next(0, Enemy.movelist.Count())]; //every skill has equal chance, change this logic to make certain attacks more weighted.

                //AttackList
                if (Enemy.isOverwhelming == true)
                {
                    AttackList.Overwhelm2(Enemy);
                    if (BattleActions.isParrying == true)
                    {
                        Console.WriteLine("You manage to return some damage.");
                        int parryDamage = Character.finesse - 2 + diceroll.Next(0,1);
                        Enemy.HealthPoints = Enemy.HealthPoints - parryDamage;
                        Story.ColorChanger(ConsoleColor.Green, $"Parry Returns {parryDamage} Damage to {Enemy.Name}");
                    }
                    Enemy.isOverwhelming = false;
                }
                else
                {
                    if (EnemyAttack == "Bite")
                    {
                        AttackList.Bite(Enemy);
                        if (BattleActions.isParrying == true)
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
                        Enemy.isOverwhelming = true;
                    }
                }
                



                //remove character attack buffs
                BattleActions.isQuick = false;
                BattleActions.isShattering = false;
                BattleActions.isGuarding = false;
                BattleActions.isParrying = false;

                if (Character.currentHealth < 0)
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
