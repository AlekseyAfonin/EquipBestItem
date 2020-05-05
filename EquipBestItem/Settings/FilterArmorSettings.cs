using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipBestItem.Settings
{
    public class FilterArmorSettings
    {
        public float HeadArmor { get; set; } = 1f;
        public float ArmorBodyArmor { get; set; } = 1f;
        public float LegArmor { get; set; } = 1f;
        public float ArmArmor { get; set; } = 1f;

        public float ManeuverBonus { get; set; } = 1f;
        public float SpeedBonus { get; set; } = 1f;
        public float ChargeBonus { get; set; } = 1f;
        public float ArmorWeight { get; set; } = 0;

        public bool ThisFilterNotDefault()
        {
            if (this.HeadArmor != 1f) return true;
            if (this.ArmorBodyArmor != 1f) return true;
            if (this.LegArmor != 1f) return true;
            if (this.ArmArmor != 1f) return true;
            if (this.ManeuverBonus != 1f) return true;
            if (this.SpeedBonus != 1f) return true;
            if (this.ChargeBonus != 1f) return true;
            if (this.ArmorWeight != 0f) return true;
            return false;
        }

        public bool ThisFilterLocked()
        {
            if (this.HeadArmor == 0f &&
                this.ArmorBodyArmor == 0f &&
                this.LegArmor == 0f &&
                this.ArmArmor == 0f &&
                this.ManeuverBonus == 0f &&
                this.SpeedBonus == 0f &&
                this.ChargeBonus == 0f &&
                this.ArmorWeight == 0f)
                return true;
            return false;
        }
    }
}
