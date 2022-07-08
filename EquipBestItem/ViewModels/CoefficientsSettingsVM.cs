using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    private readonly CustomEquipmentIndex _equipmentIndex;
    private readonly SPInventoryVM _originVM;

    internal CoefficientsSettingsVM(EquipmentIndex equipmentIndex, CharacterCoefficientsRepository repository, 
        SPInventoryVM originVM)
    {
        _equipmentIndex = (CustomEquipmentIndex)equipmentIndex;
        _repository = repository;
        _originVM = originVM;
        _headerText = _equipmentIndex.ToString();

        foreach (var param in GetEnabledParams(_equipmentIndex).GetFlags())
        {
            this.GetType()
                .GetProperty($"{param}IsHidden", typeof(bool))?
                .SetValue(this, false);
        }
        
        var coefficients = _originVM.IsInWarSet
            ? _repository.Read(_originVM.CurrentCharacterName).WarCoefficients[(int)_equipmentIndex]
            : _repository.Read(_originVM.CurrentCharacterName).CivilCoefficients[(int)_equipmentIndex];
        
        foreach (var param in GetEnabledParams(_equipmentIndex).GetFlags())
        {
            var value = coefficients.GetType()
                .GetProperty($"{param}", typeof(float))?
                .GetValue(coefficients);
            
            this.GetType()
                .GetProperty($"{param}Value", typeof(float))?
                .SetValue(this, value);
            
            this.GetType()
                .GetProperty($"{param}", typeof(float))?
                .SetValue(this, value);
        }
        
        PropertyChangedWithValue += OnPropertyChangedWithValue;
        PropertyChanged += OnPropertyChanged;
    }

    private void UpdatePercentText()
    {
        foreach (var param in GetEnabledParams(_equipmentIndex).GetFlags())
        {
            OnPropertyChanged($"{param}PercentText");
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        foreach (var param in GetEnabledParams(_equipmentIndex).GetFlags())
        {
            if (e.PropertyName != $"{param}Value") continue;

            UpdatePercentText();
        }
    }

    private void OnPropertyChangedWithValue(object sender, PropertyChangedWithValueEventArgs e)
    {

        Enum.TryParse(e.PropertyName, out ItemParams itemParam);

        if (!GetEnabledParams(_equipmentIndex).HasFlag(itemParam)) return;

        var characterCoefficients = _repository.Read(_originVM.CurrentCharacterName);

        var coefficients = _originVM.IsInWarSet
            ? characterCoefficients.WarCoefficients
            : characterCoefficients.CivilCoefficients;

        var slotCoefficients = coefficients[(int) _equipmentIndex];

        slotCoefficients.GetType()
            .GetProperty($"{e.PropertyName}", typeof(float))?
            .SetValue(slotCoefficients, e.Value);

        _repository.Update(characterCoefficients);

        _originVM.RefreshValues();
    }

    private ItemParams GetEnabledParams(CustomEquipmentIndex equipmentIndex) => equipmentIndex switch
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
        _ => throw new ArgumentOutOfRangeException(nameof(equipmentIndex), equipmentIndex, null)
    };

    public override void OnFinalize()
    {
        InformationManager.DisplayMessage(new InformationMessage($"OnFinalize"));
        PropertyChangedWithValue -= OnPropertyChangedWithValue;
        PropertyChanged -= OnPropertyChanged;
        base.OnFinalize();
    }
    
    public sealed override void RefreshValues()
    {
        base.RefreshValues();
    }
    
    public void ExecuteDefault()
    {
        InformationManager.DisplayMessage(new InformationMessage($"ExecuteDefault"));
    }

    public void ExecuteLock()
    {
        InformationManager.DisplayMessage(new InformationMessage($"ExecuteLock"));
    }

    public void ExecuteClose()
    {
        InformationManager.DisplayMessage(new InformationMessage($"ExecuteClose"));
        var inventoryScreen = ScreenManager.TopScreen as InventoryGauntletScreen;
        var coefficientsSettingsLayer = inventoryScreen?.Layers.FindLayer<CoefficientsSettingsLayer>();
        inventoryScreen?.RemoveLayer(coefficientsSettingsLayer);
        OnFinalize();
    }

    public void ExecuteValueDefault(string paramName)
    {
        InformationManager.DisplayMessage(new InformationMessage($"DefaultButton Click: {paramName}"));
    }

    private string GetValuePercentText(float propertyValue)
    {
        var sum = 0f;
        
        foreach (var param in GetEnabledParams(_equipmentIndex).GetFlags())
        {
            sum += Convert.ToSingle(GetType().GetProperty($"{param}Value", typeof(float))?.GetValue(this));
        }
        
        if (sum == 0) return "0%";
        
        var resultPercent = Math.Round(propertyValue / sum * 100).ToString(CultureInfo.InvariantCulture);
        
        return $"{resultPercent}%";
    }
}