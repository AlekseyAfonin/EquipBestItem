using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace EquipBestItem.Models;

internal static class ModTexts
{
    public static readonly string HitPoints = new TextObject("{=aCkzVUCR}Hit Points: ").ToString();
    
    public static readonly string ThrustSpeed = new TextObject("{=VPYazFVH}Thrust Speed: ").ToString();
    
    public static readonly string HeadArmor = GameTexts.FindText("str_head_armor").ToString();
    
    public static readonly string BodyArmor = GameTexts.FindText("str_body_armor").ToString();
    
    public static readonly string LegArmor = GameTexts.FindText("str_leg_armor").ToString();
    
    public static readonly string
        ArmArmor = new TextObject("{=cf61cce254c7dca65be9bebac7fb9bf5}Arm Armor: ").ToString();
   
    public static readonly string Weight = GameTexts.FindText("str_weight_text").ToString();
   
    public static readonly string ChargeDamage =
        new TextObject("{=c7638a0869219ae845de0f660fd57a9d}Charge Damage: ").ToString();
    
    public static readonly string Maneuver = new TextObject("{=3025020b83b218707499f0de3135ed0a}Maneuver: ").ToString();
    
    public static readonly string Speed = new TextObject("{=74dc1908cb0b990e80fb977b5a0ef10d}Speed: ").ToString();
    
    public static readonly string SwingSpeed = new TextObject("{=nfQhamAF}Swing Speed: ").ToString();
    
    public static readonly string MissileSpeed = new TextObject("{=YukbQgHJ}Missile Speed: ").ToString();

    public static readonly string MissileDamage =
        new TextObject("{=c9c5dfed2ca6bcb7a73d905004c97b23}Damage: ").ToString();
    
    public static readonly string WeaponLength = new TextObject("{=XUtiwiYP}Length: ").ToString();
    
    public static readonly string ThrustDamage = new TextObject("{=7sUhWG0E}Thrust Damage: ").ToString();
    
    public static readonly string SwingDamage = new TextObject("{=fMmlUHyz}Swing Damage: ").ToString();
    
    public static readonly string Accuracy = new TextObject("{=xEWwbGVK}Accuracy: ").ToString();
    
    public static readonly string Handling = new TextObject("{=YOSEIvyf}Handling: ").ToString();
    
    public static readonly string WeaponBodyArmor = new TextObject("{=bLWyjOdS}Body Armor: ").ToString();

    public static readonly string StackAmount =
        new TextObject("{=05fdfc6e238429753ef282f2ce97c1f8}Stack Amount: ").ToString();

    public static readonly string AmmoLimit =
        new TextObject("{=6adabc1f82216992571c3e22abc164d7}Ammo Limit: ").ToString();
    
    public static readonly string Weapons = new TextObject("{=2RIyK1bp}Weapons").ToString();
    
    public static readonly string Head = GameTexts.FindText("str_inventory_helm_slot").ToString();
    
    public static readonly string Body = GameTexts.FindText("str_inventory_armor_slot").ToString();
    
    public static readonly string Leg = GameTexts.FindText("str_inventory_boot_slot").ToString();
    
    public static readonly string Gloves = GameTexts.FindText("str_inventory_glove_slot").ToString();
    
    public static readonly string Cape = GameTexts.FindText("str_inventory_cloak_slot").ToString();
    
    public static readonly string Horse = GameTexts.FindText("str_inventory_mount_slot").ToString();
    
    public static readonly string HorseHarness = GameTexts.FindText("str_inventory_mount_armor_slot").ToString();
    
    public static readonly TextObject ButtonDefaultHint = new("{=ebi_hint_default}Reset to default values");
    
    public static readonly TextObject ButtonLockHint = new("{=ebi_hint_lock}Disable search for this slot");
    
    public static readonly TextObject CheckboxHint = new("{=ebi_hint_checkbox}Set to default value");

    public static readonly TextObject PercentHint =
        new("{=ebi_hint_percent}How much does the parameter affect the value of the item");

    public static readonly string ButtonDefault = new TextObject("{=ebi_default}Default").ToString();
    
    public static readonly string ButtonLock = new TextObject("{=ebi_lock}Lock").ToString();

    
}