using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EquipBestItem.Extensions;
using EquipBestItem.Layers;
using EquipBestItem.Models;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using EquipBestItem.UIExtenderEx;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using SharpRepository.XmlRepository;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.ViewModels;

internal class ModSPInventoryVM : ViewModel
{
    private readonly SPInventoryVM _originVM;
    private readonly SPInventoryVMMixin _mixinVM;
    private readonly BestItemManager _bestItemManager;
    private readonly SettingsRepository _settingsRepository;
    private readonly CharacterCoefficientsRepository _coefficientsRepository;
    public SPItemVM?[] BestItems = new SPItemVM[12];
    
    public ModSPInventoryVM(SPInventoryVM originVM, SPInventoryVMMixin mixinVM)
    {
        _originVM = originVM;
        _mixinVM = mixinVM;
        
        XmlRepository<Settings, string> settingsXmlRepository = new(Seeds.DefaultStoragePath);
        Seeds.EnsurePopulated(settingsXmlRepository, Seeds.DefaultSettings);
        _settingsRepository = new SettingsRepository(settingsXmlRepository);

        XmlRepository<CharacterCoefficients, string> charCoefficientsRepository = new(Seeds.DefaultStoragePath);
        Seeds.EnsurePopulated(charCoefficientsRepository, Seeds.DefaultCharacterCoefficients);
        _coefficientsRepository = new CharacterCoefficientsRepository(charCoefficientsRepository);

        _bestItemManager = new BestItemManager(originVM);

        UpdateCharacters();
    }

    private CharacterObject CurrentCharacter { get; set; } = InventoryManager.InventoryLogic.InitialEquipmentCharacter;
    public string CurrentCharacterName => _originVM.CurrentCharacterName;
    public bool IsInWarSet => _originVM.IsInWarSet;
    
    public override void RefreshValues()
    {
        base.RefreshValues();
        InformationManager.DisplayMessage(new InformationMessage($"ModSP Refresh test"));
    }
    
    public override void OnFinalize()
    {
        base.OnFinalize();
    }
    
    public void UpdateCurrentCharacter(CharacterObject currentCharacter)
    {
        CurrentCharacter = currentCharacter;
    }
    
    // Equip buttons left click
    public void ExecuteEquipBestItem(string equipmentIndexName)
    {
        var equipmentIndex = Helper.ParseEnum<EquipmentIndex>(equipmentIndexName);
        _bestItemManager.EquipBestItem(equipmentIndex, CurrentCharacter, ref BestItems[(int)equipmentIndex]);
        _originVM.ExecuteRemoveZeroCounts();
        _originVM.RefreshValues();
        _originVM.GetMethod("UpdateCharacterEquipment");
    }
    
    /// <summary>
    /// Equip buttons right click with parameter
    /// </summary>
    /// <param name="equipmentIndexName">Slot name</param>
    public void ExecuteShowFilterSettings(string equipmentIndexName)
    {
        InformationManager.DisplayMessage(new InformationMessage($"ShowSettings {equipmentIndexName}"));

        var equipmentIndex = Helper.ParseEnum<EquipmentIndex>(equipmentIndexName);

        OpenCloseCoefficientsSettingsLayer(equipmentIndex);
    }

    private CancellationTokenSource? _cts;
    
    /// <summary>
    /// Updating the viewmodel when you do something
    /// </summary>
    internal async Task UpdateBestItemsAsync()
    {
        if (_cts is not null)
        {
            _cts.Cancel();
            _cts = null;
        }

        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        try
        {
            var coefficients = _originVM.IsInWarSet
                ? _coefficientsRepository.Read(CurrentCharacterName)
                    .WarCoefficients
                : _coefficientsRepository.Read(CurrentCharacterName)
                    .CivilCoefficients;
            var rightItems = (bool) _settingsRepository.Read(Settings.IsRightPanelLocked).Value
                ? null
                : _originVM.RightItemListVM;
            var leftItems = (bool) _settingsRepository.Read(Settings.IsLeftPanelLocked).Value
                ? null
                : _originVM.LeftItemListVM;
            
            var bestItems = new SPItemVM?[12];
            for (var index = EquipmentIndex.WeaponItemBeginSlot; index < EquipmentIndex.NumEquipmentSetSlots; index++)
            {
                bestItems[(int) index] = await Task.Run(() => 
                    _bestItemManager.GetBestItem(CurrentCharacter, index, coefficients, rightItems, leftItems), token);

                if (token.IsCancellationRequested) return;
            }
            
            BestItems = bestItems;

            UpdateEquipButtons();
        }
        catch (MBException e)
        {
            Helper.ShowMessage($"{e.Message}");
        }
    }

    private void UpdateEquipButtons()
    {
        for (var index = CustomEquipmentIndex.Weapon0; index <= CustomEquipmentIndex.HorseHarness; index++)
        {
            _mixinVM.SetPropValue($"Is{index}ButtonDisabled", BestItems[(int) index] is null);
            _originVM.OnPropertyChanged($"{index}BestItem");
        }
    }
    

    /// <summary>
    /// Updating the repository with missing characters.
    /// </summary>
    private void UpdateCharacters()
    {
        var defaultCoefficients = _coefficientsRepository.Read(CharacterCoefficients.Default);
        var characters = _coefficientsRepository.ReadAll().Select(p => p.Name).ToArray();
        var roster = InventoryManager.InventoryLogic.RightMemberRoster.GetTroopRoster();
        
        foreach (var rosterElement in roster)
        {
            if (!rosterElement.Character.IsHero) continue;
            
            var charName = rosterElement.Character.Name.ToString();
            
            if (characters.Contains(charName)) continue;
            
            var characterCoefficients = new CharacterCoefficients()
            {
                Name = charName, 
                CivilCoefficients = defaultCoefficients.CivilCoefficients,
                WarCoefficients = defaultCoefficients.WarCoefficients
            };
            _coefficientsRepository.Create(characterCoefficients);
        }
    }

    private void OpenCloseCoefficientsSettingsLayer(EquipmentIndex equipmentIndex)
    {
        var inventoryScreen = ScreenManager.TopScreen as InventoryGauntletScreen;

        var coefficientsSettingsLayer = inventoryScreen?.Layers.FindLayer<CoefficientsSettingsLayer>();

        if (coefficientsSettingsLayer is null)
        {
            OpenCoefficientSettings();
            return;
        }

        if (coefficientsSettingsLayer.EquipmentIndex == equipmentIndex)
        {
            CloseCoefficientSettings();
        }
        else
        {
            CloseCoefficientSettings();
            OpenCoefficientSettings();
        }

        void OpenCoefficientSettings()
        {
            coefficientsSettingsLayer = new CoefficientsSettingsLayer(17, equipmentIndex, _coefficientsRepository, this);
            inventoryScreen?.AddLayer(coefficientsSettingsLayer);
            coefficientsSettingsLayer.InputRestrictions.SetInputRestrictions();
        }

        void CloseCoefficientSettings()
        {
            inventoryScreen?.RemoveLayer(coefficientsSettingsLayer);
        }
    }

    private void EquipCharacter(CharacterObject character)
    {
        var coefficients = IsInWarSet
            ? _coefficientsRepository.Read(character.Name.ToString()).WarCoefficients
            : _coefficientsRepository.Read(character.Name.ToString()).CivilCoefficients;
        var rightItems = (bool) _settingsRepository.Read(Settings.IsRightPanelLocked).Value
            ? null
            : _originVM.RightItemListVM;
        var leftItems = (bool) _settingsRepository.Read(Settings.IsLeftPanelLocked).Value
            ? null
            : _originVM.LeftItemListVM;
        
        for (var index = EquipmentIndex.WeaponItemBeginSlot; index < EquipmentIndex.NumEquipmentSetSlots; index++)
        {
            var bestItem = _bestItemManager.GetBestItem(character, index, coefficients, rightItems, leftItems);

            _bestItemManager.EquipBestItem(index, character, ref bestItem);
        }
    }

    public void EquipCurrentCharacter()
    {
        Stopwatch stopwatch = new Stopwatch();
        
        stopwatch.Start();
        //////////////
        EquipCharacter(CurrentCharacter);
        
        _originVM.ExecuteRemoveZeroCounts();
        _originVM.RefreshValues();
        _originVM.GetMethod("UpdateCharacterEquipment");
        
        stopwatch.Stop();
        Helper.ShowMessage($"EquipCurrentCharacter {stopwatch.ElapsedMilliseconds}ms");
    }

    public void EquipAllCharacters()
    {
        var roster = InventoryManager.InventoryLogic.RightMemberRoster.GetTroopRoster();

        foreach (var rosterElement in roster.Where(rosterElement => rosterElement.Character.IsHero))
        {
            EquipCharacter(rosterElement.Character);
        }
        
        _originVM.ExecuteRemoveZeroCounts();
        _originVM.RefreshValues();
        _originVM.GetMethod("UpdateCharacterEquipment");
    }

    public void SwitchLeftPanelLock()
    {
        var settings = _settingsRepository.Read(Settings.IsLeftPanelLocked);
        settings.Value = !(bool)settings.Value;
        _settingsRepository.Update(settings);
        Task.Run(async () => await UpdateBestItemsAsync());
    }
    
    public void SwitchRightPanelLock()
    {
        var settings = _settingsRepository.Read(Settings.IsRightPanelLocked);
        settings.Value = !(bool)settings.Value;
        _settingsRepository.Update(settings);
        Task.Run(async () => await UpdateBestItemsAsync());
    }

    public bool IsLeftPanelLocked => (bool) _settingsRepository.Read(Settings.IsLeftPanelLocked).Value;
    public bool IsRightPanelLocked => (bool) _settingsRepository.Read(Settings.IsRightPanelLocked).Value;
    
}