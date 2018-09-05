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
        static int why;
        static ConsoleKeyInfo keyPress;
        public static ConsoleKey ChoiceSelection(string[] choices)
        {
            chooseAmount = choices.Count();
            int i = 1;
            foreach (var item in choices)
            {
                Console.WriteLine($"{i}): {item}");
                i++;
            }
            if (keyPress.Key == ConsoleKey.H)
            {

            }
            while (why > chooseAmount | why == 0)
            {
                keyPress = Console.ReadKey(true);
                if (char.IsDigit(keyPress.KeyChar))
                {
                    why = int.Parse(keyPress.KeyChar.ToString());
                }
                else
                {
                    why = 0;
                }
            }
            return keyPress.Key;
        }
    }
}
