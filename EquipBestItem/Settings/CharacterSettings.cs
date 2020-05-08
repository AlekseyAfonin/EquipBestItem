using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipBestItem.Settings
{
    [Serializable]
    public class CharacterSettings
    {
        public string Name { get; set; }
        public List<FilterWeaponSettings> FilterWeapon { get; set; }
        public List<FilterArmorSettings> FilterArmor { get; set; }
        public FilterMountSettings FilterMount { get; set; }

        public enum ArmorSlot
        {
            Helm = 0,
            Cloak = 1,
            Armor = 2,
            Glove = 3,
            Boot = 4,
            Harness = 5
        }

        public enum WeaponSlot
        {
            Weapon1 = 0,
            Weapon2 = 1,
            Weapon3 = 2,
            Weapon4 = 3
        }

        public CharacterSettings(string name)
        {
            Name = name;
            FilterWeapon = new List<FilterWeaponSettings>(4);
            
            for (int i = 0; i < 4; i++)
            {
                FilterWeapon.Add(new FilterWeaponSettings());
            }

            FilterArmor = new List<FilterArmorSettings>(6);
            for (int i = 0; i < 6; i++)
            {
                FilterArmor.Add(new FilterArmorSettings());
            }

            FilterMount = new FilterMountSettings();
        }

        public CharacterSettings() 
        {
        }

    }
}
