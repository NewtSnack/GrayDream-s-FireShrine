using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireShrine
{
    interface IItems
    {
        string Name { get; set; }
        string Description { get; set; }    

    }
    interface IUsable
    {

    }
    interface IEquippable
    {
        void Equip();
    }
    class Blades : IItems, IEquippable
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Damage { get; set; }
        public string WeaponType { get; set; }

        public Blades(string name = "Sharp Generic", string description = "Bladed", int damage = 1, string weaponType = "Sharp")
        {
            Name = name;
            Description = description;
            Damage = damage;
            WeaponType = weaponType;            
        }
        public bool isSharp = true;
        public void Equip()
        {
            Console.WriteLine($"{Name} has been Equipped.");
        }


    }
    class Consumables : IItems, IUsable
    {

    }
}
