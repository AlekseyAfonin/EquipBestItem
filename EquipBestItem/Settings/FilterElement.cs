using System;
using System.Xml.Serialization;

namespace EquipBestItem.Settings
{
    [Serializable]
    public class FilterElement
    {
        //armor
        [XmlAttribute] public float HeadArmor { get; set; }
        [XmlAttribute] public float ArmorBodyArmor { get; set; }
        [XmlAttribute] public float LegArmor { get; set; }
        [XmlAttribute] public float ArmArmor { get; set; }

        //harness
        [XmlAttribute] public float ManeuverBonus { get; set; }
        [XmlAttribute] public float SpeedBonus { get; set; }
        [XmlAttribute] public float ChargeBonus { get; set; }


        //mount
        [XmlAttribute] public float ChargeDamage { get; set; }
        [XmlAttribute] public float HitPoints { get; set; }
        [XmlAttribute] public float Maneuver { get; set; }
        [XmlAttribute] public float Speed { get; set; }

        //weapons
        [XmlAttribute] public float MaxDataValue { get; set; }
        [XmlAttribute] public float ThrustSpeed { get; set; }
        [XmlAttribute] public float SwingSpeed { get; set; }
        [XmlAttribute] public float MissileSpeed { get; set; }
        [XmlAttribute] public float MissileDamage { get; set; }
        [XmlAttribute] public float WeaponLength { get; set; }
        [XmlAttribute] public float ThrustDamage { get; set; }
        [XmlAttribute] public float SwingDamage { get; set; }
        [XmlAttribute] public float Accuracy { get; set; }
        [XmlAttribute] public float Handling { get; set; }
        [XmlAttribute] public float WeaponBodyArmor { get; set; }

        [XmlAttribute] public float Weight { get; set; }
    }
}
