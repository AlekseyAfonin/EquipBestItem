using EquipBestItem.Models.Entities;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;

namespace EquipBestItem.Models.BestItemCalculator;

public interface IBestItemCalculator
{
    string Name { get; }
    
    float GetItemValue(EquipmentElement equipmentElement, CalculatorContext context);

    public bool IsItemNotValid(SPItemVM item, CalculatorContext context);
    
    bool IsSlotItemNotValid(EquipmentElement equipmentElement, CalculatorContext context);
}