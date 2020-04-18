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
        public bool IsHelmLocked { get; set; }
        public bool IsCloakLocked { get; set; }
        public bool IsArmorLocked { get; set; }
        public bool IsGloveLocked { get; set; }
        public bool IsBootLocked { get; set; }
        public bool IsMountLocked { get; set; }
        public bool IsHarnessLocked { get; set; }
        public bool IsWeapon1Locked { get; set; }
        public bool IsWeapon2Locked { get; set; }
        public bool IsWeapon3Locked { get; set; }
        public bool IsWeapon4Locked { get; set; }

        public CharacterSettings(string name)
        {
            Name = name;
        }

        public CharacterSettings() 
        {
        }

    }
}
