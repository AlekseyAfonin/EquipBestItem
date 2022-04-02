using System;
using EquipBestItem.Models.Enums;
using TaleWorlds.Core;

namespace EquipBestItem.Models;

public class BestItem
{
    private Equipment _equipment = new Equipment();

    public BestItem()
    {
    }
    
    public EquipmentElement? GetBestItemForSlot(EquipmentIndex slot)
    {
        return _equipment.GetEquipmentFromSlot(slot);
    }
    
    public float GetItemValue(EquipmentElement item, Coefficients coefficients)
    {
        if (item.IsEmpty) return -9999f;

        float getItemValue<T>(T obj, ItemParams itemParams)
        {
            var sumCoef = 0f;
            var value = 0f;
        
            foreach (var param in itemParams.GetFlags())
            {
                var coef = coefficients.GetPropValue(param.ToString());
                sumCoef += coef;
                value += obj.GetPropModValue(item.ItemModifier, param.ToString()) * coef;
            }

            return value / sumCoef;
        }
        
        ItemObject itemObject = item.Item;
        
        if (itemObject.HasArmorComponent)
        {
            return getItemValue(itemObject.ArmorComponent, ItemTypes.Armor);
        }

        if (itemObject.HasWeaponComponent)
        {
            var weaponComponent = itemObject.WeaponComponent.PrimaryWeapon;

            if (weaponComponent.IsRangedWeapon)
            {
                return getItemValue(weaponComponent, 
                    weaponComponent.IsConsumable 
                        ? ItemTypes.Comsumable 
                        : ItemTypes.RangedWeapon);
            }

            if (weaponComponent.IsMeleeWeapon)
            {
                return getItemValue(weaponComponent, ItemTypes.MeleeWeapon);
            }
            
            if (weaponComponent.IsShield)
            {
                return getItemValue(weaponComponent, ItemTypes.Shield);
            }  
        }

        if (itemObject.HasHorseComponent)
        {
            return getItemValue(itemObject.HorseComponent, ItemTypes.Shield);
        }

        return 0f; //TODO
    }
    
    
}