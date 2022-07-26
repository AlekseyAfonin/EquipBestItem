using System.Diagnostics.CodeAnalysis;
using TaleWorlds.Core;

namespace EquipBestItem.Models.Entities;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
public class Coefficients
{
    public float HeadArmor { get; set; }
    public float BodyArmor { get; set; }
    public float ArmArmor { get; set; }
    public float LegArmor { get; set; }

    public float ChargeDamage { get; set; }
    public float HitPoints { get; set; }
    public float Maneuver { get; set; }
    public float Speed { get; set; }

    public float MaxDataValue { get; set; }
    public float ThrustSpeed { get; set; }
    public float SwingSpeed { get; set; }
    public float MissileSpeed { get; set; }
    public float MissileDamage { get; set; }
    public float WeaponLength { get; set; }
    public float ThrustDamage { get; set; }
    public float SwingDamage { get; set; }
    public float Accuracy { get; set; }
    public float Handling { get; set; }
    public WeaponClass WeaponClass { get; set; }

    public float Weight { get; set; } = 0f;
}