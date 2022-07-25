using EquipBestItem.Models;
using EquipBestItem.ViewModels;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;

namespace EquipBestItem.Layers;

internal class CoefficientsSettingsLayer : GauntletLayer
{
    private readonly CoefficientsSettingsVM _vm;
    public readonly EquipmentIndex EquipmentIndex;

    internal CoefficientsSettingsLayer(int localOrder, EquipmentIndex equipmentIndex,
        CharacterCoefficientsRepository repository, ModSPInventoryVM modVM, string categoryId = "GauntletLayer",
        bool shouldClear = false) :
        base(localOrder, categoryId, shouldClear)
    {
        _vm = new CoefficientsSettingsVM(equipmentIndex, repository, modVM);
        EquipmentIndex = equipmentIndex;
        LoadMovie("CoefficientsSettings", _vm);
    }

    protected override void OnFinalize()
    {
        Helper.ShowMessage("CoefficientsSettingsLayer OnFinalize");
        _vm.OnFinalize();
        base.OnFinalize();
    }
}