using Helpers;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem
{
    class MainViewModel : ViewModel
    {
        private InventoryLogic _inventoryLogic;
        private CharacterSettings _characterSettings;
        private CharacterObject _currentCharacter;
        private SPInventoryVM _inventory;
        private Equipment _bestLeftEquipment;
        private Equipment _bestRightEquipment;

        #region DataSourcePropertys

        private bool _isHelmButtonEnabled;

        [DataSourceProperty]
        public bool IsHelmButtonEnabled
        {
            get => _isHelmButtonEnabled;
            set
            {
                if (_isHelmButtonEnabled != value)
                {
                    _isHelmButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isCloakButtonEnabled;

        [DataSourceProperty]
        public bool IsCloakButtonEnabled
        {
            get => _isCloakButtonEnabled;
            set
            {
                if (_isCloakButtonEnabled != value)
                {
                    _isCloakButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isArmorButtonEnabled;

        [DataSourceProperty]
        public bool IsArmorButtonEnabled
        {
            get => _isArmorButtonEnabled;
            set
            {
                if (_isArmorButtonEnabled != value)
                {
                    _isArmorButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isGloveButtonEnabled;

        [DataSourceProperty]
        public bool IsGloveButtonEnabled
        {
            get => _isGloveButtonEnabled;
            set
            {
                if (_isGloveButtonEnabled != value)
                {
                    _isGloveButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isBootButtonEnabled;

        [DataSourceProperty]
        public bool IsBootButtonEnabled
        {
            get => _isBootButtonEnabled;
            set
            {
                if (_isBootButtonEnabled != value)
                {
                    _isBootButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isMountButtonEnabled;

        [DataSourceProperty]
        public bool IsMountButtonEnabled
        {
            get => _isMountButtonEnabled;
            set
            {
                if (_isMountButtonEnabled != value)
                {
                    _isMountButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isHarnessButtonEnabled;

        [DataSourceProperty]
        public bool IsHarnessButtonEnabled
        {
            get => _isHarnessButtonEnabled;
            set
            {
                if (_isHarnessButtonEnabled != value)
                {
                    _isHarnessButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon1ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon1ButtonEnabled
        {
            get => _isWeapon1ButtonEnabled;
            set
            {
                if (_isWeapon1ButtonEnabled != value)
                {
                    _isWeapon1ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon2ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon2ButtonEnabled
        {
            get => _isWeapon2ButtonEnabled;
            set
            {
                if (_isWeapon2ButtonEnabled != value)
                {
                    _isWeapon2ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon3ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon3ButtonEnabled
        {
            get => _isWeapon3ButtonEnabled;
            set
            {
                if (_isWeapon3ButtonEnabled != value)
                {
                    _isWeapon3ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isWeapon4ButtonEnabled;

        [DataSourceProperty]
        public bool IsWeapon4ButtonEnabled
        {
            get => _isWeapon4ButtonEnabled;
            set
            {
                if (_isWeapon4ButtonEnabled != value)
                {
                    _isWeapon4ButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEquipCurrentCharacterButtonEnabled;
        [DataSourceProperty]
        public bool IsEquipCurrentCharacterButtonEnabled
        {
            get => _isEquipCurrentCharacterButtonEnabled;
            set
            {
                if (_isEquipCurrentCharacterButtonEnabled != value)
                {
                    _isEquipCurrentCharacterButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isEnabledEquipAllButton;
        [DataSourceProperty]
        public bool IsEnabledEquipAllButton
        {
            get => _isEnabledEquipAllButton;
            set
            {
                if (_isEnabledEquipAllButton != value)
                {
                    _isEnabledEquipAllButton = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion DataSourcePropertys

        public MainViewModel()
        {
            _inventoryLogic = InventoryManager.MyInventoryLogic;
            _inventory = InventoryBehavior.Inventory;
        }

        public override void RefreshValues()
        {
            base.RefreshValues();

            _currentCharacter = GetCharacterByName(_inventory.CurrentCharacterName);
            _characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(_currentCharacter.Name.ToString());

            Equipment equipment = _inventory.IsInWarSet ? _currentCharacter.FirstBattleEquipment : _currentCharacter.FirstCivilianEquipment;
            _bestLeftEquipment = new Equipment();
            _bestRightEquipment = new Equipment();

            for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
            {
                if (equipment[equipmentIndex].IsEmpty && equipmentIndex < EquipmentIndex.NonWeaponItemBeginSlot ||
                    equipment[EquipmentIndex.Horse].IsEmpty && equipmentIndex == EquipmentIndex.HorseHarness)
                    continue;

                EquipmentElement bestLeftEquipmentElement;
                EquipmentElement bestRightEquipmentElement;

                if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
                {
                    bestLeftEquipmentElement = GetBetterItemFromSide(_inventory.LeftItemListVM, equipment[equipmentIndex], equipmentIndex, !_inventory.IsInWarSet, _currentCharacter);
                }
                if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
                {
                    bestRightEquipmentElement = GetBetterItemFromSide(_inventory.RightItemListVM, equipment[equipmentIndex], equipmentIndex, !_inventory.IsInWarSet, _currentCharacter);
                }

                if (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null)
                    if (ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex) > ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex))
                    {
                        _bestLeftEquipment[equipmentIndex] = bestLeftEquipmentElement;
                    }
                    else
                    {
                        _bestRightEquipment[equipmentIndex] = bestRightEquipmentElement;
                    }
            }

            IsHelmButtonEnabled = (_bestLeftEquipment[EquipmentIndex.Head].IsEmpty && _bestRightEquipment[EquipmentIndex.Head].IsEmpty) ? false : true;
            IsCloakButtonEnabled = (_bestLeftEquipment[EquipmentIndex.Cape].IsEmpty && _bestRightEquipment[EquipmentIndex.Cape].IsEmpty) ? false : true;
            IsArmorButtonEnabled = (_bestLeftEquipment[EquipmentIndex.Body].IsEmpty && _bestRightEquipment[EquipmentIndex.Body].IsEmpty) ? false : true;
            IsGloveButtonEnabled = (_bestLeftEquipment[EquipmentIndex.Gloves].IsEmpty && _bestRightEquipment[EquipmentIndex.Gloves].IsEmpty) ? false : true;
            IsBootButtonEnabled = (_bestLeftEquipment[EquipmentIndex.Leg].IsEmpty && _bestRightEquipment[EquipmentIndex.Leg].IsEmpty) ? false : true;
            IsMountButtonEnabled = (_bestLeftEquipment[EquipmentIndex.Horse].IsEmpty && _bestRightEquipment[EquipmentIndex.Horse].IsEmpty) ? false : true;
            IsHarnessButtonEnabled = (_bestLeftEquipment[EquipmentIndex.HorseHarness].IsEmpty && _bestRightEquipment[EquipmentIndex.HorseHarness].IsEmpty) ? false : true;
            IsWeapon1ButtonEnabled = (_bestLeftEquipment[EquipmentIndex.Weapon0].IsEmpty && _bestRightEquipment[EquipmentIndex.Weapon0].IsEmpty) ? false : true;
            IsWeapon2ButtonEnabled = (_bestLeftEquipment[EquipmentIndex.Weapon1].IsEmpty && _bestRightEquipment[EquipmentIndex.Weapon1].IsEmpty) ? false : true;
            IsWeapon3ButtonEnabled = (_bestLeftEquipment[EquipmentIndex.Weapon2].IsEmpty && _bestRightEquipment[EquipmentIndex.Weapon2].IsEmpty) ? false : true;
            IsWeapon4ButtonEnabled = (_bestLeftEquipment[EquipmentIndex.Weapon3].IsEmpty && _bestRightEquipment[EquipmentIndex.Weapon3].IsEmpty) ? false : true;

            for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
            {
                if (_bestLeftEquipment[equipmentIndex].IsEmpty && _bestRightEquipment[equipmentIndex].IsEmpty)
                    IsEquipCurrentCharacterButtonEnabled = false;
                else
                {
                    IsEquipCurrentCharacterButtonEnabled = true;
                    break;
                }
            }

#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("MainViewModel RefreshValues()"));
#endif
        }

        public void EquipEveryCharacter()
        {
            foreach (TroopRosterElement rosterElement in _inventoryLogic.RightMemberRoster)
            {
                if (rosterElement.Character.IsHero)
                    EquipCharacter(rosterElement.Character);
            }
        }

        public void EquipCharacterEquipment(CharacterObject character, Equipment equipment, bool isCivilian)
        {
            for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
            {
                if (equipment[equipmentIndex].IsEmpty && equipmentIndex < EquipmentIndex.NonWeaponItemBeginSlot ||
                    equipment[EquipmentIndex.Horse].IsEmpty && equipmentIndex == EquipmentIndex.HorseHarness)
                    continue;

                EquipmentElement bestLeftEquipmentElement;
                EquipmentElement bestRightEquipmentElement;

                if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
                {
                    bestLeftEquipmentElement = GetBetterItemFromSide(_inventory.LeftItemListVM, equipment[equipmentIndex], equipmentIndex, isCivilian, character);
                }
                if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
                {
                    bestRightEquipmentElement = GetBetterItemFromSide(_inventory.RightItemListVM, equipment[equipmentIndex], equipmentIndex, isCivilian, character);
                }

                //Unequip current equipment element
                if (!equipment[equipmentIndex].IsEmpty && (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null))
                {
                    TransferCommand transferCommand = TransferCommand.Transfer(
                        1,
                        InventoryLogic.InventorySide.Equipment,
                        InventoryLogic.InventorySide.PlayerInventory,
                        new ItemRosterElement(equipment[equipmentIndex], 1),
                        equipmentIndex,
                        EquipmentIndex.None,
                        character,
                        isCivilian
                    );
                    _inventoryLogic.AddTransferCommand(transferCommand);
                }

                if (bestLeftEquipmentElement.Item != null || bestRightEquipmentElement.Item != null)
                    if (ItemIndexCalculation(bestLeftEquipmentElement, equipmentIndex) > ItemIndexCalculation(bestRightEquipmentElement, equipmentIndex))
                    {
                        TransferCommand equipCommand = TransferCommand.Transfer(
                            1,
                            InventoryLogic.InventorySide.OtherInventory,
                            InventoryLogic.InventorySide.Equipment,
                            new ItemRosterElement(bestLeftEquipmentElement, 1),
                            EquipmentIndex.None,
                            equipmentIndex,
                            character,
                            isCivilian
                        );

                        EquipMessage(equipmentIndex, character);
                        _inventoryLogic.AddTransferCommand(equipCommand);
                    }
                    else
                    {
                        TransferCommand equipCommand = TransferCommand.Transfer(
                            1,
                            InventoryLogic.InventorySide.PlayerInventory,
                            InventoryLogic.InventorySide.Equipment,
                            new ItemRosterElement(bestRightEquipmentElement, 1),
                            EquipmentIndex.None,
                            equipmentIndex,
                            character,
                            isCivilian
                        );

                        EquipMessage(equipmentIndex, character);
                        _inventoryLogic.AddTransferCommand(equipCommand);
                    }
                _inventory.GetMethod("ExecuteRemoveZeroCounts");
            }
            _inventory.GetMethod("RefreshInformationValues");
        }

        public void EquipCharacter(CharacterObject character)
        {
            _characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(character.Name.ToString());

            if (_inventory.IsInWarSet)
            {
                Equipment battleEquipment = character.FirstBattleEquipment;
                EquipCharacterEquipment(character, battleEquipment, false);
            }
            else
            {
                Equipment civilEquipment = character.FirstCivilianEquipment;
                EquipCharacterEquipment(character, civilEquipment, true);
            }
        }

        private static void EquipMessage(EquipmentIndex equipmentIndex, CharacterObject character)
        {
            switch (equipmentIndex)
            {
                case EquipmentIndex.Weapon0:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips weapon in the first slot"));
                    break;
                case EquipmentIndex.Weapon1:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips weapon in the second slot"));
                    break;
                case EquipmentIndex.Weapon2:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips weapon in the third slot"));
                    break;
                case EquipmentIndex.Weapon3:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips weapon in the fourth slot"));
                    break;
                case EquipmentIndex.Head:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips helmet"));
                    break;
                case EquipmentIndex.Body:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips body armor"));
                    break;
                case EquipmentIndex.Leg:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips boots"));
                    break;
                case EquipmentIndex.Gloves:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips gloves"));
                    break;
                case EquipmentIndex.Cape:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips cape"));
                    break;
                case EquipmentIndex.Horse:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips horse"));
                    break;
                case EquipmentIndex.HorseHarness:
                    InformationManager.DisplayMessage(new InformationMessage(character.Name + " equips horse harness"));
                    break;
                default:
                    break;
            }
        }

        public EquipmentElement GetBetterItemFromSide(MBBindingList<SPItemVM> itemListVM, EquipmentElement equipmentElement, EquipmentIndex slot, bool isCivilian, CharacterObject character)
        {
            EquipmentElement bestEquipmentElement;

            foreach (SPItemVM item in itemListVM)
            {
                if (IsCamel(item) || IsCamelHarness(item))
                    continue;
                if (isCivilian)
                {
                    if (slot < EquipmentIndex.NonWeaponItemBeginSlot &&
                        item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon != null &&
                        item.IsEquipableItem &&
                        item.IsCivilianItem &&
                        CharacterHelper.CanUseItem(character, item.ItemRosterElement.EquipmentElement)
                        )
                    {
                        if (equipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.WeaponClass &&
                            GetItemUsage(item) == equipmentElement.Item.PrimaryWeapon.ItemUsage)
                            if (bestEquipmentElement.IsEmpty)
                                if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(equipmentElement, slot) &&
                                    ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) != 0f)
                                    bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                                else
                                    continue;
                            else
                                if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(bestEquipmentElement, slot) &&
                                    ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) != 0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                    }
                    else if (item.ItemType == slot && item.IsEquipableItem && item.IsCivilianItem &&
                        CharacterHelper.CanUseItem(character, item.ItemRosterElement.EquipmentElement))
                        if (bestEquipmentElement.IsEmpty)
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(equipmentElement, slot) &&
                                ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) != 0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                            else
                                continue;
                        else
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(bestEquipmentElement, slot) &&
                                ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) != 0f)
                            bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                }
                else
                {
                    if (slot < EquipmentIndex.NonWeaponItemBeginSlot && item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon != null && item.IsEquipableItem &&
                        CharacterHelper.CanUseItem(character, item.ItemRosterElement.EquipmentElement))
                    {
                        if (equipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.WeaponClass &&
                            GetItemUsage(item) == equipmentElement.Item.PrimaryWeapon.ItemUsage)
                            if (bestEquipmentElement.IsEmpty)
                                if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(equipmentElement, slot) &&
                                    ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) != 0f)
                                    bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                                else
                                    continue;
                            else
                                if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(bestEquipmentElement, slot) &&
                                    ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) != 0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                    }
                    else if (item.ItemType == slot && item.IsEquipableItem &&
                        CharacterHelper.CanUseItem(character, item.ItemRosterElement.EquipmentElement))
                        if (bestEquipmentElement.IsEmpty)
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(equipmentElement, slot) &&
                                ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) != 0f)
                                bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                            else
                                continue;
                        else
                            if (ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) > ItemIndexCalculation(bestEquipmentElement, slot) &&
                                ItemIndexCalculation(item.ItemRosterElement.EquipmentElement, slot) != 0f)
                            bestEquipmentElement = item.ItemRosterElement.EquipmentElement;
                }
            }

            return bestEquipmentElement;
        }

        private float ItemIndexCalculation(EquipmentElement sourceItem, EquipmentIndex slot)
        {

            if (sourceItem.IsEmpty)
                return -9999f;

            float value = 0f;

            if (sourceItem.Item.HasArmorComponent)
            {
                ArmorComponent armorComponentItem = sourceItem.Item.ArmorComponent;
                FilterArmorSettings filterArmor = _characterSettings.FilterArmor[GetEquipmentSlot(slot)];

                float sum =
                    Math.Abs(filterArmor.HeadArmor) +
                    Math.Abs(filterArmor.ArmArmor) +
                    Math.Abs(filterArmor.ArmorBodyArmor) +
                    Math.Abs(filterArmor.ArmorWeight) +
                    Math.Abs(filterArmor.LegArmor);

                ItemModifier mod =
                    sourceItem.ItemModifier;

                int HeadArmor = armorComponentItem.HeadArmor,
                    BodyArmor = armorComponentItem.BodyArmor,
                    LegArmor = armorComponentItem.LegArmor,
                    ArmArmor = armorComponentItem.ArmArmor;
                float Weight = sourceItem.Weight;

                if (mod != null)
                {
                    HeadArmor = mod.ModifyArmor(HeadArmor);
                    BodyArmor = mod.ModifyArmor(BodyArmor);
                    LegArmor = mod.ModifyArmor(LegArmor);
                    ArmArmor = mod.ModifyArmor(ArmArmor);
                    //Weight *= mod.WeightMultiplier;
                }

                value = (
                    HeadArmor * filterArmor.HeadArmor +
                    BodyArmor * filterArmor.ArmorBodyArmor +
                    LegArmor * filterArmor.LegArmor +
                    ArmArmor * filterArmor.ArmArmor +
                    Weight * filterArmor.ArmorWeight
                ) / sum;

#if DEBUG
                InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: HA {1}, BA {2}, LA {3}, AA {4}, W {5}",
                                sourceItem.Item.Name, HeadArmor, BodyArmor, LegArmor, ArmArmor, Weight)));

                InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
#endif

                return value;
            }

            if (sourceItem.Item.PrimaryWeapon != null)
            {
                WeaponComponentData primaryWeaponItem = sourceItem.Item.PrimaryWeapon;
                FilterWeaponSettings filterWeapon = _characterSettings.FilterWeapon[GetEquipmentSlot(slot)];
                float sum =
                    Math.Abs(filterWeapon.Accuracy) +
                    Math.Abs(filterWeapon.WeaponBodyArmor) +
                    Math.Abs(filterWeapon.Handling) +
                    Math.Abs(filterWeapon.MaxDataValue) +
                    Math.Abs(filterWeapon.MissileSpeed) +
                    Math.Abs(filterWeapon.SwingDamage) +
                    Math.Abs(filterWeapon.SwingSpeed) +
                    Math.Abs(filterWeapon.ThrustDamage) +
                    Math.Abs(filterWeapon.ThrustSpeed) +
                    Math.Abs(filterWeapon.WeaponLength) +
                    Math.Abs(filterWeapon.WeaponWeight);

                int Accuracy = primaryWeaponItem.Accuracy,
                    BodyArmor = primaryWeaponItem.BodyArmor,
                    Handling = primaryWeaponItem.Handling,
                    MaxDataValue = primaryWeaponItem.MaxDataValue,
                    MissileSpeed = primaryWeaponItem.MissileSpeed,
                    SwingDamage = primaryWeaponItem.SwingDamage,
                    SwingSpeed = primaryWeaponItem.SwingSpeed,
                    ThrustDamage = primaryWeaponItem.ThrustDamage,
                    ThrustSpeed = primaryWeaponItem.ThrustSpeed,
                    WeaponLength = primaryWeaponItem.WeaponLength;
                float WeaponWeight = sourceItem.Weight;

                ItemModifier mod = sourceItem.ItemModifier;
                if (mod != null)
                {
                    BodyArmor = mod.ModifyArmor(BodyArmor);
                    MissileSpeed = mod.ModifyMissileSpeed(MissileSpeed);
                    SwingDamage = mod.ModifyDamage(SwingDamage);
                    SwingSpeed = mod.ModifySpeed(SwingSpeed);
                    ThrustDamage = mod.ModifyDamage(ThrustDamage);
                    ThrustSpeed = mod.ModifySpeed(ThrustSpeed);
                    MaxDataValue += mod.HitPoints;
                    //WeaponWeight *= mod.WeightMultiplier;

                }

                var weights = _characterSettings.FilterWeapon[GetEquipmentSlot(slot)];
                value = (
                    Accuracy * weights.Accuracy +
                    BodyArmor * weights.WeaponBodyArmor +
                    Handling * weights.Handling +
                    MaxDataValue * weights.MaxDataValue +
                    MissileSpeed * weights.MissileSpeed +
                    SwingDamage * weights.SwingDamage +
                    SwingSpeed * weights.SwingSpeed +
                    ThrustDamage * weights.ThrustDamage +
                    ThrustSpeed * weights.ThrustSpeed +
                    WeaponLength * weights.WeaponLength +
                    WeaponWeight * weights.WeaponWeight
                ) / sum;


#if DEBUG
                InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: Acc {1}, BA {2}, HL {3}, HP {4}, MS {5}, SD {6}, SS {7}, TD {8}, TS {9}, WL {10}, W {11}",
                                sourceItem.Item.Name, Accuracy, BodyArmor, Handling, MaxDataValue, MissileSpeed, SwingDamage, SwingSpeed, ThrustDamage, ThrustSpeed, WeaponLength, WeaponWeight)));

                InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
#endif

                return value;
            }

            if (sourceItem.Item.HasHorseComponent)
            {
                HorseComponent horseComponentItem = sourceItem.Item.HorseComponent;
                FilterMountSettings filterMount = _characterSettings.FilterMount;

                float sum =
                    Math.Abs(filterMount.ChargeDamage) +
                    Math.Abs(filterMount.HitPoints) +
                    Math.Abs(filterMount.Maneuver) +
                    Math.Abs(filterMount.Speed);

                int ChargeDamage = horseComponentItem.ChargeDamage,
                    HitPoints = horseComponentItem.HitPoints,
                    Maneuver = horseComponentItem.Maneuver,
                    Speed = horseComponentItem.Speed;

                ItemModifier mod =
                    sourceItem.ItemModifier;
                if (mod != null)
                {
                    ChargeDamage = mod.ModifyHorseCharge(ChargeDamage);
                    Maneuver = mod.ModifyHorseManuever(Maneuver);
                    Speed = mod.ModifyHorseSpeed(Speed);
                }

                var weights = _characterSettings.FilterMount;
                value = (
                    ChargeDamage * weights.ChargeDamage +
                    HitPoints * weights.HitPoints +
                    Maneuver * weights.Maneuver +
                    Speed * weights.Speed
                ) / sum;


#if DEBUG
                InformationManager.DisplayMessage(new InformationMessage(String.Format("{0}: CD {1}, HP {2}, MR {3}, SD {4}",
                                sourceItem.Item.Name, ChargeDamage, HitPoints, Maneuver, Speed)));

                InformationManager.DisplayMessage(new InformationMessage("Total score: " + value));
#endif

                return value;
            }

            return value;
        }

        public static int GetEquipmentSlot(EquipmentIndex slot)
        {
            switch (slot)
            {
                case EquipmentIndex.Weapon0:
                    return 0;
                case EquipmentIndex.Weapon1:
                    return 1;
                case EquipmentIndex.Weapon2:
                    return 2;
                case EquipmentIndex.Weapon3:
                    return 3;
                case EquipmentIndex.Head:
                    return 0;
                case EquipmentIndex.Cape:
                    return 1;
                case EquipmentIndex.Body:
                    return 2;
                case EquipmentIndex.Gloves:
                    return 3;
                case EquipmentIndex.Leg:
                    return 4;
                case EquipmentIndex.Horse:
                    return 0;
                case EquipmentIndex.HorseHarness:
                    return 5;
                default:
                    return 0;
            }
        }
        private static bool IsCamel(SPItemVM item)
        {
            if (item != null)
                if (!item.ItemRosterElement.IsEmpty)
                    if (!item.ItemRosterElement.EquipmentElement.IsEmpty)
                        if (item.ItemRosterElement.EquipmentElement.Item.HasHorseComponent)
                            if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Monster.MonsterUsage == "camel")
                                return true;
            return false;
        }

        private static bool IsCamelHarness(SPItemVM item)
        {
            if (item != null && item.StringId.StartsWith("camel_sadd"))
                return true;
            return false;
        }

        public static string GetItemUsage(SPItemVM item)
        {
            if (item == null || item.ItemRosterElement.IsEmpty || item.ItemRosterElement.EquipmentElement.IsEmpty || item.ItemRosterElement.EquipmentElement.Item.WeaponComponent == null)
                return "";
            string value = item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.ItemUsage;
            return value;
        }

        public CharacterObject GetCharacterByName(string name)
        {
            foreach (TroopRosterElement rosterElement in _inventoryLogic.RightMemberRoster)
            {
                if (rosterElement.Character.IsHero && rosterElement.Character.Name.ToString() == name)
                    return rosterElement.Character;
            }
            return null;
        }

        public void ExecuteEquipEveryCharacter()
        {
            EquipEveryCharacter();
        }

        public void ExecuteEquipCurrentCharacter()
        {
            EquipCharacter(GetCharacterByName(_inventory.CurrentCharacterName));
            this.RefreshValues();

#if DEBUG
            InformationManager.DisplayMessage(new InformationMessage("ExecuteEquipCurrentCharacter"));
#endif
        }

        public void EquipBestItem(EquipmentIndex equipmentIndex)
        {
            Equipment equipment = _inventory.IsInWarSet ? _currentCharacter.FirstBattleEquipment : _currentCharacter.FirstCivilianEquipment;
            //Unequip current equipment element
            if (!equipment[equipmentIndex].IsEmpty)
            {
                TransferCommand transferCommand = TransferCommand.Transfer(
                    1,
                    InventoryLogic.InventorySide.Equipment,
                    InventoryLogic.InventorySide.PlayerInventory,
                    new ItemRosterElement(equipment[equipmentIndex], 1),
                    equipmentIndex,
                    EquipmentIndex.None,
                    _currentCharacter,
                    !_inventory.IsInWarSet
                );
                _inventoryLogic.AddTransferCommand(transferCommand);
            }
            //Equip
            if (ItemIndexCalculation(_bestLeftEquipment[equipmentIndex], equipmentIndex) > ItemIndexCalculation(_bestRightEquipment[equipmentIndex], equipmentIndex))
            {
                TransferCommand equipCommand = TransferCommand.Transfer(
                    1,
                    InventoryLogic.InventorySide.OtherInventory,
                    InventoryLogic.InventorySide.Equipment,
                    new ItemRosterElement(_bestLeftEquipment[equipmentIndex], 1),
                    EquipmentIndex.None,
                    equipmentIndex,
                    _currentCharacter,
                    !_inventory.IsInWarSet
                );

                _inventoryLogic.AddTransferCommand(equipCommand);
            }
            else
            {
                TransferCommand equipCommand = TransferCommand.Transfer(
                    1,
                    InventoryLogic.InventorySide.PlayerInventory,
                    InventoryLogic.InventorySide.Equipment,
                    new ItemRosterElement(_bestRightEquipment[equipmentIndex], 1),
                    EquipmentIndex.None,
                    equipmentIndex,
                    _currentCharacter,
                    !_inventory.IsInWarSet
                );

                _inventoryLogic.AddTransferCommand(equipCommand);
            }
            _inventory.GetMethod("ExecuteRemoveZeroCounts");
            _inventory.GetMethod("RefreshInformationValues");
        }

        public void ExecuteEquipBestHelm()
        {
            EquipBestItem(EquipmentIndex.Head);
        }

        public void ExecuteEquipBestCloak()
        {
            EquipBestItem(EquipmentIndex.Cape);
        }

        public void ExecuteEquipBestArmor()
        {
            EquipBestItem(EquipmentIndex.Body);
        }

        public void ExecuteEquipBestGlove()
        {
            EquipBestItem(EquipmentIndex.Gloves);
        }

        public void ExecuteEquipBestBoot()
        {
            EquipBestItem(EquipmentIndex.Leg);
        }

        public void ExecuteEquipBestMount()
        {
            EquipBestItem(EquipmentIndex.Horse);
        }

        public void ExecuteEquipBestHarness()
        {
            EquipBestItem(EquipmentIndex.HorseHarness);
        }

        public void ExecuteEquipBestWeapon1()
        {
            EquipBestItem(EquipmentIndex.Weapon0);
        }

        public void ExecuteEquipBestWeapon2()
        {
            EquipBestItem(EquipmentIndex.Weapon1);
        }

        public void ExecuteEquipBestWeapon3()
        {
            EquipBestItem(EquipmentIndex.Weapon2);
        }

        public void ExecuteEquipBestWeapon4()
        {
            EquipBestItem(EquipmentIndex.Weapon3);
        }
    }
}
