using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using EquipBestItem.Extensions;
using EquipBestItem.Layers;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using EquipBestItem.ViewModels;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.Models;

internal class CoefficientsSettings
{
    private const float Tolerance = 0.00001f;
    private readonly CoefficientsSettingsVM _vm;
    private readonly CharacterCoefficientsRepository _repository;
    private readonly CustomEquipmentIndex _equipmentIndex;
    private readonly SPInventoryVM _originVM;
    private readonly CharacterCoefficients _currentCharacterCoefficients;
    private readonly CharacterCoefficients _defaultCharacterCoefficients;
    
    internal CoefficientsSettings(CoefficientsSettingsVM vm, CustomEquipmentIndex equipmentIndex, 
        CharacterCoefficientsRepository repository, SPInventoryVM originVM)
    {
        _vm = vm;
        _repository = repository;
        _equipmentIndex = equipmentIndex;
        _originVM = originVM;
        
        _currentCharacterCoefficients = _repository.Read(_originVM.CurrentCharacterName);
        _defaultCharacterCoefficients = _repository.Read(CharacterCoefficients.Default);

        VisibleParams = GetVisibleParams().GetFlags();
    }

    public void LoadValues()
    {
        foreach (var param in VisibleParams)
        {
            var value = Coefficients.GetPropValue($"{param}");
            _vm.SetPropertyValue($"{param}IsHidden", false);
            _vm.SetPropertyValue($"{param}Value", value);
            _vm.SetPropertyValue($"{param}", value);
            UpdateCheckboxState(param, value);
        }

        UpdateVisibleParamsPercentText();
    }

    private Coefficients Coefficients => _originVM.IsInWarSet
        ? _currentCharacterCoefficients.WarCoefficients[(int) _equipmentIndex]
        : _currentCharacterCoefficients.CivilCoefficients[(int) _equipmentIndex];

    private Coefficients DefaultCoefficients => _originVM.IsInWarSet
        ? _defaultCharacterCoefficients.WarCoefficients[(int) _equipmentIndex]
        : _defaultCharacterCoefficients.CivilCoefficients[(int) _equipmentIndex];

    private IEnumerable<ItemParams> VisibleParams { get; }

    public void OnFinalize()
    {
        _repository.Update(_currentCharacterCoefficients);
        _repository.Update(_defaultCharacterCoefficients);
    }
    
    private ItemParams GetVisibleParams()
    {
        return _equipmentIndex switch
        {
            CustomEquipmentIndex.Weapon0 => ItemTypes.Weapon,
            CustomEquipmentIndex.Weapon1 => ItemTypes.Weapon,
            CustomEquipmentIndex.Weapon2 => ItemTypes.Weapon,
            CustomEquipmentIndex.Weapon3 => ItemTypes.Weapon,
            CustomEquipmentIndex.Weapon4 => ItemTypes.Weapon,
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
    
    public void UpdateCheckboxState(ItemParams itemParam, object propertyValue)
    {
        var paramName = itemParam.ToString();
        var defaultValue = DefaultCoefficients.GetPropValue($"{paramName}");
        var comparisonResult = propertyValue switch
        {
            float value => Math.Abs(value - (float) defaultValue) < Tolerance,
            string value => value == (string) defaultValue,
            _ => throw new ArgumentOutOfRangeException()
        };
        _vm.SetPropertyValue($"{paramName}IsDefault", comparisonResult);
    }

    public void CoefficientsUpdate(object value, [CallerMemberName] string? propertyName = null)
    {
        Coefficients.GetType()
            .GetProperty($"{propertyName}", typeof(float))?
            .SetValue(Coefficients, value);
        _originVM.RefreshValues();
    }

    public void UpdateVisibleParamsPercentText(float? propertyValue = null,
        [CallerMemberName] string? propertyName = null)
    {
        foreach (var param in VisibleParams)
        {
            var paramName = param.ToString();
            var value = paramName == propertyName
                ? propertyValue ?? (float) _vm.GetPropertyValue($"{paramName}Value")
                : (float) _vm.GetPropertyValue($"{paramName}Value");
            _vm.SetPropertyValue($"{paramName}PercentText", GetValuePercentText(value));
        }

        string GetValuePercentText(float value)
        {
            var sum = VisibleParams.Sum(param => (float) _vm.GetPropertyValue($"{param}Value"));

            if (sum == 0) return "0%";

            var resultPercent = Math.Round(value / sum * 100).ToString(CultureInfo.InvariantCulture);

            return $"{resultPercent}%";
        }
    }

    public void DefaultClick()
    {
        foreach (var param in VisibleParams)
        {
            var value = DefaultCoefficients.GetPropValue($"{param}");
            _vm.SetPropertyValue($"{param}", value);
            _vm.SetPropertyValue($"{param}Value", value);
        }
    }
    
    public void LockClick()
    {
        foreach (var param in VisibleParams)
        {
            _vm.SetPropertyValue($"{param}Value", 0);
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
        var param = Helper.ParseEnum<ItemParams>(paramName);
        var charactersCoefficients = _repository.ReadAll().Where(p => p.Name != CharacterCoefficients.Default).ToList();
        var newValue = _vm.GetPropertyValue($"{param}");

        foreach (var charCoefficients in charactersCoefficients)
        {
            var coefficients = _originVM.IsInWarSet
                ? charCoefficients.WarCoefficients[(int) _equipmentIndex]
                : charCoefficients.CivilCoefficients[(int) _equipmentIndex];
            var defaultValue = DefaultCoefficients.GetPropValue($"{param}");
            var oldValue = coefficients.GetPropValue($"{param}");
            var comparisonResult = oldValue switch
            {
                float value => Math.Abs(value - (float) defaultValue) < Tolerance,
                string value => value == (string) defaultValue,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (comparisonResult) coefficients.SetPropValue($"{param}", newValue);
        }

        DefaultCoefficients.SetPropValue($"{param}", newValue);
        UpdateCheckboxState(param, newValue);

        charactersCoefficients.ForEach(cc => _repository.Update(cc));
    }
}