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
        static int keytoint;
        static int invcountcomparer;
        static ConsoleKeyInfo keyPress;
        public static ConsoleKey ChoiceSelection(string[] choices)
        {
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
                    MenuSelection(keyPress.Key);
                    //switch (keyPress.Key)
                    //{
                    //    case ConsoleKey.I:
                    //        break;
                    //    case ConsoleKey.P:
                    //        break;
                    //    case ConsoleKey.H:
                    //        break;
                    //    default:
                    //        break;
                    //}
                    
                    keytoint = 0;
                }
            }
            return keyPress.Key;
        }
        public static void MenuSelection(ConsoleKey Selection)
        {
            Console.Clear();
            switch (Selection)
            {
                case ConsoleKey.I:
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
                        Console.WriteLine("      Press 0 To Leave Menus");

                        //must choose number 0 - 6 or (D)rop, Us(E) add more to while for more inv space
                        while (keytoint > Character.Inventory2.Count())
                        {
                            keyPress = Console.ReadKey(true);
                            if (char.IsDigit(keyPress.KeyChar))
                            {
                                keytoint = int.Parse(keyPress.KeyChar.ToString());
                            }
                            else
                            {
                                //drop and leave
                                if (keyPress.Key == ConsoleKey.D) //drop
                                {
                                    Console.WriteLine("Select the item number to drop it.");
                                    while (keytoint > Character.Inventory2.Count())
                                    {
                                        keyPress = Console.ReadKey(true);
                                        if (char.IsDigit(keyPress.KeyChar))
                                        {
                                            keytoint = int.Parse(keyPress.KeyChar.ToString());
                                        }
                                    }
                                    Character.Inventory2[keytoint - 1].Drop();
                                }
                                if (keyPress.Key == ConsoleKey.D0) //leave
                                {
                                    break;
                                }
                                keytoint = 0;
                            }
                        }
                        
                        //INSPECT 1 - 6
                        while (keyPress != ConsoleKey.D & keyPress != ConsoleKey.D0)
                        {
                            Menues.InspectItem(keypressint - 48);
                            Console.ReadKey();
                            HUD.MenuSelection(ConsoleKey.I);
                            break;
                        }
                        //DROP D
                        if (keyPress == ConsoleKey.D)
                        {
                            Console.WriteLine("Select the item number to drop it.");
                            keyPress = Console.ReadKey(true).Key;

                            while (keyPress != ConsoleKey.D1 & keyPress != ConsoleKey.D2 & keyPress
                            != ConsoleKey.D3 & keyPress != ConsoleKey.D4 & keyPress != ConsoleKey.D5 & keyPress != ConsoleKey.D6)
                            {
                                //Drop item 1-6
                                keyPress = Console.ReadKey(true).Key;
                            }
                            int keyPressDropInt = (int)keyPress - 48; //returns int 0-5
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
                        if (keyPress == ConsoleKey.D0)
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
}
