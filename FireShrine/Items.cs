using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireShrine
{
    public interface IItems
    {
        string Name { get; set; }
        string Description { get; set; }
        void ToInv();
        void Drop();
        void Inspect();
    }
    interface IUsable
    {
        void Use();
    }
    interface IEquippable
    {
        string Name { get; set; }
        string WeaponType { get; set; }
        string[] Attributes { get; set; }
        void Equip();
    }
    class Blade : IItems, IEquippable
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string WeaponType { get; set; } //blade class implies sharp attribute
        public string[] Attributes { get; set; }

        public int Damage { get; set; }


        public Blade(string name = "Sharp Generic", string description = "Bladed", int damage = 1)
        {
            Name = name;
            Description = description;
            Damage = damage;
            WeaponType = "Sharp";            
        }
        public void Equip()
        {
            Console.WriteLine($"{Name} has been Equipped.");
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
            throw new NotImplementedException();
        }
    }
    class Blunt : IItems, IEquippable
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string WeaponType { get; set; } //blade class implies sharp attribute
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
            Console.WriteLine($"{Name} has been Equipped.");
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
