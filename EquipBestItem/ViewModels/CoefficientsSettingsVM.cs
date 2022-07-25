using System.Threading.Tasks;
using EquipBestItem.Models;
using EquipBestItem.Models.Enums;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM : ViewModel
{
    private readonly CoefficientsSettings _model;
    private string _headerText;

    private SelectorVM<SelectorItemVM>? _weaponClassSelector;

    internal CoefficientsSettingsVM(EquipmentIndex equipIndex, CharacterCoefficientsRepository repository,
        ModSPInventoryVM modVM)
    {
        var equipmentIndex = (CustomEquipmentIndex) equipIndex;
        _model = new CoefficientsSettings(this, equipmentIndex, repository, modVM);
        _model.LoadValues();
        _headerText = CoefficientsSettings.GetHeaderText(equipIndex);

        if (equipIndex >= EquipmentIndex.ArmorItemBeginSlot) return;

        var weaponClassesName = ItemTypes.GetParamsNames();
        _weaponClassSelector = new SelectorVM<SelectorItemVM>(weaponClassesName, (int) _model.GetSelectedWeaponClass(),
            OnWeaponClassChange);
    }

    [DataSourceProperty]
    public HintViewModel ButtonDefaultHint { get; } = new(new TextObject("{=ebi_hint_default}Reset to default values"));

    [DataSourceProperty]
    public HintViewModel ButtonLockHint { get; } = new(new TextObject("{=ebi_hint_lock}Disable search for this slot"));

    [DataSourceProperty]
    public HintViewModel CheckboxHint { get; } = new(new TextObject("{=ebi_hint_checkbox}Set to default value"));

    [DataSourceProperty]
    public HintViewModel PercentHint { get; } =
        new(new TextObject("{=ebi_hint_percent}How much does the parameter affect the value of the item"));

    [DataSourceProperty] public string ButtonDefaultText { get; } = new TextObject("{=ebi_default}Default").ToString();
    [DataSourceProperty] public string ButtonLockText { get; } = new TextObject("{=ebi_lock}Lock").ToString();

    [DataSourceProperty]
    public bool WeaponClassIsHidden
    {
        get => _weaponClassIsHidden;
        set
        {
            if (_weaponClassIsHidden == value) return;
            _weaponClassIsHidden = value;
            OnPropertyChanged();
        }
    }

    [DataSourceProperty]
    public SelectorVM<SelectorItemVM>? WeaponClassSelector
    {
        get => _weaponClassSelector;
        set
        {
            if (value != _weaponClassSelector)
            {
                _weaponClassSelector = value;
                OnPropertyChangedWithValue(value);
            }
        }
    }

    private void OnWeaponClassChange(SelectorVM<SelectorItemVM> obj)
    {
        var weaponClass = (WeaponClass) obj.SelectedIndex;
        _model.VisibleParams = ItemTypes.GetParamsByWeaponClass(weaponClass);
        WeaponClass = weaponClass;
    }

    public override void RefreshValues()
    {
        base.RefreshValues();
    }

    private void OnWeaponClassChanged()
    {
    }

    public override void OnFinalize()
    {
        _model.OnFinalize();
        _weaponClassSelector?.OnFinalize();
        base.OnFinalize();
    }

    public void ExecuteDefault()
    {
        Task.Run(() => _model.DefaultClick());
    }

    public void ExecuteLock()
    {
        Task.Run(() => _model.LockClick());
    }

    public void ExecuteClose()
    {
        CoefficientsSettings.CloseClick();
    }

    public void ExecuteCheckboxSetDefault(string paramName)
    {
        _model.CheckboxClick(paramName);
    }
}