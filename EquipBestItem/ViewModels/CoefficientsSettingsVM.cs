using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using EquipBestItem.Extensions;
using EquipBestItem.Layers;
using EquipBestItem.Models;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using JetBrains.Annotations;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM : ViewModel
{
    private readonly CharacterCoefficientsRepository _repository;
    private readonly CharacterCoefficients _currentCharacterCoefficients;
    private readonly CharacterCoefficients _defaultCharacterCoefficients;
    private readonly CustomEquipmentIndex _equipmentIndex;
    private readonly SPInventoryVM _originVM;

    internal CoefficientsSettingsVM(EquipmentIndex equipmentIndex, CharacterCoefficientsRepository repository, 
        SPInventoryVM originVM)
    {
        _equipmentIndex = (CustomEquipmentIndex)equipmentIndex;
        _repository = repository;
        _originVM = originVM;
        _headerText = _equipmentIndex.ToString();

        _currentCharacterCoefficients = _repository.Read(_originVM.CurrentCharacterName);
        _defaultCharacterCoefficients = _repository.Read(CharacterCoefficients.Default);

        foreach (var param in VisibleParams.GetFlags())
        {
            SetPropertyValue($"{param}IsHidden", false);
        }
        
        foreach (var param in VisibleParams.GetFlags())
        {
            var value = GetPropertyValue($"{param}");
            SetPropertyValue($"{param}Value", value);
            SetPropertyValue($"{param}", value);
        }
        
        PropertyChangedWithValue += OnPropertyChangedWithValue;
        PropertyChanged += OnPropertyChanged;
    }

    private Coefficients Coefficients => _originVM.IsInWarSet
        ? _currentCharacterCoefficients.WarCoefficients[(int)_equipmentIndex]
        : _currentCharacterCoefficients.CivilCoefficients[(int)_equipmentIndex];

    private Coefficients DefaultCoefficients => _originVM.IsInWarSet 
        ? _defaultCharacterCoefficients.WarCoefficients[(int)_equipmentIndex] 
        : _defaultCharacterCoefficients.CivilCoefficients[(int)_equipmentIndex];

    private ItemParams VisibleParams => _equipmentIndex switch
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
    
    private void UpdatePercentText()
    {
        foreach (var param in VisibleParams.GetFlags())
        {
            var paramValue = (float) GetPropertyValue($"{param}Value");
            var paramValuePercent = GetValuePercentText(paramValue);
            SetPropertyValue($"{param}PercentText", paramValuePercent);
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
    }

    private void OnPropertyChangedWithValue(object sender, PropertyChangedWithValueEventArgs e)
    {

        Enum.TryParse(e.PropertyName, out ItemParams itemParam);

        if (!VisibleParams.HasFlag(itemParam)) return;
        
        Coefficients.GetType()
            .GetProperty($"{e.PropertyName}", typeof(float))?
            .SetValue(Coefficients, e.Value);
        
        _originVM.RefreshValues();
    }

    public override void OnFinalize()
    {
        PropertyChangedWithValue -= OnPropertyChangedWithValue;
        PropertyChanged -= OnPropertyChanged;
        _repository.Update(_currentCharacterCoefficients);
        base.OnFinalize();
    }
    
    public sealed override void RefreshValues()
    {
        base.RefreshValues();
    }
    
    public void ExecuteDefault()
    {
        foreach (var param in VisibleParams.GetFlags())
        {
            var value = GetPropertyValue($"{param}");
            SetPropertyValue($"{param}", value);
            SetPropertyValue($"{param}Value", value);
        }
    }

    public void ExecuteLock()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        foreach (var param in VisibleParams.GetFlags())
        {
            SetPropertyValue($"{param}Value", 0);
            SetPropertyValue($"{param}", 0);
        }
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Helper.ShowMessage($"ExecuteLock: {elapsedMs}");
    }

    public void ExecuteClose()
    {
        var inventoryScreen = ScreenManager.TopScreen as InventoryGauntletScreen;
        var coefficientsSettingsLayer = inventoryScreen?.Layers.FindLayer<CoefficientsSettingsLayer>();
        inventoryScreen?.RemoveLayer(coefficientsSettingsLayer);
    }

    public void ExecuteValueDefault(string paramName)
    {
        InformationManager.DisplayMessage(new InformationMessage($"DefaultButton Click: {paramName}"));
    }

    private string GetValuePercentText(float propertyValue)
    {
        var sum = VisibleParams.GetFlags().Sum(param => (float) GetPropertyValue($"{param}Value"));

        if (sum == 0) return "0%";
        
        var resultPercent = Math.Round(propertyValue / sum * 100).ToString(CultureInfo.InvariantCulture);
        
        return $"{resultPercent}%";
    }
}