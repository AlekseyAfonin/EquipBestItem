using System;
using System.Collections;
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
    private readonly CharacterCoefficients _currentCharacterCoefficients;
    private readonly CharacterCoefficients _defaultCharacterCoefficients;
    private readonly CustomEquipmentIndex _equipmentIndex;
    private readonly SPInventoryMixin _mixin;
    private readonly CharacterCoefficientsRepository _repository;
    private readonly CoefficientsSettingsVM _vm;

    private ItemParams[] _visibleParams;

    internal CoefficientsSettings(CoefficientsSettingsVM vm, CustomEquipmentIndex equipmentIndex,
        CharacterCoefficientsRepository repository, SPInventoryMixin mixin)
    {
        _vm = vm;
        _repository = repository;
        _equipmentIndex = equipmentIndex;
        _mixin = mixin;

        _currentCharacterCoefficients = _repository.Read(_mixin.CurrentCharacterName);
        _defaultCharacterCoefficients = _repository.Read(CharacterCoefficients.Default);

        _visibleParams = GetVisibleParams();
    }

    private Coefficients Coefficients => _mixin.IsInWarSet
        ? _currentCharacterCoefficients.WarCoefficients[(int) _equipmentIndex]
        : _currentCharacterCoefficients.CivilCoefficients[(int) _equipmentIndex];

    private Coefficients DefaultCoefficients => _mixin.IsInWarSet
        ? _defaultCharacterCoefficients.WarCoefficients[(int) _equipmentIndex]
        : _defaultCharacterCoefficients.CivilCoefficients[(int) _equipmentIndex];

    public ItemParams[] VisibleParams
    {
        get => _visibleParams;
        set
        {
            if (Equals(value, _visibleParams)) return;

            foreach (var param in _visibleParams) _vm.SetPropertyValue($"{param}IsHidden", true);

            foreach (var param in value)
            {
                var paramName = param.ToString();
                _vm.SetPropertyValue($"{paramName}IsHidden", false);
                var paramValue = Coefficients.GetPropValue($"{paramName}");
                _vm.SetPropertyValue($"{paramName}", paramValue);
                UpdateCheckboxState(paramValue, paramName);
            }
            _visibleParams = value;
            UpdateParamText(_visibleParams);
            UpdateVisibleParamsPercentText();
        }
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
            
            _vm.WeaponClass = GetWeaponClass();
        }

        UpdateVisibleParamsPercentText();
        UpdateParamText(VisibleParams);

        _vm.PropertyChangedWithValue += OnPropertyChangedWithValue;
    }
    
    private void UpdateParamText(ItemParams[] visibleParams)
    {
        if (visibleParams.SequenceEqual(ItemTypes.Ammo) || visibleParams.SequenceEqual(ItemTypes.Thrown))
            _vm.MaxDataValueText = new TextObject("{=05fdfc6e238429753ef282f2ce97c1f8}Stack Amount: ").ToString(); 
        if (visibleParams.SequenceEqual(ItemTypes.Bow) || visibleParams.SequenceEqual(ItemTypes.Crossbow) || visibleParams.SequenceEqual(ItemTypes.Shield))
            _vm.ThrustSpeedText = new TextObject("{=74dc1908cb0b990e80fb977b5a0ef10d}Speed: ").ToString();
        if (visibleParams.SequenceEqual(ItemTypes.Crossbow))
            _vm.MaxDataValueText = new TextObject("{=6adabc1f82216992571c3e22abc164d7}Ammo Limit: ").ToString();  
        if (visibleParams.SequenceEqual(ItemTypes.Shield))
            _vm.MaxDataValueText = new TextObject("{=aCkzVUCR}Hit Points: ").ToString(); 
        if (visibleParams.SequenceEqual(ItemTypes.MeleeWeapon))
            _vm.ThrustSpeedText = new TextObject("{=VPYazFVH}Thrust Speed: ").ToString();
    }

    private void OnPropertyChangedWithValue(object sender, PropertyChangedWithValueEventArgs e)
    {
        Coefficients.SetPropValue($"{e.PropertyName}", e.Value);
        UpdateVisibleParamsPercentText();
        Task.Run(async () => await _mixin.UpdateBestItemsAsync());
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

    public WeaponClass GetWeaponClass()
    {
        return Coefficients.WeaponClass == WeaponClass.Undefined
            ? _mixin.CurrentCharacter.Equipment[(int) _equipmentIndex].Item?.PrimaryWeapon?.WeaponClass ??
              Coefficients.WeaponClass
            : Coefficients.WeaponClass;
    }

    public ItemParams[] GetVisibleParams()
    {
        return _equipmentIndex switch
        {
            <= CustomEquipmentIndex.Weapon4 => ItemTypes.GetParamsByWeaponClass(GetWeaponClass()),
            CustomEquipmentIndex.Head => ItemTypes.Head,
            CustomEquipmentIndex.Body => ItemTypes.Armor,
            CustomEquipmentIndex.Leg => ItemTypes.Legs,
            CustomEquipmentIndex.Gloves => ItemTypes.Arms,
            CustomEquipmentIndex.Cape => ItemTypes.Capes,
            CustomEquipmentIndex.Horse => ItemTypes.Horse,
            CustomEquipmentIndex.HorseHarness => ItemTypes.HorseHarness,
            _ => throw new ArgumentOutOfRangeException(nameof(_equipmentIndex), _equipmentIndex, null)
        };
    }

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
        foreach (var param in VisibleParams) _vm.SetPropertyValue($"{param}", 0);
    }

    public static void CloseClick()
    {
        if (ScreenManager.TopScreen is not InventoryGauntletScreen inventoryScreen) return;
        if (inventoryScreen.Layers.FindLayer<CoefficientsSettingsLayer>() is not ScreenLayer coefficientsSettingsLayer) 
            return;
        inventoryScreen.RemoveLayer(coefficientsSettingsLayer);
    }

    public void CheckboxClick(string paramName)
    {
        var charactersCoefficients = _repository.ReadAll().Where(p => p.Key != CharacterCoefficients.Default).ToList();
        var newValue = paramName == "WeaponClass"
            ? (WeaponClass) _vm.WeaponClassSelector.SelectedIndex
            : _vm.GetPropertyValue($"{paramName}");

        foreach (var charCoefficients in charactersCoefficients)
        {
            var coefficients = _mixin.IsInWarSet
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

    public static string GetHeaderText(EquipmentIndex equipmentIndex)
    {
        return equipmentIndex switch
        {
            var index and < EquipmentIndex.Head => $"{new TextObject("{=2RIyK1bp}Weapons")} {(int)index + 1}",
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
}