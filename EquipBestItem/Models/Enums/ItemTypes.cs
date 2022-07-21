namespace EquipBestItem.Models.Enums;

internal static class ItemTypes
{
    public static readonly ItemParams[] Armor = 
    {
        ItemParams.Weight,
        ItemParams.ArmArmor,
        ItemParams.BodyArmor,
        ItemParams.HeadArmor,
        ItemParams.LegArmor
    };

    public static readonly ItemParams[] Weapon =
    {
        ItemParams.MaxDataValue,
        ItemParams.MissileDamage,
        ItemParams.MissileSpeed,
        ItemParams.Accuracy,
        ItemParams.Handling,
        ItemParams.Weight,
        ItemParams.SwingDamage,
        ItemParams.SwingSpeed,
        ItemParams.ThrustDamage,
        ItemParams.ThrustSpeed,
        ItemParams.WeaponLength
    };

    public static readonly ItemParams[] MeleeWeapon =
    {
        ItemParams.Handling,
        ItemParams.Weight,
        ItemParams.SwingDamage,
        ItemParams.SwingSpeed,
        ItemParams.ThrustDamage,
        ItemParams.ThrustSpeed,
        ItemParams.WeaponLength
    };

    public static readonly ItemParams[] Thrown =
    {
        ItemParams.Accuracy, 
        ItemParams.Weight, 
        ItemParams.MissileSpeed, 
        ItemParams.MissileDamage,
        ItemParams.WeaponLength, 
        ItemParams.MaxDataValue
    };

    public static readonly ItemParams[] Ammo =
    {
        ItemParams.Weight,
        ItemParams.MissileDamage,
        ItemParams.MaxDataValue
    };

    public static readonly ItemParams[] Bow =
    {
        ItemParams.Accuracy,
        ItemParams.Weight,
        ItemParams.MissileSpeed,
        ItemParams.MissileDamage,
        ItemParams.ThrustSpeed
    };

    public static readonly ItemParams[] Crossbow =
    {
        ItemParams.Accuracy,
        ItemParams.Weight,
        ItemParams.MissileSpeed,
        ItemParams.MissileDamage,
        ItemParams.ThrustSpeed,
        ItemParams.MaxDataValue
    };
    
    public static readonly ItemParams[] Shield =
    {
        ItemParams.Weight,
        ItemParams.ThrustSpeed,
        ItemParams.BodyArmor,
        ItemParams.WeaponLength,
        ItemParams.MaxDataValue
    };

    public static readonly ItemParams[] Horse =
    {
        ItemParams.Maneuver,
        ItemParams.Speed,
        ItemParams.ChargeDamage,
        ItemParams.HitPoints
    };

    public static readonly ItemParams[] Head =
    {
        ItemParams.Weight,
        ItemParams.HeadArmor
    };

    public static readonly ItemParams[] Capes =
    {
        ItemParams.Weight,
        ItemParams.ArmArmor,
        ItemParams.BodyArmor
    };

    public static readonly ItemParams[] Legs =
    {
        ItemParams.Weight,
        ItemParams.LegArmor
    };

    public static readonly ItemParams[] Arms =
    {
        ItemParams.Weight,
        ItemParams.ArmArmor
    };

    public static readonly ItemParams[] HorseHarness =
    {
        ItemParams.Weight,
        ItemParams.BodyArmor
    };
}