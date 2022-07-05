using System.Collections.Generic;
using EquipBestItem.Extensions;
using EquipBestItem.Layers;
using EquipBestItem.Models;
using SandBox.GauntletUI;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.ViewModels;

internal class CoefficientsSettingsVM : ViewModel
{
    private readonly CharacterCoefficientsRepository _repository;
    private readonly EquipmentIndex _equipmentIndex;
    
    internal CoefficientsSettingsVM(EquipmentIndex equipmentIndex, CharacterCoefficientsRepository repository)
    {
        _equipmentIndex = equipmentIndex;
        _repository = repository;
        _headerText = equipmentIndex.ToString();
    }

    private string _headerText;
    
    [DataSourceProperty]
    public string HeaderText
    {
        get => _headerText;
        set
        {
            if (_headerText == value) return;
            _headerText = value;
            OnPropertyChanged();
        }
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

    public override void OnFinalize()
    {
        InformationManager.DisplayMessage(new InformationMessage($"OnFinalize"));
        base.OnFinalize();
    }
}