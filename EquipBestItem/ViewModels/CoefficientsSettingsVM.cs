using EquipBestItem.Models;
using EquipBestItem.Models.Enums;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM : ViewModel
{
    private const float Tolerance = 0.00001f;
    private readonly CoefficientsSettings _model;
    private string _headerText;
    private SelectorVM<SelectorItemVM> _weaponClassSelector;

    internal CoefficientsSettingsVM(EquipmentIndex equipIndex, CharacterCoefficientsRepository repository,
        SPInventoryMixin mixin)
    {
        var equipmentIndex = (CustomEquipmentIndex) equipIndex;
        _model = new CoefficientsSettings(this, equipmentIndex, repository, mixin);
        _model.LoadValues();
        _headerText = CoefficientsSettings.GetHeaderText(equipIndex);

        if (equipIndex >= EquipmentIndex.ArmorItemBeginSlot) return;

        var weaponClassesName = ItemTypes.GetParamsNames();
        _weaponClassSelector = new SelectorVM<SelectorItemVM>(weaponClassesName, (int) _model.GetSelectedWeaponClass(),
            OnWeaponClassChange);
    }
    
    public override void OnFinalize()
    {
        _model.OnFinalize();
        _weaponClassSelector?.OnFinalize();
        base.OnFinalize();
    }
    
    private void OnWeaponClassChange(SelectorVM<SelectorItemVM> obj)
    {
        var weaponClass = (WeaponClass) obj.SelectedIndex;
        WeaponClass = weaponClass;
        _model.VisibleParams = _model.GetVisibleParams();
    }
}