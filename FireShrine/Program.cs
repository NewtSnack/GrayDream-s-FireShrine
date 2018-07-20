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
        public static int dexterity = 5;
        public static int initiative = 3;
        public static int mental = 5;
        public static int MaxSanity = 10;
        public static string hpFraction = currentHealth.ToString() + "/" + vitality.ToString();
        public static string mindFraction = mental.ToString() + "/" + MaxSanity.ToString();
        public static float hpfractionActual = (float)currentHealth / (float)vitality;
        public static List<string[][]> Inventory = new List<string[][]>();
        public static string equipped = "Fists";
        public static string[] battleActs = new string[] {"Punch", "Grab", "Block" };

        //survival stats
        static double maxHunger = 20;
        public static double currentHunger = 10;
        static double hungerfraction = currentHunger / maxHunger;
        static double maxThirst = 20;
        public static double currentThirst = 10;
        static double thirstfraction = currentThirst / maxThirst;

        public static void DisplayStats()
        {
            //stat logic 
            if (mental > MaxSanity)
            {
                mental = MaxSanity;
            }

            //later may want to implement different message for the same threshold based on a rand.

            //HEALTH
            Console.WriteLine("Health: {0}", hpFraction);
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
            Console.WriteLine("Sanity: {0}", mindFraction);
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
                dexterity = -4;
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
            Story.Continue(0);
        }

    }
    public class BattleActions
    {
        //BattleAction States/Buffs
        public static bool parryState;
        public static bool isQuick = false;
        public static int invslot; // value is set when when MoveList is called. MoveList must be called when switching equipped item.

        static Random diceroll = new Random();
        static int damvaluelow = Int32.Parse(Character.Inventory[invslot][2][0]);
        static int damvaluehigh = Int32.Parse(Character.Inventory[invslot][2][1]);


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
                                Character.battleActs = new string[] { "Draw a Bead", "Shoot" };
                                invslot = i;
                                break;

                            }
                            if (itemAttri == "Blunt")
                            {
                                Character.battleActs = new string[] { "Smash", "Swing", "Block" };
                                invslot = i;
                                break;
                            }
                        }
                    }                    
                }
            }
            else
            {
                Character.battleActs = new string[] { "Punch", "Grab", "Block" };
            }
                
                
            
                //var invslot = Character.Inventory.IndexOf(Character.equipped); //the equipped item searches the inventory list for the attrib and returns a movelist array that can be added to with .Add()
        }
        public static void ActstoActions(string Acts)
        {
            switch (Acts)
            {
                case "Stab":
                    Stab();
                    break;
                case "Slash":
                    Slash();
                    break;
                case "Parry":
                    Parry();
                    break;
                default:
                    break;
            }
        }
        public static void Stab()
        {
            int weapondmg = diceroll.Next(damvaluelow, damvaluehigh);
            Console.WriteLine($"You rush foward and plunge the {Character.equipped} into the enemy.");
            int damageDealt = weapondmg + diceroll.Next(0, Character.strength / 2);

            //Get dmg of equipped weapon for all these actions
        }
        public static void Slash()
        {
            isQuick = true;
            Console.WriteLine("You unleash a flurry of quick strikes");
            Story.Continue(0);
        }
        public static void Parry()
        {
            parryState = true;
            //deal dmg if attacked next turn
        }
    }

    public class Menues
    //ChoiceSelection -> MenuSelection -> InspectItem

    {
        static int chooseAmount; //amount of Options asked to player
        static int theChoice = 0;
        public static int ChoiceSelection(string choice1, string choice2, string choice3, string choice4, string choice5) //The Method That presents Choices to the player and returns a selection as a 
        {
            while (theChoice == 0)
            {

                Console.WriteLine("(1) {0}", choice1);
                Console.WriteLine("(2) {0}", choice2);
                if (choice3 != null)
                {
                    chooseAmount = 3;
                    Console.WriteLine("(3) {0}", choice3);
                    if (choice4 != null)
                    {
                        chooseAmount = 4;
                        Console.WriteLine("(4) {0}", choice4);
                        if (choice5 != null)
                        {
                            chooseAmount = 5;
                            Console.WriteLine("(5) {0}", choice5);
                        }
                    }
                }
                var keyPress = Console.ReadKey(true).Key;
                while (keyPress != ConsoleKey.D1 & keyPress != ConsoleKey.D2 & keyPress != ConsoleKey.D3 & keyPress != ConsoleKey.D4 & keyPress != ConsoleKey.D5
                     & keyPress != ConsoleKey.I & keyPress != ConsoleKey.P & keyPress != ConsoleKey.H & keyPress != ConsoleKey.Escape)
                {
                    keyPress = Console.ReadKey(true).Key;
                }

                switch (keyPress)
                {
                    case ConsoleKey.D1:
                        theChoice = 1;
                        break;
                    case ConsoleKey.D2:
                        theChoice = 2;
                        break;
                    default:
                        switch (keyPress)
                        {
                            case ConsoleKey.D3:
                                if (chooseAmount != 0)
                                {
                                    theChoice = 3;
                                }
                                break;
                            case ConsoleKey.D4:
                                if (chooseAmount != 0)
                                {
                                    theChoice = 4;
                                }
                                break;
                            case ConsoleKey.D5:
                                if (chooseAmount != 0)
                                {
                                    theChoice = 5;
                                }
                                break;
                            default:
                                HUD.MenuSelection(keyPress);
                                break;
                        }

                        Story.Continue(1);
                        Console.Clear();
                        break;
                        //move jumptostory outside these methods.
                        //needs to call itself and return a choice
                }
                if (theChoice == 0)
                {
                    Story.JumpToStory();
                }
            }
            Console.Clear();
            var returntheChoice = theChoice;
            theChoice = 0; //reset
            return returntheChoice;
        }
        public static void InspectItem(int inspect)
        {
            if (inspect <= 6 & inspect > 0) //may change this to if
            {
                //check inventory is not empty before call AND if item index exists

                if (Character.Inventory.Count >= inspect)
                {
                    Console.WriteLine("Name:  " + Character.Inventory[inspect - 1][0][0]);
                    Console.WriteLine(Character.Inventory[inspect - 1][1][0]);
                    Console.WriteLine("Damage:       " + Character.Inventory[inspect - 1][2][0] + "-" + Character.Inventory[inspect - 1][2][1]);
                    switch (Character.Inventory[inspect - 1][3].Count())
                    //Each item can only hold up to three attributes
                    {
                        case 1:
                            Console.WriteLine("Attributes:   " + Character.Inventory[inspect - 1][3][0]);
                            break;
                        case 2:
                            Console.WriteLine("Attributes:   " + Character.Inventory[inspect - 1][3][0] + ", " + Character.Inventory[inspect - 1][3][1]);
                            break;
                        case 3:
                            Console.WriteLine("Attributes:   " + Character.Inventory[inspect - 1][3][0] + ", " + Character.Inventory[inspect - 1][3][1] + ", " + Character.Inventory[inspect - 1][3][2]);
                            break;
                        default:
                            break;
                    }
                    if (Character.Inventory[inspect - 1][3].Contains("Ranged Weapon"))
                    {
                        Console.WriteLine("Ammunition:   " + Character.Inventory[inspect - 1][4][0] + "Shots Remaining");
                    }
                    //show ammo if gun
                    if (Character.Inventory[inspect - 1][3].Contains("Edible") | Character.Inventory[inspect - 1][3].Contains("Drink"))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Press U to Use item");
                        ConsoleKey keyPress = Console.ReadKey(true).Key;


                        if (keyPress == ConsoleKey.U)
                        {
                            int healthheal = Int32.Parse(Character.Inventory[inspect - 1][4][0]); //dura
                            int hungerheal = Int32.Parse(Character.Inventory[inspect - 1][2][0]); //lower dam
                            int thirstheal = Int32.Parse(Character.Inventory[inspect - 1][2][1]); //upper dam

                            Character.currentHealth = Character.currentHealth + healthheal;
                            Character.currentHunger = Character.currentHunger + hungerheal;
                            Character.currentThirst = Character.currentThirst + thirstheal;
                            Program.BeliDrop(inspect - 1);
                        }                        
                    }
                    else
                    {
                        Console.WriteLine("You have no usable items in your inventory.");
                    }
                    if (Character.Inventory[inspect - 1][3].Contains("Melee Weapon") | Character.Inventory[inspect - 1][3].Contains("Ranged Weapon"))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Press E to Equip item");
                        ConsoleKey keyPress = Console.ReadKey(true).Key;
                        if (keyPress == ConsoleKey.E)
                        {
                            if (Character.Inventory[inspect - 1][0][0] == Character.equipped)
                            {
                                Console.WriteLine($"You unequiped the {Character.equipped}");
                                Character.equipped = "Fists";
                                BattleActions.MoveList();
                            }
                            else
                            {
                                Character.equipped = Character.Inventory[inspect - 1][0][0];
                                string Attri = Character.Inventory[inspect - 1][3][0];
                                Console.WriteLine($"Equipped the {Character.Inventory[inspect - 1][0][0]}");
                                BattleActions.MoveList();
                            }                            
                        }
                    }


                }
                else
                {
                    Console.Clear();
                    HUD.MenuSelection(ConsoleKey.I);
                }
            }
        }
    }
    class HUD
    {
        public static void MenuSelection(ConsoleKey Selection)
        {
            Console.Clear();
            switch (Selection)
            {
                case ConsoleKey.I:
                    Console.WriteLine("Player Inventory:");
                    if (Character.Inventory.Count() == 0)
                    {
                        Console.WriteLine("You have no items");
                    }
                    else
                    {
                        int i = 0;
                        foreach (string[][] element in Character.Inventory)
                        {
                            i++;
                            if (element[0][0] == Character.equipped)
                            {
                                Console.Write("(Equipped) ");
                            }
                            Console.WriteLine($"{i}: {element[0][0]}");
                        }
                        Console.WriteLine("      Input the item number to inspect/use...,");
                        Console.WriteLine("      Press D To Drop an Item.");
                        Console.WriteLine("      Press 0 To Leave Menus");

                        ConsoleKey keypress = Console.ReadKey(true).Key;
                        //must choose number 0 - 6 or (D)rop, Us(E) add more to while for more inv space

                        while (keypress != ConsoleKey.D0 & keypress != ConsoleKey.D1 & keypress != ConsoleKey.D2 & keypress
                            != ConsoleKey.D3 & keypress != ConsoleKey.D4 & keypress != ConsoleKey.D5 & keypress != ConsoleKey.D6 & keypress != ConsoleKey.D)
                        {
                            keypress = Console.ReadKey(true).Key;
                        }
                        //INSPECT 1 - 6
                        int keypressint = (int)keypress;
                        while (keypress != ConsoleKey.D & keypress != ConsoleKey.D0)
                        {
                            Menues.InspectItem(keypressint - 48);
                            Console.ReadKey();
                            HUD.MenuSelection(ConsoleKey.I);
                            break;
                        }
                        //DROP D
                        if (keypress == ConsoleKey.D)
                        {
                            Console.WriteLine("Select the item number to drop it.");
                            keypress = Console.ReadKey(true).Key;

                            while (keypress != ConsoleKey.D1 & keypress != ConsoleKey.D2 & keypress
                            != ConsoleKey.D3 & keypress != ConsoleKey.D4 & keypress != ConsoleKey.D5 & keypress != ConsoleKey.D6)
                            {
                                //Drop item 1-6
                                keypress = Console.ReadKey(true).Key;
                            }
                            int keyPressDropInt = (int)keypress - 48; //returns int 0-5
                            if (keyPressDropInt > 0 & keyPressDropInt <= Character.Inventory.Count()) //reject negatives and numbers larger than max inv
                            {
                                Console.WriteLine();
                                Console.WriteLine("                  Are you sure?   Y to Drop, N to Cancel");
                                var confirm = Console.ReadKey(true).Key;
                                while (confirm != ConsoleKey.Y & confirm != ConsoleKey.N)
                                {
                                    confirm = Console.ReadKey(true).Key;
                                }
                                if (confirm == ConsoleKey.Y)
                                {
                                    Program.BeliDrop(keyPressDropInt - 1);
                                }
                            }
                            break;
                        }
                        //CANCEL
                        if (keypress == ConsoleKey.D0)
                        {
                            break;
                        }
                    }
                    break;
                case ConsoleKey.P:
                    Console.WriteLine("Player Stats");
                    Character.DisplayStats();
                    break;
                case ConsoleKey.H:
                    Console.WriteLine("Press I for (I)nventory, Press P for (P)layer Stats, Press Escape to Return");
                    MenuSelection((Console.ReadKey(true)).Key);
                    break;
                case ConsoleKey.Escape:
                    break;
                default:
                    break;
            }

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
            Console.WriteLine(@"                                          ______ _          _____ _          _            ");
            Console.WriteLine(@"                                          |  ___(_)        /  ___| |        (_)           ");
            Console.WriteLine(@"                                          | |_   _ _ __ ___\ `--.| |__  _ __ _ _ __   ___ ");
            Console.WriteLine(@"                                          |  _| | | '__/ _ \`--. \ '_ \| '__| | '_ \ / _ \");
            Console.WriteLine(@"                                          | |   | | | |  __/\__/ / | | | |  | | | | |  __/");
            Console.WriteLine(@"                                          \_|   |_|_|  \___\____/|_| |_|_|  |_|_| |_|\___|");
            Console.WriteLine("                                                                                 -Darron C.");
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
        public static void BeliDrop(int item)
        {
            //this should be called in the MenuSelection menu
            Story.ColorChanger(ConsoleColor.Blue, $"{Character.Inventory[item][0]} Dropped from inventory");
            //later can implement a system to have the dropped item exist in game world.
            Character.Inventory.RemoveAt(item);
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
        public static string[] ItemGetByAttribute(string[] Attribute) //Searches inventory by attribute and returns the name string
        {
            List<string> returnSearch = new List<string>();

            foreach (string[][] item in Character.Inventory) //Check For a "Weapon Attribute" 
            {
                if (Array.IndexOf(item[3], Attribute) >= 0)
                {
                    returnSearch.Add(item[0][0]);
                    string[] ArraySearch = returnSearch.ToArray();
                    return ArraySearch;
                }
            }
            return null;
        }
        public static void DevActions()
        {
            Console.WriteLine("Here be the testing envoirnment");
            Story.Continue(0);
            maxInventory = 3;
            BeliAdd(new string[] { "Wooden Club" }, new string[] { "A piece of furnishing was part of a chair." }, new string[] { "2", "3" }, new string[] { Program.attrilist[1], Program.attrilist[7] }, new string[] { "15" }, null);
            BeliAdd(new string[] { "Knife" }, new string[] { "A small rusted blade." }, new string[] { "3", "4" }, new string[] { Program.attrilist[1], Program.attrilist[6] }, new string[] { "12" }, null);
            BeliAdd(new string[] { "God Blade" }, new string[] { "A shining 'S' Word." }, new string[] { "12", "13" }, new string[] { Program.attrilist[1], Program.attrilist[6] }, new string[] { "999" }, null);
            BeliAdd(new string[] { "God Blade2" }, new string[] { "A shining 'S' Word." }, new string[] { "12", "13" }, new string[] { Program.attrilist[1], Program.attrilist[6] }, new string[] { "999" }, null);

            Character.equipped = "Knife";
            Entities ManBat = new Entities
            {
                Name = "Grotesque Bat",
                movelist = new string[] { "Hunt", "Bite", "Overwhelm", "Bulwark" }
            
            };

            isInBattle = true;
            BattleActions.MoveList();
            Battle(ManBat);
        }

        static void Battle(Entities Enemy) //give a entity when calling
        {
            Console.WriteLine($"You've engaged in combat with the {Enemy.Name}");
            Console.WriteLine("                        Kill Or Be Killed.");
            int turncounter = 0; //use this to control buffs that last longer than 1 turn

            while (isInBattle == true)
            {
                Console.WriteLine("Health: {0}", Character.hpFraction);
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
                var keypress = Console.ReadKey(true).Key;

                while (keypress != ConsoleKey.D1 & keypress != ConsoleKey.D2 & keypress != ConsoleKey.D3 & keypress
                            != ConsoleKey.H & keypress != ConsoleKey.I & keypress != ConsoleKey.P & keypress != ConsoleKey.Enter)
                {
                    keypress = Console.ReadKey(true).Key;
                }
                switch (keypress)//add logic to add move acceptable keypresses if there are more than 3 actions for a weapon
                {
                    case ConsoleKey.D1:
                        BattleActions.ActstoActions(Character.battleActs[0]);
                        break;                        
                    case ConsoleKey.D2:
                        BattleActions.ActstoActions(Character.battleActs[1]);
                        break;
                    case ConsoleKey.D3:
                        BattleActions.ActstoActions(Character.battleActs[2]);
                        break;
                    default:
                        HUD.MenuSelection(keypress);
                        continue;                        

                }
                //remove enemy buffs from last turn
                //AttackList.RemoveAllEnemyBuffs(Enemy);
                               


                if (Enemy.HealthPoints < 0)
                {
                    isInBattle = false;
                    Console.WriteLine("You've slain the creature.");
                }
                Thread.Sleep(100);
                //Enemy Turn
                var EnemyAttack = Enemy.movelist[diceroll.Next(0, Enemy.movelist.Count())];

                //AttackList
                if (EnemyAttack == "Bite")
                {
                    AttackList.Bite(Enemy);
                    if (BattleActions.parryState == true)
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
                    if (BattleActions.parryState == true)
                    {

                    }
                }
             


                //remove character buffs

                if (Character.currentHealth < 0)
                {
                    isInBattle = false;
                    Console.WriteLine("You Are Dead.");
                    Story.Continue(0);
                    Environment.Exit(1);
                    //Trigger PlayDeath()
                }
            }

        }
        public static void AttackToMethod()
        {

        }
    }
}
