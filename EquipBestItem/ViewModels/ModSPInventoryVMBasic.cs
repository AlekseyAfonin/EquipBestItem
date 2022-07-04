using System;
using System.Collections.Generic;
using System.Linq;
using EquipBestItem.Models;
using EquipBestItem.Models.Entities;
using EquipBestItem.UIExtenderEx;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using SharpRepository.XmlRepository;
using TaleWorlds.Engine;

namespace EquipBestItem.ViewModels;

internal partial class ModSPInventoryVM : ViewModel
{
    private readonly SPInventoryVM _originVM;
    private readonly SPInventoryVMMixin _mixinVM;
    private readonly BestItemManager _bestItemManager;

    private readonly SettingsRepository _settingsReposotiry;
    private readonly CharacterCoefficientsRepository _coefficientsRepository;
    
    private CharacterObject _currentCharacter { get; set; } = InventoryManager.InventoryLogic.InitialEquipmentCharacter;
    
    public ModSPInventoryVM(SPInventoryVM originVM, SPInventoryVMMixin mixinVM)
    {
        _originVM = originVM;
        _mixinVM = mixinVM;
        
        XmlRepository<Settings, string> settingsXmlReposotiry = new(Seeds.DefaultStoragePath);
        Seeds.EnsurePopulated(settingsXmlReposotiry, Seeds.DefaultSettings);
        _settingsReposotiry = new(settingsXmlReposotiry);

        XmlRepository<CharacterCoefficients, string> charCoefficientsRepository = new(Seeds.DefaultStoragePath);
        Seeds.EnsurePopulated(charCoefficientsRepository, Seeds.DefaultCharacterCoefficients);
        _coefficientsRepository = new(charCoefficientsRepository);

        _bestItemManager = new BestItemManager(originVM);

        UpdateCharacters();
    }

    private readonly SPItemVM?[] _bestItems = new SPItemVM[12];
    
    internal void Update()
    {
        InformationManager.DisplayMessage(new InformationMessage($"Update"));
        var rightItemList = (bool)_settingsReposotiry.Read(Settings.IsRightPanelLocked).Value ? null : _originVM.RightItemListVM;
        var leftItemList = (bool)_settingsReposotiry.Read(Settings.IsLeftPanelLocked).Value ? null : _originVM.LeftItemListVM;

        for (var equipIndex = EquipmentIndex.WeaponItemBeginSlot; equipIndex < EquipmentIndex.NumEquipmentSetSlots; equipIndex++)
        {
            var equipment = _originVM.IsInWarSet
                ? _currentCharacter.FirstBattleEquipment
                : _currentCharacter.FirstCivilianEquipment;
            var coefficients = _originVM.IsInWarSet
                ? _coefficientsRepository.Read(_currentCharacter.Name.ToString()).WarCoefficients[(int)equipIndex]
                : _coefficientsRepository.Read(_currentCharacter.Name.ToString()).CivilCoefficients[(int)equipIndex];
            _bestItems[(int)equipIndex] = BestItemManager.GetBestItem(coefficients, equipment[equipIndex], equipIndex, rightItemList, leftItemList);
            SlotButtonUpdate(equipIndex, _bestItems[(int)equipIndex] is not null);
        }
    }

    private void SlotButtonUpdate(EquipmentIndex index, bool value)
    {
        InformationManager.DisplayMessage(new InformationMessage($"SlotButtonUpdate: {index.ToString()}"));
        switch (index)
        {
            case EquipmentIndex.Head:
                _mixinVM.IsHeadButtonEnabled = value;
                break;
            case EquipmentIndex.Cape:
                _mixinVM.IsCapeButtonEnabled = value;
                break;
            case EquipmentIndex.WeaponItemBeginSlot:
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
            default:
                break;
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

    public void ExecuteShowFilterSettings(string equipmentIndexName)
    {
        InformationManager.DisplayMessage(new InformationMessage($"ShowSettings {equipmentIndexName}"));

        #region future code
        //var equipmentIndex = Helper.ParseEnum<EquipmentIndex>(equipmentIndexName);

        //switch (equipmentIndex)
        //{
        //    case EquipmentIndex.Head :
        //        InformationManager.DisplayMessage(new InformationMessage($"ShowSettings EquipmentIndex.Head "));
        //        break;
        //    case EquipmentIndex.Cape :
        //        InformationManager.DisplayMessage(new InformationMessage($"ShowSettings EquipmentIndex.Cape"));
        //        break;
        //    case EquipmentIndex.None:
        //        break;
        //    case EquipmentIndex.WeaponItemBeginSlot:
        //        break;
        //    case EquipmentIndex.Weapon1:
        //        break;
        //    case EquipmentIndex.Weapon2:
        //        break;
        //    case EquipmentIndex.Weapon3:
        //        break;
        //    case EquipmentIndex.Weapon4:
        //        break;
        //    case EquipmentIndex.Body:
        //        break;
        //    case EquipmentIndex.Leg:
        //        break;
        //    case EquipmentIndex.Gloves:
        //        break;
        //    case EquipmentIndex.ArmorItemEndSlot:
        //        break;
        //    case EquipmentIndex.HorseHarness:
        //        break;
        //    case EquipmentIndex.NumEquipmentSetSlots:
        //        break;
        //    default:
        //        InformationManager.DisplayMessage(new InformationMessage($"ShowSettings wrong EquipmentIndex"));
        //        break;
        //}
        #endregion
    }

    public void ExecuteEquipBestItem(string equipmentIndexName)
    {
        var equipmentIndex = Helper.ParseEnum<EquipmentIndex>(equipmentIndexName);
        _bestItemManager.EquipBestItem(equipmentIndex, _currentCharacter, ref _bestItems[(int)equipmentIndex]);
    }
    
    public void UpdateCurrentCharacter(CharacterObject currentCharacter)
    {
        _currentCharacter = currentCharacter;
    }

    public override void RefreshValues()
    {
        base.RefreshValues();
        InformationManager.DisplayMessage(new InformationMessage($"ModSP Refresh test"));
    }

    public override void OnFinalize()
    {
        base.OnFinalize();
    }
    
    
}