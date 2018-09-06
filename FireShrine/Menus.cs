using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireShrine
{
    class Menus
    {
        static int chooseAmount;
        static int TheChoice { get; set; }
        static string[] Choiceref { get; set; }
        static int keytoint;
        static bool invAccessed = false;
        static bool leaver = false;
        static ConsoleKeyInfo keyPress;
        public static ConsoleKey ChoiceSelection(string[] choices)
        {
            Choiceref = choices;
            chooseAmount = choices.Count();
            int i = 1;
            foreach (var item in choices) //display choices
            {
                Console.WriteLine($"{i}): {item}");
                i++;
            }            
            while (keytoint > chooseAmount | keytoint == 0)
            {
                keyPress = Console.ReadKey(true);
                if (char.IsDigit(keyPress.KeyChar))
                {
                    keytoint = int.Parse(keyPress.KeyChar.ToString());
                }
                else
                {
                    if (keyPress.Key == ConsoleKey.I | keyPress.Key == ConsoleKey.P | keyPress.Key == ConsoleKey.H)
                    {
                        MenuSelection(keyPress.Key);
                    }
                    keytoint = 0;

                }
            }
            return keyPress.Key;
        }
        public static void MenuSelection(ConsoleKey Selection)
        {
            switch (Selection)
            {
                case ConsoleKey.I:
                    invAccessed = true;
                    Console.Clear();
                    Console.WriteLine("Player Inventory:");
                    if (Character.Inventory2.Count() == 0)
                    {
                        Console.WriteLine("You have no items");
                    }
                    else
                    {
                        int i = 0;
                        foreach (IItems element in Character.Inventory2)
                        {
                            i++;
                            if (element.Name == Character.equipped)
                            {
                                Console.Write("(Equipped) ");
                            }
                            Console.WriteLine($"{i}: {element.Name}");
                        }
                        Console.WriteLine("      Input the item number to inspect/use...,");
                        Console.WriteLine("      Press D To Drop an Item.");
                        Console.WriteLine("      Press Esc To Leave Menus");

                        //must choose number 0 - 6 or (D)rop, Us(E) add more to while for more inv space
                        while (keytoint > Character.Inventory2.Count() | keytoint == 0)
                        {
                            keyPress = Console.ReadKey(true);
                            if (char.IsDigit(keyPress.KeyChar)) //is the char a digit? convert it to a int
                            {
                                keytoint = int.Parse(keyPress.KeyChar.ToString());
                                //display itemsstats
                                if (keytoint <= Character.Inventory2.Count() & keytoint != 0)
                                {
                                    Character.Inventory2[keytoint - 1].Inspect();
                                }
                            }
                            else
                            {
                                //drop and leave
                                if (keyPress.Key == ConsoleKey.D) //drop
                                {
                                    Console.WriteLine("Select the item number to drop it. Press Escape to Exit.");
                                    while (keytoint > Character.Inventory2.Count() | keytoint == 0)
                                    {
                                        keyPress = Console.ReadKey();
                                        if (char.IsDigit(keyPress.KeyChar))
                                        {
                                            keytoint = int.Parse(keyPress.KeyChar.ToString());
                                        }
                                        if (keyPress.Key == ConsoleKey.Escape)
                                        {
                                            leaver = true;
                                            break;
                                        }
                                    }
                                    if (!leaver)
                                    {
                                        Console.WriteLine("                  Are you sure?   Y to Drop, N to Cancel");
                                        var confirm = Console.ReadKey(true).Key;
                                        while (confirm != ConsoleKey.Y & confirm != ConsoleKey.N)
                                        {
                                            confirm = Console.ReadKey(true).Key;
                                        }
                                        if (confirm == ConsoleKey.Y)
                                        {
                                            Character.Inventory2[keytoint - 1].Drop();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Canceled.");
                                            break;
                                        }
                                    }
                                                                        
                                }
                                if (keyPress.Key == ConsoleKey.Escape) //leave
                                {
                                    break;
                                }
                                keytoint = 0;
                            }
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
                case ConsoleKey.Enter:
                    break;
                default:
                    break;
            }
            //Here it should clear and go back to Items list
            keytoint = 0;
            leaver = false;
            if (invAccessed == true)
            {
                invAccessed = false;
                Story.Continue(0);
                MenuSelection(ConsoleKey.I);
            }
            else
            {
                //jump to chapter:
                ChoiceSelection(Choiceref);
            }
        }

    }
}
