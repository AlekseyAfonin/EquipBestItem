using System;

namespace EquipBestItem.Models.Enums;

[Flags]
public enum ItemParams : uint
{
    HeadArmor = 1U,
    BodyArmor = 2U,
    ArmArmor = 4U,
    LegArmor = 8U,
    ChargeDamage = 16U,
    HitPoints = 32U,
    Maneuver = 64U,
    Speed = 128U,
    MaxDataValue = 256U,
    ThrustSpeed = 512U,
    SwingSpeed = 1024U,
    MissileSpeed = 2048U,
    MissileDamage = 4096U,
    WeaponLength = 8192U,
    ThrustDamage = 16384U,
    SwingDamage = 32768U,
    Accuracy = 65536U,
    Handling = 131072U,
    Weight = 262144U
}