using System;
using System.Collections.Generic;
using System.Globalization;
using EquipBestItem.Extensions;
using EquipBestItem.Layers;
using EquipBestItem.Models;
using EquipBestItem.Models.Enums;
using JetBrains.Annotations;
using SandBox.GauntletUI;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM : ViewModel
{
    private readonly CharacterCoefficientsRepository _repository;
    private readonly CustomEquipmentIndex _equipmentIndex;
    
    internal CoefficientsSettingsVM(EquipmentIndex equipmentIndex, CharacterCoefficientsRepository repository)
    {
        _equipmentIndex = (CustomEquipmentIndex)equipmentIndex;
        _repository = repository;
        _headerText = _equipmentIndex.ToString();

        foreach (var param in GetEnabledParams(_equipmentIndex).GetFlags())
        {
            GetType().GetProperty($"{param.ToString()}IsHidden", typeof(bool))?.SetValue(this, false);
        }
    }

    private static ItemParams GetEnabledParams(CustomEquipmentIndex equipmentIndex) => equipmentIndex switch
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
    }

    public void ExecuteValueDefault(string paramName)
    {
        InformationManager.DisplayMessage(new InformationMessage($"DefaultButton Click: {paramName}"));
    }
    
    private void UpdateArmorProperties()
    {
        OnPropertyChanged(nameof(HeadArmorValueText));
        OnPropertyChanged(nameof(BodyArmorValueText));
        OnPropertyChanged(nameof(LegArmorValueText));
        OnPropertyChanged(nameof(ArmArmorValueText));
        OnPropertyChanged(nameof(WeightValueText));
    }

    private string GetArmorValuePercentText(float propertyValue)
    {
        var sum = Math.Abs(HeadArmorValue) +
                  Math.Abs(BodyArmorValue) +
                  Math.Abs(LegArmorValue) +
                  Math.Abs(ArmArmorValue) +
                  Math.Abs(WeightValue);
        
        if (sum == 0) return "0%";
        
        var resultPercent = Math.Round(propertyValue / sum * 100).ToString(CultureInfo.InvariantCulture);
        
        return $"{resultPercent}%";
    }
}