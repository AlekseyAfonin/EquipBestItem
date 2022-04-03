using EquipBestItem.Extensions;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.Models;

public class BestEquipment
{
    private readonly Equipment _activeEquipment;

    //public Equipment Equipment { get; } = new Equipment();

    public BestEquipment(Equipment activeEquipment)
    {
        _activeEquipment = activeEquipment;
    }
    
    public (float, EquipmentElement?) GetBestItemForSlot(
        EquipmentIndex slot, 
        MBBindingList<SPItemVM> items, 
        Coefficients coefficients)
    {
        var bestItemValue = _activeEquipment[slot].GetItemValueByCoefficient(coefficients);
        EquipmentElement? bestItem = null;

        foreach (var item in items)
        {
            if (!item.IsEquipableItem) continue;
            
            var itemValue = item.ItemRosterElement.EquipmentElement.GetItemValueByCoefficient(coefficients);

            if (bestItemValue > itemValue) continue;

            bestItem = item.ItemRosterElement.EquipmentElement;
            bestItemValue = itemValue;
        }

        return (bestItemValue, bestItem);
    }
}