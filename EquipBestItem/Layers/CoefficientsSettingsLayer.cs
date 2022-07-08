using EquipBestItem.Models;
using EquipBestItem.ViewModels;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;

namespace EquipBestItem.Layers;

internal class CoefficientsSettingsLayer : GauntletLayer
{
    public readonly EquipmentIndex EquipmentIndex;
    
    internal CoefficientsSettingsLayer(int localOrder, EquipmentIndex equipmentIndex,
        CharacterCoefficientsRepository repository, SPInventoryVM originVM, string categoryId = "GauntletLayer", bool shouldClear = false) : 
        base(localOrder, categoryId, shouldClear)
    {
        var vm = new CoefficientsSettingsVM(equipmentIndex, repository, originVM);
        EquipmentIndex = equipmentIndex;
        LoadMovie("CoefficientsSettings", vm);
    }
}