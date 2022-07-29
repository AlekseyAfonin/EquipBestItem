using EquipBestItem.Models.Entities;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;

namespace EquipBestItem.Models.BestItemCalculator;

public class EffectivenessCalculator : BestItemCalculatorBase
{
    public EffectivenessCalculator(string name) : base(name)
    {
    }

    public override float GetItemValue(EquipmentElement equipmentElement, CalculatorContext context)
    {
        return equipmentElement.Item.Effectiveness;
    }

    public override bool IsItemNotValid(SPItemVM item, CalculatorContext context)
    {
        var index = context.EquipmentIndex;
        var character = context.Character;
        var equipment = context.IsInWarSet ? character.FirstBattleEquipment : character.FirstCivilianEquipment;
        
        var currentWeaponComponents = item.ItemRosterElement.EquipmentElement.Item?.Weapons;
        var itemWeaponComponents = equipment[index].Item?.Weapons;

        // If they differ in the number of components skip
        if (itemWeaponComponents?.Count != currentWeaponComponents?.Count)
            return true;

        // If they have the same number of components, then we enumerate each one and compare by class
        for (var i = 0; i < itemWeaponComponents?.Count; i++)
            if (currentWeaponComponents?[i].ItemUsage != itemWeaponComponents?[i].ItemUsage)
                return true;

        return false;
    }

    public override bool IsSlotItemNotValid(EquipmentElement equipmentElement, CalculatorContext context)
    {
        return false;
    }
}