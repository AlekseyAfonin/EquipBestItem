using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EquipBestItem.Extensions;
using EquipBestItem.Layers;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using EquipBestItem.ViewModels;
using SandBox.GauntletUI;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.Models;

internal class CoefficientsSettings
{
    private const float Tolerance = 0.00001f;
    private readonly CoefficientsSettingsVM _vm;
    private readonly CharacterCoefficientsRepository _repository;
    private readonly CustomEquipmentIndex _equipmentIndex;
    private readonly ModSPInventoryVM _modVM;
    private readonly CharacterCoefficients _currentCharacterCoefficients;
    private readonly CharacterCoefficients _defaultCharacterCoefficients;
    
    internal CoefficientsSettings(CoefficientsSettingsVM vm, CustomEquipmentIndex equipmentIndex, 
        CharacterCoefficientsRepository repository, ModSPInventoryVM modVM)
    {
        _vm = vm;
        _repository = repository;
        _equipmentIndex = equipmentIndex;
        _modVM = modVM;
        
        _currentCharacterCoefficients = _repository.Read(_modVM.CurrentCharacterName);
        _defaultCharacterCoefficients = _repository.Read(CharacterCoefficients.Default);

        _visibleParams = GetVisibleParams();
    }

    public void LoadValues()
    {
        foreach (var param in VisibleParams)
        {
            var paramName = param.ToString();
            var value = Coefficients.GetPropValue($"{paramName}");
            _vm.SetPropertyValue($"{paramName}IsHidden", false);
            _vm.SetPropertyValue($"{paramName}", value);
            UpdateCheckboxState(value, paramName);
        }

        if (_equipmentIndex < CustomEquipmentIndex.Head)
        {
            _vm.WeaponClassIsHidden = false;
            _vm.WeaponClass = Coefficients.WeaponClass;
        }
        
        UpdateVisibleParamsPercentText();

        //_vm.PropertyChanged += OnPropertyChanged;
        _vm.PropertyChangedWithValue += OnPropertyChangedWithValue;
    }

    private void OnPropertyChangedWithValue(object sender, PropertyChangedWithValueEventArgs e)
    {
        Coefficients.SetPropValue($"{e.PropertyName}", e.Value);
        //Coefficients.SetPropValue($"{e.PropertyName}", Enum.Parse(typeof(WeaponClass),e.Value.ToString()));
        UpdateVisibleParamsPercentText();
        Task.Run(async () => await _modVM.UpdateBestItemsAsync());
    }

    private Coefficients Coefficients => _modVM.IsInWarSet
        ? _currentCharacterCoefficients.WarCoefficients[(int) _equipmentIndex]
        : _currentCharacterCoefficients.CivilCoefficients[(int) _equipmentIndex];

    private Coefficients DefaultCoefficients => _modVM.IsInWarSet
        ? _defaultCharacterCoefficients.WarCoefficients[(int) _equipmentIndex]
        : _defaultCharacterCoefficients.CivilCoefficients[(int) _equipmentIndex];

    private IEnumerable<ItemParams> _visibleParams;
    
    public IEnumerable<ItemParams> VisibleParams
    {
        get => _visibleParams;
        set
        {
            if (Equals(value, _visibleParams)) return;

            foreach (var param in _visibleParams)
            {
                _vm.SetPropertyValue($"{param}IsHidden", true);
            }

            foreach (var param in value)
            {
                var paramName = param.ToString();
                _vm.SetPropertyValue($"{paramName}IsHidden", false);
                var paramValue = Coefficients.GetPropValue($"{paramName}");
                _vm.SetPropertyValue($"{paramName}", paramValue);
                UpdateCheckboxState(paramValue, paramName);
                
            }
            
            UpdateVisibleParamsPercentText();

            _visibleParams = value;
        }
    }

    public void OnFinalize()
    {
        _repository.Update(_currentCharacterCoefficients);
        _repository.Update(_defaultCharacterCoefficients);
        _vm.PropertyChangedWithValue -= OnPropertyChangedWithValue;
    }

    public WeaponClass GetSelectedWeaponClass()
    {
        return Coefficients.WeaponClass;
    }
    
    private IEnumerable<ItemParams> GetVisibleParams() => _equipmentIndex switch
    {
        CustomEquipmentIndex.Weapon0 => ItemTypes.GetParamsByWeaponClass(Coefficients.WeaponClass),
        CustomEquipmentIndex.Weapon1 => ItemTypes.GetParamsByWeaponClass(Coefficients.WeaponClass),
        CustomEquipmentIndex.Weapon2 => ItemTypes.GetParamsByWeaponClass(Coefficients.WeaponClass),
        CustomEquipmentIndex.Weapon3 => ItemTypes.GetParamsByWeaponClass(Coefficients.WeaponClass),
        CustomEquipmentIndex.Weapon4 => ItemTypes.GetParamsByWeaponClass(Coefficients.WeaponClass),
        CustomEquipmentIndex.Head => ItemTypes.Head,
        CustomEquipmentIndex.Body => ItemTypes.Armor,
        CustomEquipmentIndex.Leg => ItemTypes.Legs,
        CustomEquipmentIndex.Gloves => ItemTypes.Arms,
        CustomEquipmentIndex.Cape => ItemTypes.Capes,
        CustomEquipmentIndex.Horse => ItemTypes.Horse,
        CustomEquipmentIndex.HorseHarness => ItemTypes.HorseHarness,
        _ => throw new ArgumentOutOfRangeException(nameof(_equipmentIndex), _equipmentIndex, null)
    };
    
    public void UpdateCheckboxState(object propertyValue, [CallerMemberName] string? propertyName = null)
    {
        var defaultValue = DefaultCoefficients.GetPropValue($"{propertyName}");
        var comparisonResult = propertyValue switch
        {
            float value => Math.Abs(value - (float) defaultValue) < Tolerance,
            WeaponClass value => value == (WeaponClass) defaultValue,
            _ => throw new ArgumentOutOfRangeException()
        };
        _vm.SetPropertyValue($"{propertyName}IsDefault", comparisonResult);
    }

    private void UpdateVisibleParamsPercentText()
    {
        foreach (var param in VisibleParams)
        {
            var paramName = param.ToString();
            var value = (float) _vm.GetPropertyValue($"{paramName}");
            _vm.SetPropertyValue($"{paramName}PercentText", GetValuePercentText(value));
        }

        string GetValuePercentText(float value)
        {
            var sum = VisibleParams.Sum(param => (float) _vm.GetPropertyValue($"{param}"));

            if (sum == 0) return "0%";

            var resultPercent = Math.Round(value / sum * 100).ToString(CultureInfo.InvariantCulture);

            return $"{resultPercent}%";
        }
    }

    public void DefaultClick()
    {
        foreach (var param in VisibleParams)
        {
            var paramName = param.ToString();
            var value = DefaultCoefficients.GetPropValue($"{paramName}");
            _vm.SetPropertyValue($"{paramName}", value);
        }
    }
    
    public void LockClick()
    {
        foreach (var param in VisibleParams)
        {
            _vm.SetPropertyValue($"{param}", 0);
        }
    }

    public void CloseClick()
    {
        var inventoryScreen = ScreenManager.TopScreen as InventoryGauntletScreen;
        var coefficientsSettingsLayer = inventoryScreen?.Layers.FindLayer<CoefficientsSettingsLayer>();
        inventoryScreen?.RemoveLayer(coefficientsSettingsLayer);
    }

    public void CheckboxClick(string paramName)
    {
        var charactersCoefficients = _repository.ReadAll().Where(p => p.Name != CharacterCoefficients.Default).ToList();
        var newValue = paramName == "WeaponClass" 
            ? Enum.Parse(typeof(WeaponClass), _vm.WeaponClassSelector?.SelectedItem.StringItem!) 
            : _vm.GetPropertyValue($"{paramName}");

        foreach (var charCoefficients in charactersCoefficients)
        {
            var coefficients = _modVM.IsInWarSet
                ? charCoefficients.WarCoefficients[(int) _equipmentIndex]
                : charCoefficients.CivilCoefficients[(int) _equipmentIndex];
            var defaultValue = DefaultCoefficients.GetPropValue($"{paramName}");
            var oldValue = coefficients.GetPropValue($"{paramName}");
            var comparisonResult = oldValue switch
            {
                float value => Math.Abs(value - (float) defaultValue) < Tolerance,
                WeaponClass value => value == (WeaponClass) defaultValue,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (comparisonResult) coefficients.SetPropValue($"{paramName}", newValue);
        }

        DefaultCoefficients.SetPropValue($"{paramName}", newValue);
        UpdateCheckboxState(newValue, paramName);

        charactersCoefficients.ForEach(characterCoefficients => _repository.Update(characterCoefficients));
    }

    public string GetHeaderText(EquipmentIndex equipmentIndex) => equipmentIndex switch
    {
        EquipmentIndex.Weapon0 => new TextObject("{=2RIyK1bp}Weapons") + " 1",
        EquipmentIndex.Weapon1 => new TextObject("{=2RIyK1bp}Weapons") + " 2",
        EquipmentIndex.Weapon2 => new TextObject("{=2RIyK1bp}Weapons") + " 3",
        EquipmentIndex.Weapon3 => new TextObject("{=2RIyK1bp}Weapons") + " 4",
        EquipmentIndex.Weapon4 => new TextObject("{=2RIyK1bp}Weapons") + " 5",
        EquipmentIndex.Head => GameTexts.FindText("str_inventory_helm_slot").ToString(),
        EquipmentIndex.Body => GameTexts.FindText("str_inventory_armor_slot").ToString(),
        EquipmentIndex.Leg => GameTexts.FindText("str_inventory_boot_slot").ToString(),
        EquipmentIndex.Gloves => GameTexts.FindText("str_inventory_glove_slot").ToString(),
        EquipmentIndex.Cape => GameTexts.FindText("str_inventory_cloak_slot").ToString(),
        EquipmentIndex.Horse => GameTexts.FindText("str_inventory_mount_slot").ToString(),
        EquipmentIndex.HorseHarness => GameTexts.FindText("str_inventory_mount_armor_slot").ToString(),
        _ => throw new ArgumentOutOfRangeException(nameof(equipmentIndex), equipmentIndex, null)
    };

}