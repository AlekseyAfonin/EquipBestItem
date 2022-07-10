using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EquipBestItem.Extensions;
using EquipBestItem.Layers;
using EquipBestItem.Models;
using EquipBestItem.Models.Entities;
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

internal partial class ModSPInventoryVM : ViewModel
{
    private readonly SPInventoryVM _originVM;
    private readonly SPInventoryVMMixin _mixinVM;
    private readonly BestItemManager _bestItemManager;
    private readonly SettingsRepository _settingsRepository;
    private readonly CharacterCoefficientsRepository _coefficientsRepository;
    private SPItemVM?[] _bestItems = new SPItemVM[12];
    
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
        _bestItemManager.EquipBestItem(equipmentIndex, CurrentCharacter, ref _bestItems[(int)equipmentIndex]);
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

    private readonly SemaphoreSlim _semaphore = new(1);
    private (CancellationTokenSource cts, Task task)? _state;

    internal async Task RestartUpdateAsync()
    {
        Task? task;
        
        await _semaphore.WaitAsync();

        try
        {
            if (_state.HasValue)
            {
                _state.Value.cts.Cancel();
                _state.Value.cts.Dispose();

                try
                {
                    await _state.Value.task;
                }
                catch (OperationCanceledException)
                {
                    InformationManager.DisplayMessage(new InformationMessage($"_state exc"));
                }

                _state = null;
            }

            var cts = new CancellationTokenSource();
            task = UpdateAsync(cts.Token);

            _state = (cts, task);
        }
        finally
        {
            _semaphore.Release();
        }

        try
        {
            await task;
        }
        catch (OperationCanceledException)
        {
            //InformationManager.DisplayMessage(new InformationMessage($"RestartUpdateAsync"));
        }
    }

    /// <summary>
    /// Updating the viewmodel when you do something
    /// </summary>
    internal async Task UpdateAsync(CancellationToken token)
    {
        try
        {
            var rightItemList = (bool) _settingsRepository.Read(Settings.IsRightPanelLocked).Value
                ? null
                : _originVM.RightItemListVM;
            var leftItemList = (bool) _settingsRepository.Read(Settings.IsLeftPanelLocked).Value
                ? null
                : _originVM.LeftItemListVM;
            
            for (var equipIndex = EquipmentIndex.WeaponItemBeginSlot;
                 equipIndex < EquipmentIndex.NumEquipmentSetSlots;
                 equipIndex++)
            {
                token.ThrowIfCancellationRequested();

                var equipment = _originVM.IsInWarSet
                    ? CurrentCharacter.FirstBattleEquipment
                    : CurrentCharacter.FirstCivilianEquipment;
                var coefficients = _originVM.IsInWarSet
                    ? _coefficientsRepository.Read(CurrentCharacter.Name.ToString())
                        .WarCoefficients[(int) equipIndex]
                    : _coefficientsRepository.Read(CurrentCharacter.Name.ToString())
                        .CivilCoefficients[(int) equipIndex];
                _bestItems[(int) equipIndex] = await _bestItemManager.GetBestItemAsync(token, coefficients, equipment[equipIndex], equipIndex, rightItemList, leftItemList);
                
                SlotButtonUpdate(equipIndex, _bestItems[(int) equipIndex] is not null);
            }
        }
        catch (OperationCanceledException)
        {
            //InformationManager.DisplayMessage(new InformationMessage($"UpdateAsync exception num starts: {numberOfStarts}"));
            throw;
        }





        void SlotButtonUpdate(EquipmentIndex index, bool value)
        {
            //InformationManager.DisplayMessage(new InformationMessage($"SlotButtonUpdate: {index.ToString()}"));
            switch (index)
            {
                case EquipmentIndex.Head:
                    _mixinVM.IsHeadButtonEnabled = value;
                    break;
                case EquipmentIndex.Cape:
                    _mixinVM.IsCapeButtonEnabled = value;
                    break;
                case EquipmentIndex.Weapon0:
                    _mixinVM.IsWeapon0ButtonEnabled = value;
                    break;
                case EquipmentIndex.Weapon1:
                    _mixinVM.IsWeapon1ButtonEnabled = value;
                    break;
                case EquipmentIndex.Weapon2:
                    _mixinVM.IsWeapon2ButtonEnabled = value;
                    break;
                case EquipmentIndex.Weapon3:
                    _mixinVM.IsWeapon3ButtonEnabled = value;
                    break;
                case EquipmentIndex.Body:
                    _mixinVM.IsBodyButtonEnabled = value;
                    break;
                case EquipmentIndex.Leg:
                    _mixinVM.IsLegButtonEnabled = value;
                    break;
                case EquipmentIndex.Gloves:
                    _mixinVM.IsGlovesButtonEnabled = value;
                    break;
                case EquipmentIndex.Horse:
                    _mixinVM.IsHorseButtonEnabled = value;
                    break;
                case EquipmentIndex.HorseHarness:
                    _mixinVM.IsHorseHarnessButtonEnabled = value;
                    break;
            }
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
            coefficientsSettingsLayer = new CoefficientsSettingsLayer(17, equipmentIndex, _coefficientsRepository, _originVM);
            inventoryScreen?.AddLayer(coefficientsSettingsLayer);
            coefficientsSettingsLayer.InputRestrictions.SetInputRestrictions();
        }

        void CloseCoefficientSettings()
        {
            inventoryScreen?.RemoveLayer(coefficientsSettingsLayer);
        }
    }
}