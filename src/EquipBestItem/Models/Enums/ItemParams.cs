using System;

namespace EquipBestItem.Models.Enums;

[Flags]
public enum ItemParams
{
    HeadArmor = 1,
    BodyArmor = 2,
    ArmArmor = 4,
    LegArmor = 8,
    ChargeDamage = 16,
    HitPoints = 32,
    Maneuver = 64,
    Speed = 128,
    MaxDataValue = 256,
    ThrustSpeed = 512,
    SwingSpeed = 1024,
    MissileSpeed = 2048,
    MissileDamage = 4096,
    WeaponLength = 8192,
    ThrustDamage = 16384,
    SwingDamage = 32768,
    Accuracy = 65536,
    Handling = 131072,
    Weight = 262144
}