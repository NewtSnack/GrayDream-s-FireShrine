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
        static int Selection = 0;
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
        
        public static void JumpToStory() //story broker
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
            if (Chapter == "02ab")
            {
                Story02ab();
            }
            if (Chapter == "02bb")
            {
                Story02bb();
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
                Console.WriteLine("a wooden floor. Dust, webbings and moss have claimed this place. The floor beneath you groans as you find your footing");
                Console.WriteLine("and you make your way towards the door that now hangs almost completely off its hinges.");
                Console.WriteLine("A huge ornate chandelier dangles precariously and somehow beautifully above as you being to wonder how you wound up");
                Console.WriteLine("here.");
                Console.WriteLine("Press Enter to continue");
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
                    Program.BeliAdd(new string[] { "Wooden Club" }, new string[] { "A piece of furnishing was part of a chair." }, new string[] { "2", "3" }, new string[] { Program.attrilist[1], Program.attrilist[7] }, new string[] { "15" }, null);
                    break;
                case 2:
                    Console.WriteLine("You Grabbed The Knife From the dinner table");
                    Program.BeliAdd(new string[] { "Knife" }, new string[] { "A small rusted blade." }, new string[] { "3", "4" }, new string[] { Program.attrilist[1], Program.attrilist[6] }, new string[] { "12" }, null);
                    break;

            }
            //reset the storysent variable at the end of chapter
            storysent = false;
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
                    " explain where you are and have no correlation with each other. A machine being one of them. A sense " +
                    "of urgency begins to rise within you when a hollow knock echoes through these old walls.");
                Continue(0);
                Console.WriteLine("What will you do?");
                storysent = true;
                Selection = Menues.ChoiceSelection("Search the dinner room.", "Move to the end of the room toward the grand staircase.", "Attempt to locate the direction of the noise.", null, null);
               
            }
            else
            {
                Console.WriteLine("You try to recall how you ended up here, but it fails you. The only images that flash through your mind do not" +
                    " explain where you are and have no correlation with each other. A machine being one of them. A sense " +
                    "of urgency begins to rise within you when a hollow knock echoes through these old walls.");
                Continue(1);

                Console.WriteLine();
                Console.WriteLine("What will you do?");
                Selection = Menues.ChoiceSelection("Search the dinner Room.", "Move to the end of the room toward the large stairs.", "Attempt to locate the direction of the noise.", null, null);

            }
            switch (Selection)
            {
                case 1:
                    Console.WriteLine("It seems this space was used to convene many, assumingly wealthy individuals. But any proof that people still reside in this desolate place is lost to you. ");
                    storysent = false;
                    Story02a();
                    break;
                case 2:
                    Console.WriteLine("You approach grand staircase, marveling at its sheer magnitude and architecture. As you near the head of the stairs you peer over the broken ledge and " +
                        "mild vertigo sets deep into your gut. The two arms of the staircase wrap around the circular chamber to the ground below where lies nothing but more ruin. That is when you notice several motionless figures on the steps. People.");
                    storysent = false;
                    Story02b();
                    break;
                case 3:
                    Console.WriteLine("You take a knee in response to the knock, waiting for it to happen again. Time feels to slow to a halt as you listen to the still silence and gentle wind grooming the crumbling ramparts.");
                    storysent = false;
                    Story02c();
                    break;
                default:
                    break;
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
                Program.BeliAdd(new string[] { "Apple" }, new string[] { "It isn't crisp, but it'll do" }, new string[] { "1", "1", }, new string[] { Program.attrilist[8] }, new string[] { "1" }, null);
                //for edible items the dam range corresponds to lower number hunger, upper number thirst, the health restored or removed refers to dura.
                Continue(0);
                Console.WriteLine("As you finish, you hear another sound. Rhythmic. Closer than before. It sounds like footsteps, too massive to belong to a person. The thumping, the only sound in this" +
                    " derelict building, as if it was its old beating heart. It’s coming from the somewhere off the bottom of the grand staircase at the other end of the room. And it’s coming this way.");
                storysent = true;
                Selection = Menues.ChoiceSelection("You know no fear, Attack", "Find a place to hide", null, null, null);
            }
            else
            {
                Console.WriteLine("As you finish, you hear another sound. Rhythmic. Closer than before. It sounds like footsteps, too massive to belong to a person. The thumping, the only sound in this" +
                   " derelict building, as if it was its old beating heart. It’s coming from the somewhere off the bottom of the grand staircase at the other end of the room. And it’s coming this way.");
                Selection = Menues.ChoiceSelection("You know no fear, Attack", "Find a place to hide", null, null, null);

            }
            switch (Selection)
            {
                case 1:
                    storysent = false;
                    Story02aa();
                    break;
                case 2:
                    storysent = false;
                    Story02ab();
                    break;
                default:
                    break;
            }
        }
        public static void Story02aa()
        {
            Chapter = "02aa";
            Console.WriteLine($"You decide it’s best to face whatever is coming head on. With your {Character.equipped} ready, you sprint toward the head of the stairs. Right as you near the top, a large furry bat-like creature" +
                $" sees you and lets out a horrifying screech. You do not break stride as you courageously leap to strike the creature.");
            Entities ManBat = new Entities
            {
                Name = "Grotesque Bat",
                movelist = new string[] { "Hunt", "Bite", "Overwhelm", "Bulwark" }
            };

            Program.isInBattle = true;
            BattleActions.MoveList();
            Program.Battle(ManBat);

        }
        public static void Story02ab()
        {
            Chapter = "02ab";

            if (storysent == false)
            {
                Console.WriteLine("You immediately search for a place to hide. You spot several over turned chairs and tables in a corner near a door to another room. It seems that would provide good cover. You carefully move toward" +
                    " it careful not to knock anything over or make any noise. Gingerly moving between the debris to find a spot to hide, dust fall onto your face and you bring a hand to shield your airways. This would be a terrible time" +
                    " for a sneeze. Just as you consider bolstering the veil of chairs and trash around you, the beast appears at the top of the stairs at the other end of the room.");
                Continue(0);
                Console.WriteLine("The first thing that steals your attention are the two beady eyes that glow with menace in the dark, then you see the oddly long ears that protrude off the top of its head. It drags its feet lazily leaving" +
                    " deep scratches into the wood and has ferocious arms attached to wings that couldn’t possibly lift this creature. The wings are dragged along the floor as it moves sniffing the room and its ragged breath sets a pit" +
                    " in your gut. ");
                Continue(0);
                Console.WriteLine("It moves closer but is oblivious to you. It moves less carefully before as if no longer stalking, and heads toward the door near you. You could use this opportunity to attack or you could let the beast pass.");
                storysent = true;
                Selection = Menues.ChoiceSelection("Attack", "Remain Hidden.", null, null, null);
            }
            else
            {
                Console.WriteLine("It moves closer but is oblivious to you. It moves less carefully before as if no longer stalking, and heads toward the door near you. You could use this opportunity to attack or you could let the beast pass.");
                Selection = Menues.ChoiceSelection("Attack", "Remain Hidden.", null, null, null);
            }
            switch (Selection)
            {
                case 1:
                    storysent = false;
                    Story02bba();
                    break;
                case 2:
                    storysent = false;
                    Story02bbb();
                    break;
                default:
                    break;
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
                Program.maxInventory = 6;
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
                Selection = Menues.ChoiceSelection("Sprint for the gun", "Carefully sneak back to the dinner room.", null, null, null);
                
            }
            else
            {
                Console.WriteLine("A gleam catches your eye near the base of the stairs. A pistol. It must have fallen from of the soldiers. Unfortunately for you, a partially destroyed sun roof above illuminates the center of the vestibule." +
                    " The creature will certainly see you if you make a break for the weapon. You can also stealthily backtrack up the stairs to figure out how to avoid the creature.");
                Continue(0);
                Console.WriteLine();
                Console.WriteLine("Decide.");
                Selection = Menues.ChoiceSelection("Sprint for the gun", "Carefully sneak back to the dinner room.", null, null, null);
            }
            switch (Selection)
            {
                case 1:
                    storysent = false;
                    Story02ba();
                    break;
                case 2:
                    storysent = false;
                    Story02bb();
                    break;
                default:
                    break;
            }
        }
        public static void Story02ba()
        {
            Chapter = "02ba";
            if (storysent == false)
            {
                Console.WriteLine("You make a break for the gun, the beast notices you immediately and bellows a horrid sound that is a mix between a screech and a thunderous roar. You focus on swooping downwards." +
                    " All your attention is on getting to this handgun before the beast gets to you. The weapon is almost to you now and from the corner of your eye a blur of or a dark-colored fur bursts from the shade. Just a few more steps…");
                Continue(0);
                Console.WriteLine("In a moment that feels like a rapid eternity, you lunge to the pistol before the few remaining steps and use the momentum to swiftly grab the gun and into a roll to line the shot from the floor." +
                    " The beast is less than 10 meters away now and the first thing you realize is the deformed face. Awkwardly tiny eyes, and huge teeth bared and sharp.");
                Continue(0);
                Console.WriteLine("It’s like nothing you’ve ever seen. And soon it would be like nothing you’ve ever killed. You squeeze the trigger.");
                Continue(0);
                Console.Clear();
                Console.WriteLine("To your horror, it does nothing but something within you clicks. The beach lunges with its winged arms outstretched, as you immediately roll to the side." +
                    " It crashes into the ground where you just were and as you swivel to your feet your hands move on their own. You disengage the safety, cock the gun and as a round ejects from the slide, " +
                    "three shots rang out before the monster swings and it connects sending you into a wall.");
                Continue(0);
                Console.WriteLine("The beast screeches in pain as blood pours from the wounds, splattering the ground. Winded, you stand up using some of the debris around you to steady yourself. It was a hard hit but nothing fatal." +
                    " The beast squares you up, and you see that most of its frame comes from the large tattered wings attached to large muscular arms that ended in claws. It has a rodent-like face and body covered it short fur. Its " +
                    "legs were long and strange as if not meant for running, and its most defining feature was its two gigantic ears that pointed toward the shattered sunroof, illuminated by the pale light. It dawns on you that this" +
                    " beast has similar features to a bat, despite being over 6 feet tall and humanoid.");
                Continue(0);
                Console.WriteLine("Another guttural screech as phlegm and salvia expels from between razor sharp teeth, and it charges. ");
                Continue(0);
                ColorChanger(ConsoleColor.Red, "You've suffered 2 damage");
                ColorChanger(ConsoleColor.Green, "The monster has suffered 3 damage.");

                Character.currentHealth = Character.currentHealth - 2;
                Program.BeliAdd(new string[] { "M9 Beretta" }, new string[] { "A Semi-automatic Pistol." }, new string[] { "8", "9" }, new string[] { Program.attrilist[2] }, new string[] { "12" }, new string[] { "8" });
                Entities ManBat = new Entities
                {
                    Name = "Grotesque Bat",
                    movelist = new string[] { "Hunt", "Bite", "Overwhelm", "Bulwark" },
                    HealthPoints = 17
                };

                Program.isInBattle = true;
                BattleActions.MoveList();
                Program.Battle(ManBat);
            }
        }
        public static void Story02bb() //HIDE primary
        {
            Chapter = "02bb";
            if (storysent == false)
            {
                Console.WriteLine("Not wanting to risk a confrontation with whatever it is in front you, you quietly move back from the stairs all while keeping your gaze transfixed on the figure hidden in the dark. It knocks over a small" +
                    " table just as you creep away from its line of sight.");
                Continue(0);
                Console.WriteLine("You immediately search for a place to hide. You spot several over turned chairs and tables in a corner near a door to another room. It seems that would provide good cover. You carefully move toward" +
                    " it careful not to knock anything over or make any noise. Gingerly moving between the debris to find a spot to hide, dust fall onto your face and you bring a hand to shield your airways. This would be a terrible time" +
                    " for a sneeze. Just as you consider bolstering the veil of chairs and trash around you, the beast appears at the top of the stairs at the other end of the room.");
                Continue(0);
                Console.WriteLine("The first thing that steals your attention are the two beady eyes that glow with menace in the dark, then you see the oddly long ears that protrude off the top of its head. It drags its feet lazily leaving" +
                    " deep scratches into the wood and has ferocious arms attached to wings that couldn’t possibly lift this creature. The wings are dragged along the floor as it moves sniffing the room and its ragged breath sets a pit" +
                    " in your gut. ");
                Continue(0);
                Console.WriteLine("It moves closer but is oblivious to you. It moves less carefully before as if no longer stalking, and heads toward the door near you. You could use this opportunity to attack or you could let the beast pass.");
                storysent = true;
                Selection = Menues.ChoiceSelection("Attack", "Remain Hidden.", null, null, null);

            }
            else
            {
                Console.WriteLine("It moves closer but is oblivious to you. It moves less carefully before as if no longer stalking, and heads toward the door near you. You could use this opportunity to attack or you could let the beast pass.");
                Selection = Menues.ChoiceSelection("Attack", "Remain Hidden.", null, null, null);

            }
            switch (Selection)
            {
                case 1:
                    storysent = false;
                    Story02bba();
                    break;
                case 2:
                    storysent = false;
                    Story02bbb();
                    break;
                default:
                    break;
            }
        }
        public static void Story02bba()
        {
            Console.WriteLine($"Deftly, you emerge from your concealment with your {Character.equipped} at the ready. You are close enough to smell the reek of this foul creature and see the dark hairs close to its leathery skin.");
            Continue(0);

            if (Character.equipped == "Knife") //later do an attribute check on equipped item. 
            {
                Console.WriteLine("Carefully you close the distance and as soon as you are in range you stab the blade deep into the creature’s back. The beast let’s out an earsplitting screech spins and swings wildly narrowly missing your head. You manage to pull your weapon free and watch as its blood paints the floor. You hit something important. ");
            }
            if (Character.equipped == "Wooden Club")
            {
                Console.WriteLine("Carefully you close the distance and as soon as you are in range you bludgeon the back of the creature’s skull and the beast stumbles forward briefly disoriented and its claws cleave through the air inches from your face. It lets out a screech that you feel in your bones.");

            }
            if (Character.equipped == "Fists")
            {
                Console.WriteLine("Carefully you close the distance and as soon as you are in range you jump and attempt to strangle the creature in a headlock. The monster immediately flails its arms and the stranglehold becomes an " +
                    "effort to not be violently tossed from its back. It crashes dangerously into jagged debris in an effort to drop you but only succeeds in damaging itself. You notice its claws are about to reach you as you brace" +
                    " to leap from its back. You roll when you land prepared to fight right as the beast bellows a deep growl.");
            }
            ColorChanger(ConsoleColor.Green, "Battle Advantage!");
            ColorChanger(ConsoleColor.Green, "The monster has suffered 2 damage.");
            Continue(0);
            Console.WriteLine("The bat creature hunches menacingly, ready to return the favor.");
            Entities ManBat = new Entities
            {
                Name = "Grotesque Bat",
                movelist = new string[] { "Hunt", "Bite", "Overwhelm", "Bulwark" },
                HealthPoints = 18
            };

            Program.isInBattle = true;
            BattleActions.MoveList();
            Program.Battle(ManBat);
        }
        public static void Story02bbb()
        {
            Console.WriteLine("You decide to remain hidden and the beast walks past you toward the door. The creature shoves its mass into the door so suddenly you almost flee, but you do not yield and neither does the door." +
                " It braces to do it again when a very audible ring comes from the floor above. It sounded mechanical. If you heard this then beast definitely had as well. In a blur of fur and wing, the creature turned from the " +
                "door and crashed through the room knocking aside furnishings.");
            Continue(0);
            Console.WriteLine("It got to another staircase that was obscured by broken tables and chairs in a makeshift barricade and leaped, smashing into the top half and crashing onto the other side. In another flash of movement, it vanished up the stairs.");
        }
        public static void Story02c()
        {
            Chapter = "02c";

            if (storysent == false)
            {
                Console.WriteLine("The smell of mold, wood and slightly rotten food hang in the dull air and the occasional avian chirp can be heard in the far distance. This place must truly be derelict." +
                    " A second knocking dispels any preconception you have of inhabitance followed by a dragging sound. You are able to tell that the source is coming from floor beneath you at the bottom of a" +
                    " grand staircase but you are too far away to see down there.");
                Continue(0);
                Console.WriteLine("Stealthily you move towards what’s left of the ledge near the head of the stairs. The dinner room you are in seamlessly opens outward into a circular foyer where an extravagant" +
                    " staircase curves to the ground below on either side. Most of what lies below and beyond remains in darkness partly due to the contrasting column of light in the center of the foyer from" +
                    " a shattered sky roof several floors above. You cautiously observe from your perch remaining close to the ground. You still hear dragging, and from beyond the column of light you can just make" +
                    " out the movement of a figure. It finally moves into view… ");
                Continue(0);
                Console.Clear();
                Console.WriteLine("A mix of horrible clicking and guttural breath hisses as a hulking figure laboriously moves into the center of the foyer. Its lumbered steps are slow and thundering, two beady " +
                    "points of light pierce through the dark before entering the light. It lurks about, as if searching. Dread looms over you, but it has not noticed you yet. You see the oddly long ears that protrude" +
                    " off the top of its head. It drags its feet lazily leaving deep scratches into the wood and has ferocious arms attached to wings that couldn’t possibly lift this creature. The wings are dragged along " +
                    "the floor as it moves sniffing the room and its ragged breath sets a pit in your gut. ");
                Continue(0);
                Console.WriteLine("     You feel uneasy observe this creature for any longer, as if doing so would allow it to somehow sense your presence, so you shrink away from the ledge. Your heart begins pound in" +
                    " your ears as you frantically look around room for anything that can help you. That’s when you notice a pile of debris at the far end of dining room near two large doors to your left.");
                Continue(0);
                Console.WriteLine("You were about to head toward when you remember the chandelier. Observing it, you see it is poorly supported with its pulley system partly destroyed being held in place by a single meager" +
                    " rope tied to another end of the room near a blockade of chairs and tables and an overturned ornate piano. There is just enough cover to remain out of sight at this end of the room, but if the beast comes close it will surely see you." );
                storysent = true;
                Selection = Menues.ChoiceSelection("(Intiative) bait and trap the spot under the hanging chandelier", "Hide", "Move toward the rope", null, null);
                
            }
            else
            {
                Console.WriteLine("You were about to head toward when you remember the chandelier. Observing it, you see it is poorly supported with its pulley system partly destroyed being held in place by a single meager" +
                     " rope tied to another end of the room near a blockade of chairs and tables and an overturned ornate piano. There is just enough cover to remain out of sight at this end of the room, but if the beast comes close it will surely see you.");
                Selection = Menues.ChoiceSelection("(Intiative) bait and trap the spot under the hanging chandelier", "Hide", "Move toward the rope", null, null);
            }
            switch (Selection)
            {
                case 1: // intiative check to select this option, otherwise
                    if (Character.initiative < 3)
                    {
                        Console.WriteLine("You do not have enough Initiaive to select this option");
                        Selection = Menues.ChoiceSelection("Hide", "Move toward the rope", null, null, null);
                        switch (Selection)
                        {
                            case 1:
                                storysent = false;
                                Story02cb();
                                break;
                            case 2:
                                storysent = false;
                                Story02cc();
                                break;
                        }
                        break;
                    }
                    else
                    {
                        storysent = false;
                        Story02ca();
                        break;

                    }

                case 2:
                    storysent = false;
                    Story02cb();
                    break;
                case 3:
                    storysent = false;
                    Story02cc();
                    break;
                default:
                    break;
            }
        }
        private static void Story02ca()//Trapping
        {
            bool Sharpitem = false;
            Chapter = "02ca";
            if (storysent == false)
            {
                Console.WriteLine("The ragged shirt you have on smells of sweat and sticks to your skin. You can’t think of a more perfect bait to hold the creature’s attention.");
                Continue(0);
                Console.Clear();
                Console.WriteLine("Carefully, you move under the gigantic fixture which dangled high into the air almost directly over the center of the huge dinner tabletop. A thump reverberates from the bottom of the staircase, " +
                    "you need to act quickly. The centerpiece roast holds the spot and you worry the rotten stench will mask your own, so you gingerly lift the massive hunk of meat off the table by its giant platter. Luckily for you it was " +
                    "already mostly eaten. You lower it on the ground careful not to drop it or make any noise. You swiftly remove your shirt and drop it in its place as the footsteps are dangerously near the top of the stairs now.");
                Continue(0);
                Console.WriteLine("The beast emerges just as you manage to run to the spot by the rope and out of its sight. It stops suddenly and kneels strangely to the ground, using its beady eyes and long ears to find you. It’s clear" +
                    " that smell is not this beast strongest sense but eventually it catches the scent of your shirt. It swiftly moves on top of the table to investigate.");
                foreach (string[][] item in Character.Inventory)
                {
                    if (item[3].Contains("Sharp"))
                    {
                        Sharpitem = true;                        
                    }
                }
                if (Sharpitem == true)
                {
                    string SharpitemName = Program.ItemGetByAttribute("Sharp");
                    Console.WriteLine($"The beast is in position. You take out your {SharpitemName} and start working on the thick twine. It is harder then you expected and you being to worry that the creature will hear you with its gigantic" +
                            " ears and move out of the way. You grab the rope with your free hand for leverage and begin cutting more desperately now, sweat dropping from your brow. Not yet relenting, you sneak a quick glance at the dinner and with" +
                            " a sudden and loud snap, the rope rips from your hands.");
                    Continue(0);
                    Console.WriteLine("The massive chandelier crashes into the table where the beast was standing, cleaving the great table in half and sending silverware and wreckage in all directions.");
                }
                else
                {
                    Console.WriteLine("The beast is in position. You began to work on untying the rope, it would be easier if you had something sharp to work with. It’s attached to a metal bar jutting from the wall in well-tied knots. You begin to worry that the" +
                        " creature will hear you with its gigantic ears and move out of the way. Your hands begin to ache and skin beings to peel as you use all your strength to undo the thick twine knots.");
                    Continue(0);
                    Console.Clear();
                    Console.WriteLine("Sweat dripping from your brow, you steal a glance from the table and see that two beady eyes staring back at you. It sees you. In an act of desperation, you swing your {blunt weapon} at the metal bar as hard as you can, and" +
                        " it goes flying, pulled up toward the ceiling. In the next instant, a loud crash fills the room, cleaving the great table in half and sending silverware and wreckage in all directions. You leap into cover to avoid getting hit. When the dust " +
                        "clears, you see the beast lay nearby struggling to get to its feet, it managed to avoid a direct hit. You attack while you still have the advantage.");
                    Continue(0);
                    ColorChanger(ConsoleColor.Green, "Battle Advantage!");
                    ColorChanger(ConsoleColor.Green, "Enemy has suffered 5 damage.");

                    Entities ManBat = new Entities
                    {
                        Name = "Grotesque Bat",
                        movelist = new string[] { "Hunt", "Bite", "Overwhelm", "Bulwark" },
                        HealthPoints = 15                        
                    };

                    Program.isInBattle = true;
                    BattleActions.MoveList();
                    Program.Battle(ManBat);
                    Character.initiative = Character.initiative + 1;
                }
                storysent = true;
            }
            else
            {

            }

        }
        private static void Story02cb() //Hiding
        {
            Story02bb(); //goes to this chapter
        }
        private static void Story02cc() //Rope non Initiative
        {
            Chapter = "02cc";
            bool Sharpitem = false;

            if (storysent == false)
            {

                Console.WriteLine("You prepare to untie the rope as the creature walks into view. But without proper bait there is nothing keeping the creature drawn to the center of the room.");
                storysent = true;
                foreach (string[][] item in Character.Inventory)
                {
                    if (item[3].Contains("Sharp"))
                    {
                        Sharpitem = true;
                    }
                }
                if (Sharpitem == true)
                {
                    string SharpitemName = Program.ItemGetByAttribute("Sharp");
                    Console.WriteLine($"The beast is as close to the damage zone as you think it'll ever be. You take out your {SharpitemName} and start working on the thick twine. It is harder then you expected and you being to worry that the creature will hear you with its gigantic" +
                            " ears and move out of the way. You grab the rope with your free hand for leverage and begin cutting more desperately now, sweat dropping from your brow. Not yet relenting, you sneak a quick glance at the dinner and with" +
                            " a sudden and loud snap, the rope rips from your hands.");
                    Continue(0);
                    Console.WriteLine("The massive chandelier crashes into the table near where the beast was standing, cleaving the great table in half and sending silverware and wreckage in all directions as you leap behind the piano to avoid the debris.");
                    Continue(0);
                    Console.WriteLine("It wasn't a direct hit. You sprint to attack while you still have the chance.");
                    ColorChanger(ConsoleColor.Green, "Battle Advantage!");
                    ColorChanger(ConsoleColor.Red, "3 Damage dealt to enemy.");

                    Entities ManBat = new Entities
                    {
                        Name = "Grotesque Bat",
                        movelist = new string[] { "Hunt", "Bite", "Overwhelm", "Bulwark" },
                        HealthPoints = 17
                    };

                    Program.isInBattle = true;
                    BattleActions.MoveList();
                    Program.Battle(ManBat);
                }
                else
                {
                    string BluntitemName = Program.ItemGetByAttribute("Blunt");
                    Console.WriteLine("The beast is as close to the damage zone as you think it'll ever be. You began to work on untying the rope, it would be easier if you had something sharp to work with. It’s attached to a metal bar jutting from the wall in well-tied knots. You begin to worry that the" +
                        " creature will hear you with its gigantic ears and move out of the way. Your hands begin to ache and skin beings to peel as you use all your strength to undo the thick twine knots.");
                    Continue(0);
                    Console.Clear();
                    Console.WriteLine($"Sweat dripping from your brow, you steal a glance from the table and see that two beady eyes staring back at you. It sees you. In an act of desperation, you swing your {BluntitemName} at the metal bar as hard as you can, and" +
                        " it goes flying, pulled up toward the ceiling. In the next instant, a loud crash fills the room, cleaving the great table in half and sending silverware and wreckage in all directions. You leap into cover to avoid getting hit. When the dust " +
                        "clears, you see the beast lay nearby struggling to get to its feet, it wasn't a direct hit. You attack while you still have the advantage.");
                    Continue(0);
                    ColorChanger(ConsoleColor.Green, "Battle Advantage!");
                    ColorChanger(ConsoleColor.Red, "1 Damage dealt to enemy.");

                    Entities ManBat = new Entities
                    {
                        Name = "Grotesque Bat",
                        movelist = new string[] { "Hunt", "Bite", "Overwhelm", "Bulwark" },
                        HealthPoints = 19
                    };

                    Program.isInBattle = true;
                    BattleActions.MoveList();
                    Program.Battle(ManBat);
                }
                storysent = true;
            }            
            else
            {

            }

        }
    }
}
