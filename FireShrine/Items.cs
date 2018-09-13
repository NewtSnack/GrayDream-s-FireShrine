using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireShrine
{
    public interface IItems
        //TO DO: Ability to combine items based on attributes to solve puzzles. new item type needed, can't combine weapons or consums
    {
        string Name { get; set; }
        string Description { get; set; }
        void ToInv();
        void Drop();
        void Inspect();
        //void Combine();
    }
    interface IUsable
    {
        void Use();
    }
    interface IEquippable
    {
        //TRY GENERAL ATTACK NAMES FOR ALL IEQUIPPABLES ATTACK1 ATTACK2 ATTACK3 AND EACH IS DIFFERENT DEPENDENT ON ITEM TYPE. THEN EquippedItem.ATTACK1()
        string Name { get; set; }
        string WeaponType { get; set; }
        string[] Attributes { get; set; }
        string[] BattleActs { get; set; }
        int Damage { get; set; }
        void Equip();
        void Unequip();
    }
    class Blade : IItems, IEquippable
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string WeaponType { get; set; } //blade class implies sharp attribute
        public string[] Attributes { get; set; }
        public int Damage { get; set; }
        string[] BattleActs { get; set; }

        public Blade(string name = "Sharp Generic", string description = "Bladed", int damage = 1)
        {
            Name = name;
            Description = description;
            Damage = damage;
            WeaponType = "Sharp";
            BattleActs = new string[] {"Stab","Slash","Parry" };
        }
        public int Stab()
        {
            return Acts.Stab(this);
        }
        public int Slash()
        {
            return Acts.Slash(this);
        }
        public void Equip()
        {
            if (Character.equipped == Name)
            {
                Unequip();
            }
            else
            {
                Console.WriteLine($"{Name} has been Equipped.");
                Character.equipped = Name;
            }
        }
        public void ToInv()
        {
            Program.NuAdd(this);
        }

        public void Drop()
        {
            Console.WriteLine($"{Name} has been removed from your inventory");
            Character.Inventory2.Remove(this);
        }

        public void Inspect()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine(Description);
            Console.WriteLine("Weapon type: " + WeaponType);
            if (Attributes != null)
            {
                foreach (var item in Attributes)
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine("Base Damage: " + Damage);
            Console.WriteLine("Press E To Equip/Unequip this item.");
            ConsoleKey Keypress = Console.ReadKey(true).Key;
            if (Keypress == ConsoleKey.E)
            {
                Equip();
            }
        }

        public void Unequip()
        {
            Console.WriteLine($"{Name} has been Unequipped.");
            Character.equipped = "Fists";
            Character.Unarmbool = true;
        }

        public int BattleActions()
        {            

        }
    }
    class Blunt : IItems, IEquippable
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string WeaponType { get; set; } 
        public string[] Attributes { get; set; }

        public int Damage { get; set; }


        public Blunt(string name = "Blunt Generic", string description = "Heavy", int damage = 1)
        {
            Name = name;
            Description = description;
            Damage = damage;
            WeaponType = "Blunt";
        }
        public void Equip()
        {
            if (Character.equipped == Name)
            {
                Unequip();
            }
            else
            {
                Console.WriteLine($"{Name} has been Equipped.");
                Character.equipped = Name;
            }
        }

        public void ToInv()
        {
            Program.NuAdd(this);
        }

        public void Drop()
        {
            Console.WriteLine($"{Name} has been removed from your inventory");
            Character.Inventory2.Remove(this);
        }

        public void Inspect()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine(Description);
            Console.WriteLine("Weapon type: " + WeaponType);
            foreach (var item in Attributes)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Base Damage: " + Damage);
            Console.WriteLine("Press E To Equip/Unequip this item.");
            ConsoleKey Keypress = Console.ReadKey(true).Key;
            if (Keypress == ConsoleKey.E)
            {
                Equip();
            }
        }

        public void Unequip()
        {
            Console.WriteLine($"{Name} has been Unequipped.");
            Character.equipped = "Fists";
        }
    }
    class Consumable : IItems, IUsable
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int HealthRestored { get; set; }
        public int HungerValue { get; set; }
        public int ThirstValue { get; set; }

        public Consumable(string name = "Food Item", string description = "Eat or Drink", int  healthrestored = 1, int hungervalue = 1, int thirstvalue = 1)
        {
            Name = name;
            Description = description;
            HealthRestored = healthrestored;
            HungerValue = hungervalue;
            ThirstValue = thirstvalue;

        }
        public void Use()
        {
            Console.WriteLine($"{Name} has been consumed.");
        }

        public void ToInv()
        {
            Program.NuAdd(this);
        }

        public void Drop()
        {
            Program.NuDrop(this);
        }

        public void Inspect()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine(Description);
            if (HealthRestored != 0)
            {
                Console.WriteLine("Health: " + HealthRestored.ToString());
            }
            if (HungerValue != 0)
            {
                Console.WriteLine("Hunger: " + HungerValue.ToString());
            }
            if (ThirstValue != 0)
            {
                Console.WriteLine("Thirst: " + ThirstValue.ToString());
            }
        }
    }
    class Ranged : IItems, IEquippable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string WeaponType { get; set; } 
        public string[] Attributes { get; set; }

        public int Damage { get; set; }
        public int AmmoCount { get; set; }

        public void Drop()
        {
            Program.NuDrop(this);
        }

        public void Equip()
        {
            if (Character.equipped == Name)
            {
                Unequip();
            }
            else
            {
                Console.WriteLine($"{Name} has been Equipped.");
                Character.equipped = Name;
            }
        }

        public void Inspect()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine(Description);
            Console.WriteLine("Weapon type: " + WeaponType);
            foreach (var item in Attributes)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Ammunition Left: " + AmmoCount.ToString());
            Console.WriteLine("Base Damage: " + Damage);
            Console.WriteLine("Press E To Equip/Unequip this item.");
            ConsoleKey Keypress = Console.ReadKey(true).Key;
            if (Keypress == ConsoleKey.E)
            {
                Equip();
            }
        }

        public void ToInv()
        {
            Program.NuAdd(this);
        }

        public void Unequip()
        {
            Console.WriteLine($"{Name} has been Unequipped.");
            Character.equipped = "Fists";
        }
    }
}
