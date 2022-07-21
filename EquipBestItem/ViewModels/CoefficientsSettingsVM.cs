using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EquipBestItem.Extensions;
using EquipBestItem.Layers;
using EquipBestItem.Models;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using EquipBestItem.UIExtenderEx;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM : ViewModel
{
    private readonly CoefficientsSettings _model;
    private string _headerText;

    internal CoefficientsSettingsVM(EquipmentIndex equipIndex, CharacterCoefficientsRepository repository,
        ModSPInventoryVM modVM)
    {
        var equipmentIndex = (CustomEquipmentIndex) equipIndex;
        _headerText = equipmentIndex.ToString();
        _model = new CoefficientsSettings(this, equipmentIndex, repository, modVM);
        _model.LoadValues();
    }
    
    public override void OnFinalize()
    {
        _model.OnFinalize();
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
        _model.CloseClick();
    }

    public void ExecuteCheckboxSetDefault(string paramName)
    {
        _model.CheckboxClick(paramName);
    }

    
}