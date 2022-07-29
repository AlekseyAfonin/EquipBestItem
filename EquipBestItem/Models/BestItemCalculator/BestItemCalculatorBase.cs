using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;

namespace EquipBestItem.Models.BestItemCalculator;

public abstract class BestItemCalculatorBase : IBestItemCalculator
{
    protected BestItemCalculatorBase(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
    
    public abstract float GetItemValue(EquipmentElement equipmentElement, CalculatorContext context);

    public abstract bool IsItemNotValid(SPItemVM item, CalculatorContext context);

    public abstract bool IsSlotItemNotValid(EquipmentElement equipmentElement, CalculatorContext context);
}