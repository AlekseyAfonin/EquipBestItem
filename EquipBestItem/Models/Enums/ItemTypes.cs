namespace EquipBestItem.Models.Enums;

internal static class ItemTypes
{
    public const ItemParams Armor = ItemParams.Weight |
                                    ItemParams.ArmArmor |
                                    ItemParams.BodyArmor |
                                    ItemParams.HeadArmor |
                                    ItemParams.LegArmor;

    public const ItemParams Weapon = ItemParams.MaxDataValue |
                                     ItemParams.MissileDamage |
                                     ItemParams.MissileSpeed |
                                     ItemParams.Accuracy |
                                     ItemParams.Handling |
                                     ItemParams.Weight |
                                     ItemParams.SwingDamage |
                                     ItemParams.SwingSpeed |
                                     ItemParams.ThrustDamage |
                                     ItemParams.ThrustSpeed |
                                     ItemParams.WeaponLength;
    
    public const ItemParams MeleeWeapon = ItemParams.Handling |
                                          ItemParams.Weight |
                                          ItemParams.SwingDamage |
                                          ItemParams.SwingSpeed |
                                          ItemParams.ThrustDamage |
                                          ItemParams.ThrustSpeed |
                                          ItemParams.WeaponLength;

    public const ItemParams Consumable = ItemParams.Accuracy |
                                   ItemParams.Weight |
                                   ItemParams.MissileSpeed |
                                   ItemParams.MissileDamage |
                                   ItemParams.WeaponLength |
                                   ItemParams.MaxDataValue;

    public const ItemParams RangedWeapon = ItemParams.Accuracy |
                                           ItemParams.Weight |
                                           ItemParams.MissileSpeed |
                                           ItemParams.MissileDamage |
                                           ItemParams.ThrustSpeed |
                                           ItemParams.MaxDataValue; //TODO check
    
    public const ItemParams Shield = ItemParams.Weight |
                                     ItemParams.ThrustSpeed |
                                     ItemParams.BodyArmor |
                                     ItemParams.WeaponLength |
                                     ItemParams.MaxDataValue;

    public const ItemParams Horse = ItemParams.Maneuver |
                                    ItemParams.Speed |
                                    ItemParams.ChargeDamage |
                                    ItemParams.HitPoints;

    public const ItemParams Head = ItemParams.Weight |
                                    ItemParams.HeadArmor;

    public const ItemParams Capes = ItemParams.Weight |
                                    ItemParams.ArmArmor |
                                    ItemParams.BodyArmor;

    public const ItemParams Legs = ItemParams.Weight |
                                   ItemParams.LegArmor;

    public const ItemParams Arms = ItemParams.Weight |
                                   ItemParams.ArmArmor;

    public const ItemParams HorseHarness = ItemParams.Weight |
                                           ItemParams.BodyArmor;



}