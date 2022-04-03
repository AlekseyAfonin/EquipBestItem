using System;
using System.Xml.Serialization;
using TaleWorlds.Core;

namespace EquipBestItem.Models.Entities;

[Serializable]
public abstract class Coefficients
{
    public float HeadArmor { get; set; } = 100f;
    public float BodyArmor { get; set; } = 100f;
    public float ArmArmor { get; set; } = 100f;
    public float LegArmor { get; set; } = 100f;

    public float ChargeDamage { get; set; } = 100f;
    public float HitPoints { get; set; } = 100f;
    public float Maneuver { get; set; } = 100f;
    public float Speed { get; set; } = 100f;

    public float MaxDataValue { get; set; } = 100f;
    public float ThrustSpeed { get; set; } = 100f;
    public float SwingSpeed { get; set; } = 100f;
    public float MissileSpeed { get; set; } = 100f;
    public float MissileDamage { get; set; } = 100f;
    public float WeaponLength { get; set; } = 100f;
    public float ThrustDamage { get; set; } = 100f;
    public float SwingDamage { get; set; } = 100f;
    public float Accuracy { get; set; } = 100f;
    public float Handling { get; set; } = 100f;
    public WeaponClass? WeaponClass { get; set; }
    
    public float Weight { get; set; } = 0f;
}