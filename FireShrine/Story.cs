using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FireShrine
{    
    class Story
    {
        static bool storysent = false;
        public static string Chapter;

        public static void Continue(int speed)
        {
            if (speed == 0)
            {
                var keypress = Console.ReadKey(true).Key;
                while(keypress != ConsoleKey.Enter)
                {
                    keypress = Console.ReadKey(true).Key;
                }
                Console.WriteLine("...");
            }
            else
            {
                Console.WriteLine("...");
            }
        }
        
        public static void JumpToStory()
        {
            if (Chapter == "01")
            {
                Story01();
            }
            if (Chapter == "02")
            {
                Story02();
            }
            if (Chapter == "02a")
            {
                Story02a();
            }
            if (Chapter == "02b")
            {
                Story02b();
            }
            if (Chapter == "02c")
            {
                Story02c();
            }
        }
        public static void ColorChanger(ConsoleColor color, string Text)
        {
            ConsoleColor currentcolor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(Text);
            Console.ForegroundColor = currentcolor;
        }
        public static void Story01()
        {
            Story.Chapter = "01";
            if (Story.storysent == false)
            {
                //textwidth must be 120 characers across for proper formatting
                Console.WriteLine("You wake from a restless slumber in a room unfamiliar to you. The air is dank and you can see spears of light piercing ");
                Console.WriteLine("through the crumbling marble that were once great walls. The cot beneath you protests as you stir. Searching your");
                Console.WriteLine("surroundings it appears to be a long forgotten room. Disheveled wooden furnishings and a large matted carpet rests upon");
                Console.WriteLine("an uneven floor. Dust, webbings and moss have claimed this place. The floor beneath you groans as you find your footing");
                Console.WriteLine("and you make your way towards the door that now hangs almost completely off its hinges.");
                Console.WriteLine("A huge ornate chandelier dangles precariously and somehow beautifully above as you being to wonder how you wound up");
                Console.WriteLine("here.");
                Console.WriteLine("Press any key to continue");
                Continue(0);
                Console.WriteLine("Under the light fixture, there is a long table with various silverware next to various fruit, some neatly placed " +
                    "in bowls and other strewn about, staining the tablecloth and littering the floor. Your eye rests upon a decently sized carving knife " +
                    "near the center next to a huge peice of rotting meat. Closer to you, is a " +
                    "broken chair with a piece that could also be used to defend yourself. Wanting to keep a free hand you decide to carry one of these items"); ;
                Console.WriteLine("Would you prefer the wooden Club or the rusty Knife?");
                storysent = true;
                Console.WriteLine();
                Console.WriteLine("Press 'H' To review menu options. You can utilize this whenever you presented with a Choice");
                Console.WriteLine();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Under the light fixture, there is a long table with various silverware next to rotting fruit, some neatly placed " +
                    "in bowls and other strewn about on the floor. Your eye rests upon a decently sized carving knife. Closer to you, is a large " +
                    "chair in parts with a piece from it resembling a club. Wanting to keep a free hand you decide to carry one of these items");
                Console.WriteLine("Would you prefer the wooden Club or the rusty Knife?");
                Console.WriteLine();
            }
            int Selection = Menues.ChoiceSelection("Club", "Knife", null, null, null);
            switch (Selection)
            {
                case 1:
                    Console.WriteLine("You Picked up the improvised Club");
                    Program.BeliAdd(new string[] { "Wooden Club" }, new string[] { "A piece of furnishing was part of a chair." }, new string[] { "2", "3" }, new string[] { Program.attrilist[1] }, new string[] { "15" }, null);
                    break;
                case 2:
                    Console.WriteLine("You Grabbed The Knife From the dinner table");
                    Program.BeliAdd(new string[] { "Knife" }, new string[] { "A small rusted blade." }, new string[] { "3", "4" }, new string[] { Program.attrilist[1], Program.attrilist[6] }, new string[] { "12" }, null);
                    break;

            }
            //reset the storysent variable at the end of chapter
            //storysent = false;
            //Continue the Story

            Story02();

        }
        public static void Story02()
        {

            Chapter = "02";
            if (storysent == false)
            {
                if (Character.Inventory[0][0].Contains("Knife"))
                {
                    Console.WriteLine("The blade shows signs of rust but is still sharp enough.");
                    Continue(0);
                }
                else
                {
                    Console.WriteLine("The club is heafty but well balanced in your hand.");
                    Continue(0);
                }
                Console.WriteLine();

                Console.WriteLine("You try to recall how you ended up here, but it fails you. The only images that flash through your mind do not" +
                    " explain where you are and have no correlation with each other. A train, a door, and a computer or a machine of sorts. A sense " +
                    "of urgency begins to rise within just before a hollow knock echoes through these old walls.");
                Continue(0);
                Console.WriteLine("What will you do?");
                storysent = true;
                int Selection = Menues.ChoiceSelection("Search the dinner room.", "Move to the end of the room toward the grand staircase.", "Attempt to locate the direction of the noise.", null, null);
                switch (Selection)
                {
                    case 1:
                        Console.WriteLine("It seems this space was used to convene many, assumingly wealthy individuals. But any proof that people still reside in this desolate place is lost to you. ");
                        storysent = false;
                        Story02a();
                        break;
                    case 2:
                        Console.WriteLine("You approach grand staircase, marveling at its sheer magnitude and architecture. As you near the head of the stairs you peer over the ledge and feel " +
                            "mild vertigo. The two arms of the staircase wrap around the circular room to the ground below where lies nothing but more, albeit lesser ruin. That is when you notice several motionless figures on the steps. People.");
                        Story02b();
                        break;
                    case 3:
                        Console.WriteLine("3");
                        Story02c();
                        break;
                    default:
                        break;
                }

                Continue(0);
            }
            else
            {
                Console.WriteLine("You try to recall how you ended up here, but it fails you. The only images that flash through your mind do not" +
                    " explain where you are and have no correlation with each other. A train, a door, and a computer or a machine of sorts. A sense " +
                    "of urgency begins to rise within just before a hollow knock echoes through these old walls.");
                Continue(0);

                Console.WriteLine();
                Console.WriteLine("What will you do?");
                int Selection = Menues.ChoiceSelection("Search the dinner Room.", "Move to the end of the room toward the large stairs.", "Attempt to locate the direction of the noise.", null, null);
                switch (Selection)
                {
                    case 1:
                        Console.WriteLine("It seems this space was used to convene many, assumingly wealthy individuals. But any proof that people still reside in this desolate place is lost to you.");
                        storysent = false;
                        Story02a();
                        break;
                    case 2:
                        Console.WriteLine("You approach grand staircase, marveling at its sheer magnitude and architecture. As you near the head of the stairs you peer over the ledge and feel " +
                            "mild vertigo. The two arms of the staircase wrap around the circular room to the ground below where lies nothing but more, albeit lesser ruin. That is when you notice several motionless figures on the steps. People.");
                        storysent = false;
                        Story02b();
                        break;
                    case 3:
                        Console.WriteLine("3");
                        storysent = false;
                        Story02c();
                        break;
                    default:
                        break;
                }
            }

        }
        public static void Story02a()
        {
            Chapter = "02a";

            if (storysent == false)
            {
                Continue(0);
                Console.WriteLine("You move around the table searching for anything can give you some inclination of what happened here. There must indeed have been some sudden urgency from a group but the absence of weapons, blood, or corpses makes you wonder what " +
                    "could have made these people leave in such a hurry. Your eyes then rest upon the meals on the table; there lies an arrangement of spices, herb, grain and braised meat on each plate, some neat and untouched and some knocked over and spilled.");
                Continue(0);
                Console.WriteLine("In the center a small pig roast that was half done into exposing rib and spine. Rot had set into most of the meals but upon closer inspection you see that the food couldn’t have been left rotting for more than a couple days. Several untouched apples sit in a" +
                " bowl next to the roast and they seem safe enough to eat, so that is in fact what you do.");
                Console.WriteLine("You also shove one into your pocket.");


                Continue(0);
                ColorChanger(ConsoleColor.Cyan, "Gain Health and Fullness from eating");
                Character.currentHealth++;
                Character.currentHunger++;
                Program.BeliAdd(new string[] { "Apple" }, new string[] { "It isn't crisp, but it'll do" }, new string[] { "1", "1", }, new string[] { Program.attrilist[7] }, new string[] { "1" }, null);
                //for edible items the dam range corresponds to lower number hunger, upper number thirst, the health restored or removed refers to dura.
                //Menues.ChoiceSelection("Dev Test", "Dev Test", "Dev Test", null, null);
            }
            else
            {

            }
        }
        public static void Story02b()
        {
            Chapter = "02b";

            if (storysent == false)
            {
                Continue(0);
                foreach (string[][] item in Character.Inventory) //Check For a "Weapon Attribute" 
                {
                    if (Array.IndexOf(item[3], "Melee Weapon") >= 0 | Array.IndexOf(item[3], "Ranged Weapon") >= 0)
                    {
                        Console.WriteLine("Instinctively, you sprint down the stairs toward them clutching your {0} close. You may have use for it.", item[0][0]);
                    }
                    else
                    {
                        Console.WriteLine("Instinctively, you sprint down the stairs toward them.");
                    }

                }
                Continue(0);
                Console.WriteLine("The bodies are motionless but what is more unsettling is the way they are dressed. " +
                    "Kevlar body armor, tactical helmets, and various broken devices. A hexagonal " +
                    "emblem on the shoulder with the text “C.C.” are on each of their uniforms but you don’t recognize the division or outfit. ");
                Console.WriteLine("A rucksack lay near one of the soldiers. A dark-haired woman who seemed to have been desperately reaching for something" +
                    " before she perished. Picking up the bag to search its contents you notice to your dismay its lightness. It’s empty. What ever she had been looking for in her last moments is gone.");
                Continue(0);
                ColorChanger(ConsoleColor.Blue, "Inventory Capacity increased to 6.");
                Console.WriteLine("You decide to keep the backpack for yourself when a sickening sound steals focus toward the floor below.");
                Continue(0);
                Console.Clear();
                Console.WriteLine("A mix of a horrible clicking and guttural breath hisses nearby as a hulking figure laboriously moves at the far side of the dimly lit foyer." +
                    " Its lumbered steps are slow and thundering, two beady points of light pierce through the dark.");
                Continue(0);
                Console.WriteLine("It lurks about, as if searching. Dread looms over you, but it has not noticed you yet.");
                Console.WriteLine("A gleam catches your eye near the base of the stairs. A pistol. It must have fallen from of the soldiers. Unfortunately for you, a partially destroyed sun roof above illuminates the center of the vestibule." +
                    " The creature will certainly see you if you make a break for the weapon. You can also stealthily backtrack up the stairs to figure out how to avoid the creature.");
                Continue(0);
                Console.WriteLine();
                Console.WriteLine("Decide.");
                storysent = true;
                int Selection = Menues.ChoiceSelection("Sprint for the gun", "Carefully sneak back to the dinner room.", null, null, null);
                switch (Selection)
                {
                    case 1:
                        Console.WriteLine("");
                        break;
                    case 2:
                        Console.WriteLine("");
                        break;
                    default:
                        break;
                }
            }
            else
            {

            }
        }
        public static void Story02c()
        {
            Chapter = "02c";

            if (storysent == false)
            {

            }
        }
    }
}
