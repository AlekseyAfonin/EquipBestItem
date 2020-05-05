using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace EquipBestItem
{
    public class FilterWeaponSettings
    {
        public float MaxDataValue { get; set; } = 1f;
        public float ThrustSpeed { get; set; } = 1f;
        public float SwingSpeed { get; set; } = 1f;
        public float MissileSpeed { get; set; } = 1f;
        public float WeaponLength { get; set; } = 1f;
        public float ThrustDamage { get; set; } = 1f;
        public float SwingDamage { get; set; } = 1f;
        public float Accuracy { get; set; } = 1f;
        public float Handling { get; set; } = 1f;
        public float WeaponWeight { get; set; } = 0f;
        public float WeaponBodyArmor { get; set; } = 1f;

        //public DamageTypes SwingDamageType { get; set; } = 0;
        //public DamageTypes ThrustDamageType { get; set; } = 0;
        //public int MissileDamage { get; set; }
        //public float WeaponBalance { get; set; }


        //public WeaponClass? WeaponClass { get; set; }
        //public string ItemUsage { get; set; }


        //public WeaponFlags? WeaponFlags { get; set; }

        public bool ThisFilterNotDefault()
        {
            if (this.MaxDataValue != 1f) return true;
            if (this.ThrustSpeed != 1f) return true;
            if (this.SwingSpeed != 1f) return true;
            if (this.MissileSpeed != 1f) return true;
            if (this.WeaponLength != 1f) return true;
            if (this.ThrustDamage != 1f) return true;
            if (this.SwingDamage != 1f) return true;
            if (this.Accuracy != 1f) return true;
            if (this.Handling != 1f) return true;
            if (this.WeaponWeight != 0f) return true;
            if (this.WeaponBodyArmor != 1f) return true;
            return false;
        }

        public bool ThisFilterLocked()
        {
            if (this.MaxDataValue == 0f &&
                this.ThrustSpeed == 0f &&
                this.SwingSpeed == 0f &&
                this.MissileSpeed == 0f &&
                this.WeaponLength == 0f &&
                this.ThrustDamage == 0f &&
                this.SwingDamage == 0f &&
                this.Accuracy == 0f &&
                this.Handling == 0f &&
                this.WeaponWeight == 0f &&
                this.WeaponBodyArmor == 0f)
                return true;
            return false;
        }

        public FilterWeaponSettings()
        {
            
        }
    }
}
