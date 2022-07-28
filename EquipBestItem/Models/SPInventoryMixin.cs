using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EquipBestItem.Extensions;
using EquipBestItem.Layers;
using EquipBestItem.Models.Entities;
using EquipBestItem.Models.Enums;
using EquipBestItem.UIExtenderEx;
using EquipBestItem.XmlRepository;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.ScreenSystem;

namespace EquipBestItem.Models;

internal class SPInventoryMixin
{
    private readonly BestItemManager _bestItemManager;
    private readonly CharacterCoefficientsRepository _coefficientsRepository;
    private readonly SPInventoryVMMixin _mixinVM;
    private readonly SPInventoryVM _originVM;
    private readonly SettingsRepository _settingsRepository;
    private readonly IEnumerable<Settings> _settings;

    private CancellationTokenSource? _cts;
    public SPItemVM?[] BestItems = new SPItemVM[12];

    public SPInventoryMixin(SPInventoryVM originVM, SPInventoryVMMixin mixinVM)
    {
        _originVM = originVM;
        _mixinVM = mixinVM;

        XmlRepository<Settings> settingsXmlRepository = new(Seeds.DefaultStoragePath);
        Seeds.EnsurePopulated(settingsXmlRepository, Seeds.DefaultSettings);
        _settingsRepository = new SettingsRepository(settingsXmlRepository);

        XmlRepository<CharacterCoefficients> charCoefficientsRepository = new(Seeds.DefaultStoragePath);
        Seeds.EnsurePopulated(charCoefficientsRepository, Seeds.DefaultCharacterCoefficients);
        _coefficientsRepository = new CharacterCoefficientsRepository(charCoefficientsRepository);

        _settings = _settingsRepository.ReadAll().ToList();
        _bestItemManager = new BestItemManager(originVM);

        UpdateCharacters();
    }

    public CharacterObject CurrentCharacter { get; private set; } = InventoryManager.InventoryLogic.InitialEquipmentCharacter;
    public string CurrentCharacterName => _originVM.CurrentCharacterName;
    public bool IsInWarSet => _originVM.IsInWarSet;

    public bool IsLeftPanelLocked => _settings.First(x => x.Key == Settings.IsLeftPanelLocked).Value;
    public bool IsRightPanelLocked => _settings.First(x => x.Key == Settings.IsRightPanelLocked).Value;
    public bool IsLeftMenuVisible => _settings.First(x => x.Key == Settings.IsLeftMenuVisible).Value;
    public bool IsRightMenuVisible => _settings.First(x => x.Key == Settings.IsRightMenuVisible).Value;
    
    public void UpdateCurrentCharacter(CharacterObject currentCharacter)
    {
        CurrentCharacter = currentCharacter;
    }

    /// <summary>
    ///     Equip buttons left click
    /// </summary>
    /// <param name="equipmentIndexName">Slot name</param>
    public void ExecuteEquipBestItem(string equipmentIndexName)
    {
        var equipmentIndex = Helper.ParseEnum<EquipmentIndex>(equipmentIndexName);
        _bestItemManager.EquipBestItem(equipmentIndex, CurrentCharacter, ref BestItems[(int) equipmentIndex]);
        _originVM.ExecuteRemoveZeroCounts();
        _originVM.RefreshValues();
        _originVM.GetMethod("UpdateCharacterEquipment");
    }

    /// <summary>
    ///     Equip buttons right click with parameter
    /// </summary>
    /// <param name="equipmentIndexName">Slot name</param>
    public void ExecuteShowFilterSettings(string equipmentIndexName)
    {
        var equipmentIndex = Helper.ParseEnum<EquipmentIndex>(equipmentIndexName);

        OpenCloseCoefficientsSettingsLayer(equipmentIndex);
    }

    /// <summary>
    ///     Updating the viewmodel when you do something
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
            var rightItems = _settingsRepository.Read(Settings.IsRightPanelLocked).Value
                ? null
                : _originVM.RightItemListVM;
            var leftItems = _settingsRepository.Read(Settings.IsLeftPanelLocked).Value
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

    /// <summary>
    ///     Equip button visibility update
    /// </summary>
    private void UpdateEquipButtons()
    {
        for (var index = CustomEquipmentIndex.Weapon0; index <= CustomEquipmentIndex.HorseHarness; index++)
        {
            _mixinVM.SetPropValue($"Is{index}ButtonDisabled", BestItems[(int) index] is null);
            _originVM.OnPropertyChanged($"{index}BestItem");
        }
    }

    /// <summary>
    ///     Add missing characters coefficients.
    /// </summary>
    private void UpdateCharacters()
    {
        var defaultCoefficients = _coefficientsRepository.Read(CharacterCoefficients.Default);
        var characters = _coefficientsRepository.ReadAll().Select(p => p.Key).ToArray();
        var roster = InventoryManager.InventoryLogic.RightMemberRoster.GetTroopRoster();

        var charactersCoefficientsToCreate = from rosterElement in roster
            where rosterElement.Character.IsHero
            select rosterElement.Character.Name.ToString()
            into charName
            where !characters.Contains(charName)
            select new CharacterCoefficients
            {
                Key = charName,
                CivilCoefficients = defaultCoefficients.CivilCoefficients,
                WarCoefficients = defaultCoefficients.WarCoefficients
            };
        
        foreach (var characterCoefficients in charactersCoefficientsToCreate)
        {
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
            coefficientsSettingsLayer =
                new CoefficientsSettingsLayer(17, equipmentIndex, _coefficientsRepository, this);
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
        var rightItems = _settingsRepository.Read(Settings.IsRightPanelLocked).Value
            ? null
            : _originVM.RightItemListVM;
        var leftItems = _settingsRepository.Read(Settings.IsLeftPanelLocked).Value
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
        EquipCharacter(CurrentCharacter);

        _originVM.ExecuteRemoveZeroCounts();
        _originVM.RefreshValues();
        _originVM.GetMethod("UpdateCharacterEquipment");
    }

    public void EquipAllCharacters()
    {
        var roster = InventoryManager.InventoryLogic.RightMemberRoster.GetTroopRoster();

        foreach (var rosterElement in roster.Where(rosterElement => rosterElement.Character.IsHero))
            EquipCharacter(rosterElement.Character);

        _originVM.ExecuteRemoveZeroCounts();
        _originVM.RefreshValues();
        _originVM.GetMethod("UpdateCharacterEquipment");
    }

    public void SwitchLeftPanelLock()
    {
        var settings = _settingsRepository.Read(Settings.IsLeftPanelLocked);
        settings.Value = !settings.Value;
        _settingsRepository.Update(settings);
        Task.Run(async () => await UpdateBestItemsAsync());
    }

    public void SwitchRightPanelLock()
    {
        var settings = _settingsRepository.Read(Settings.IsRightPanelLocked);
        settings.Value = !settings.Value;
        _settingsRepository.Update(settings);
        Task.Run(async () => await UpdateBestItemsAsync());
    }

    public void SwitchLeftMenu()
    {
        var settings = _settingsRepository.Read(Settings.IsLeftMenuVisible);
        settings.Value = !settings.Value;
        _settingsRepository.Update(settings);
    }
    
    public void SwitchRightMenu()
    {
        var settings = _settingsRepository.Read(Settings.IsRightMenuVisible);
        settings.Value = !settings.Value;
        _settingsRepository.Update(settings);
    }
}