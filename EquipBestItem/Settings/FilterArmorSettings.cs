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
        public float ArmorWeight { get; set; } = 1f;
    }
}
