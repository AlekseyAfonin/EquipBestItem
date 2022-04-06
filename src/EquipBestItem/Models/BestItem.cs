using EquipBestItem.Extensions;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using Messages.FromLobbyServer.ToClient;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.Models;

public static class BestItem
{
    public static SPItemVM? GetBestItem(Coefficients coefficients, EquipmentElement currentItem, 
        params MBBindingList<SPItemVM>?[] lists)
    {
        var bestItemValue = currentItem.IsEmpty ? -1f : currentItem.GetItemValueByCoefficient(coefficients);

        SPItemVM? bestItem = null;

        foreach (var list in lists)
        {
            if (list is null) continue;
            
            foreach (var item in list)
            {
                if (!item.IsEquipableItem || item.IsLocked || !item.CanCharacterUseItem) continue;
            
                var itemValue = item.ItemRosterElement.EquipmentElement.GetItemValueByCoefficient(coefficients);

                if (bestItemValue > itemValue) continue;

                bestItem = item;
                bestItemValue = itemValue;
            }
        }
        
        return bestItem;
    }
}