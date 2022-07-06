using EquipBestItem.Models;
using EquipBestItem.ViewModels;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;

namespace EquipBestItem.Layers;

internal class CoefficientsSettingsLayer : GauntletLayer
{
    public readonly EquipmentIndex EquipmentIndex;
    
    internal CoefficientsSettingsLayer(int localOrder, EquipmentIndex equipmentIndex, 
        CharacterCoefficientsRepository repository, string categoryId = "GauntletLayer", bool shouldClear = false) : 
        base(localOrder, categoryId, shouldClear)
    {
        var vm = new CoefficientsSettingsVM(equipmentIndex, repository);
        EquipmentIndex = equipmentIndex;
        LoadMovie("CoefficientsSettings", vm);
    }
}