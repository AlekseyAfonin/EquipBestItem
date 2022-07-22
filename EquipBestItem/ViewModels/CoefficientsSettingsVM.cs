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
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM : ViewModel
{
    private readonly CoefficientsSettings _model;
    private string _headerTextObject = new TextObject("{=equipbestitem_head_header}Head slot coefficients").ToString();
    private string _headerText;

    internal CoefficientsSettingsVM(EquipmentIndex equipIndex, CharacterCoefficientsRepository repository,
        ModSPInventoryVM modVM)
    {
        var equipmentIndex = (CustomEquipmentIndex) equipIndex;
        _model = new CoefficientsSettings(this, equipmentIndex, repository, modVM);
        _model.LoadValues();
        _headerText = _model.GetHeaderText(equipIndex);
    }

    public override void RefreshValues()
    {
        base.RefreshValues();
        
            
    }
    
    [DataSourceProperty] public HintViewModel ButtonDefaultHint { get; } = new (new TextObject("{=ebi_hint_default}Reset to default values"));
    [DataSourceProperty] public HintViewModel ButtonLockHint { get; } = new (new TextObject("{=ebi_hint_lock}Disable search for this slot"));
    [DataSourceProperty] public HintViewModel CheckboxHint { get; } = new(new TextObject("{=ebi_hint_checkbox}Set to default value"));
    [DataSourceProperty] public HintViewModel PercentHint { get; } = new (new TextObject("{=ebi_hint_percent}How much does the parameter affect the value of the item"));
    [DataSourceProperty] public string ButtonDefaultText { get; } =  new TextObject("{=ebi_default}Default").ToString();
    [DataSourceProperty] public string ButtonLockText { get; } =  new TextObject("{=ebi_lock}Lock").ToString();
    
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