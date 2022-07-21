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
using TaleWorlds.Library;
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

        VisibleParams = GetVisibleParams();
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

        UpdateVisibleParamsPercentText();

        //_vm.PropertyChanged += OnPropertyChanged;
        _vm.PropertyChangedWithValue += OnPropertyChangedWithValue;
    }

    private void OnPropertyChangedWithValue(object sender, PropertyChangedWithValueEventArgs e)
    {
        Coefficients.SetPropValue($"{e.PropertyName}", e.Value);
        UpdateVisibleParamsPercentText();
        Task.Run(async () => await _modVM.UpdateBestItemsAsync());
    }

    private Coefficients Coefficients => _modVM.IsInWarSet
        ? _currentCharacterCoefficients.WarCoefficients[(int) _equipmentIndex]
        : _currentCharacterCoefficients.CivilCoefficients[(int) _equipmentIndex];

    private Coefficients DefaultCoefficients => _modVM.IsInWarSet
        ? _defaultCharacterCoefficients.WarCoefficients[(int) _equipmentIndex]
        : _defaultCharacterCoefficients.CivilCoefficients[(int) _equipmentIndex];

    private IEnumerable<ItemParams> VisibleParams { get; }

    public void OnFinalize()
    {
        _repository.Update(_currentCharacterCoefficients);
        _repository.Update(_defaultCharacterCoefficients);
        _vm.PropertyChangedWithValue -= OnPropertyChangedWithValue;
    }
    
    private ItemParams[] GetVisibleParams()
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
    
    public void UpdateCheckboxState(object propertyValue, [CallerMemberName] string? propertyName = null)
    {
        var defaultValue = DefaultCoefficients.GetPropValue($"{propertyName}");
        var comparisonResult = propertyValue switch
        {
            float value => Math.Abs(value - (float) defaultValue) < Tolerance,
            string value => value == (string) defaultValue,
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
        var newValue = _vm.GetPropertyValue($"{paramName}");

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
                string value => value == (string) defaultValue,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (comparisonResult) coefficients.SetPropValue($"{paramName}", newValue);
        }

        DefaultCoefficients.SetPropValue($"{paramName}", newValue);
        UpdateCheckboxState(newValue, paramName);

        charactersCoefficients.ForEach(characterCoefficients => _repository.Update(characterCoefficients));
    }
}