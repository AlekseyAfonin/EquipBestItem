using EquipBestItem.Settings;
using SandBox.GauntletUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using static TaleWorlds.Core.ItemObject;

namespace EquipBestItem
{
    public class EquipBestItemViewModel : ViewModel
    {

        public static InventoryGauntletScreen InventoryScreen { get; set; }

        public static bool IsHelmButtonEnabled { get; set; }
        public static bool IsCloakButtonEnabled { get; set; }
        public static bool IsArmorButtonEnabled { get; set; }
        public static bool IsGloveButtonEnabled { get; set; }
        public static bool IsBootButtonEnabled { get; set; }
        public static bool IsMountButtonEnabled { get; set; }
        public static bool IsHarnessButtonEnabled { get; set; }
        public static bool IsWeapon1ButtonEnabled { get; set; }
        public static bool IsWeapon2ButtonEnabled { get; set; }
        public static bool IsWeapon3ButtonEnabled { get; set; }
        public static bool IsWeapon4ButtonEnabled { get; set; }
        public static bool IsEquipAllButtonEnabled { get; set; }
        public static bool IsEquipAllActivated { get; set; }

        public static bool IsLeftPanelLocked { get; set; }
        public static bool IsRightPanelLocked { get; set; }

        public static SPItemVM BestHelm;
        public static SPItemVM BestCloak;
        public static SPItemVM BestArmor;
        public static SPItemVM BestGlove;
        public static SPItemVM BestBoot;
        public static SPItemVM BestMount;
        public static SPItemVM BestHarness;
        public static SPItemVM BestWeapon1;
        public static SPItemVM BestWeapon2;
        public static SPItemVM BestWeapon3;
        public static SPItemVM BestWeapon4;

        private static CharacterSettings _characterSettings { get; set; }
        public static string CurrentCharacterName { get; set; }

        public static SPInventoryVM _inventory;

        public EquipBestItemViewModel()
        {
            _inventory = EquipBestItemViewModel.InventoryScreen.GetField("_dataSource") as SPInventoryVM;
            UpdateCurrentCharacterName();
        }

        private string GetItemUsage(SPItemVM item)
        {
            if (item == null || item.ItemRosterElement.IsEmpty || item.ItemRosterElement.EquipmentElement.IsEmpty || item.ItemRosterElement.EquipmentElement.Item.WeaponComponent == null)
                return "";
            string value = item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.ItemUsage;
            return value;
        }

        private static void FindBestItemsFromSide(MBBindingList<SPItemVM> inventory)
        {
            foreach (SPItemVM item in inventory)
            {
                if (_inventory.IsInWarSet)
                {
                    if (item.IsEquipableItem && item.CanCharacterUseItem)
                    {
                        if (item.ItemRosterElement.EquipmentElement.Item.ArmorComponent != null)
                        {
                            switch (item.ItemRosterElement.EquipmentElement.Item.ItemType)
                            {
                                case ItemTypeEnum.HeadArmor:
                                    {
                                        if (_characterSettings.IsHelmLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterHelmSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestHelm))
                                        {
                                            BestHelm = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.Cape:
                                    {
                                        if (_characterSettings.IsCloakLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterCloakSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestCloak))
                                        {
                                            BestCloak = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.BodyArmor:
                                    {
                                        if (_characterSettings.IsArmorLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterTorsoSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestArmor))
                                        {
                                            BestArmor = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HandArmor:
                                    {
                                        if (_characterSettings.IsGloveLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterGloveSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestGlove))
                                        {
                                            BestGlove = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.LegArmor:
                                    {
                                        if (_characterSettings.IsBootLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterBootSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestBoot))
                                        {
                                            BestBoot = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HorseHarness:
                                    {
                                        if (_characterSettings.IsHarnessLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterMountArmorSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestHarness))
                                        {
                                            BestHarness = item;
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                        if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent != null)
                        {
                            if (_inventory.CharacterWeapon1Slot != null && !_inventory.CharacterWeapon1Slot.ItemRosterElement.IsEmpty && !_characterSettings.IsWeapon1Locked)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon1Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass &&
                                    item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.ItemUsage == _inventory.CharacterWeapon1Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.ItemUsage)
                                    if (GetWeaponEffectiveness(item) > GetWeaponEffectiveness(_inventory.CharacterWeapon1Slot) && GetWeaponEffectiveness(item) > GetWeaponEffectiveness(BestWeapon1))
                                    {
                                        BestWeapon1 = item;
                                    }
                            if (_inventory.CharacterWeapon2Slot != null && !_inventory.CharacterWeapon2Slot.ItemRosterElement.IsEmpty && !_characterSettings.IsWeapon2Locked)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon2Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass)
                                    if (GetWeaponEffectiveness(item) > GetWeaponEffectiveness(_inventory.CharacterWeapon2Slot) && GetWeaponEffectiveness(item) > GetWeaponEffectiveness(BestWeapon2))
                                    {
                                        BestWeapon2 = item;
                                    }
                            if (_inventory.CharacterWeapon3Slot != null && !_inventory.CharacterWeapon3Slot.ItemRosterElement.IsEmpty && !_characterSettings.IsWeapon3Locked)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon3Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass)
                                    if (GetWeaponEffectiveness(item) > GetWeaponEffectiveness(_inventory.CharacterWeapon3Slot) && GetWeaponEffectiveness(item) > GetWeaponEffectiveness(BestWeapon3))
                                    {
                                        BestWeapon3 = item;
                                    }
                            if (_inventory.CharacterWeapon4Slot != null && !_inventory.CharacterWeapon4Slot.ItemRosterElement.IsEmpty && !_characterSettings.IsWeapon4Locked)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon4Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass)
                                    if (GetWeaponEffectiveness(item) > GetWeaponEffectiveness(_inventory.CharacterWeapon4Slot) && GetWeaponEffectiveness(item) > GetWeaponEffectiveness(BestWeapon4))
                                    {
                                        BestWeapon4 = item;
                                    }

                        }
                        if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent != null && !_characterSettings.IsMountLocked)
                        {
                            if (GetMountEffectiveness(item) > GetMountEffectiveness(_inventory.CharacterMountSlot) && GetMountEffectiveness(item) > GetMountEffectiveness(BestMount))
                                BestMount = item;
                        }
                    }
                }
                else
                {
                    if (item.IsEquipableItem && item.CanCharacterUseItem && item.IsCivilianItem)
                    {
                        if (item.ItemRosterElement.EquipmentElement.Item.ArmorComponent != null)
                        {
                            switch (item.ItemRosterElement.EquipmentElement.Item.ItemType)
                            {
                                case ItemTypeEnum.HeadArmor:
                                    {
                                        if (_characterSettings.IsHelmLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterHelmSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestHelm))
                                        {
                                            BestHelm = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.Cape:
                                    {
                                        if (_characterSettings.IsCloakLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterCloakSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestCloak))
                                        {
                                            BestCloak = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.BodyArmor:
                                    {
                                        if (_characterSettings.IsArmorLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterTorsoSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestArmor))
                                        {
                                            BestArmor = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HandArmor:
                                    {
                                        if (_characterSettings.IsGloveLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterGloveSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestGlove))
                                        {
                                            BestGlove = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.LegArmor:
                                    {
                                        if (_characterSettings.IsBootLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterBootSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestBoot))
                                        {
                                            BestBoot = item;
                                        }
                                        break;
                                    }
                                case ItemTypeEnum.HorseHarness:
                                    {
                                        if (_characterSettings.IsHarnessLocked)
                                            break;
                                        if (GetArmorEffectiveness(item) > GetArmorEffectiveness(_inventory.CharacterMountArmorSlot) && GetArmorEffectiveness(item) > GetArmorEffectiveness(BestHarness))
                                        {
                                            BestHarness = item;
                                        }
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                        if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent != null)
                        {
                            if (_inventory.CharacterWeapon1Slot != null && !_inventory.CharacterWeapon1Slot.ItemRosterElement.IsEmpty && !_characterSettings.IsWeapon1Locked)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon1Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass)
                                    if (GetWeaponEffectiveness(item) > GetWeaponEffectiveness(_inventory.CharacterWeapon1Slot) && GetWeaponEffectiveness(item) > GetWeaponEffectiveness(BestWeapon1))
                                    {
                                        BestWeapon1 = item;
                                    }
                            if (_inventory.CharacterWeapon2Slot != null && !_inventory.CharacterWeapon2Slot.ItemRosterElement.IsEmpty && !_characterSettings.IsWeapon2Locked)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon2Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass)
                                    if (GetWeaponEffectiveness(item) > GetWeaponEffectiveness(_inventory.CharacterWeapon2Slot) && GetWeaponEffectiveness(item) > GetWeaponEffectiveness(BestWeapon2))
                                    {
                                        BestWeapon2 = item;
                                    }
                            if (_inventory.CharacterWeapon3Slot != null && !_inventory.CharacterWeapon3Slot.ItemRosterElement.IsEmpty && !_characterSettings.IsWeapon3Locked)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon3Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass)
                                    if (GetWeaponEffectiveness(item) > GetWeaponEffectiveness(_inventory.CharacterWeapon3Slot) && GetWeaponEffectiveness(item) > GetWeaponEffectiveness(BestWeapon3))
                                    {
                                        BestWeapon3 = item;
                                    }
                            if (_inventory.CharacterWeapon4Slot != null && !_inventory.CharacterWeapon4Slot.ItemRosterElement.IsEmpty && !_characterSettings.IsWeapon4Locked)
                                if (item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass == _inventory.CharacterWeapon4Slot.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass)
                                    if (GetWeaponEffectiveness(item) > GetWeaponEffectiveness(_inventory.CharacterWeapon4Slot) && GetWeaponEffectiveness(item) > GetWeaponEffectiveness(BestWeapon4))
                                    {
                                        BestWeapon4 = item;
                                    }

                        }
                        if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent != null && !_characterSettings.IsMountLocked)
                        {
                            if (GetMountEffectiveness(item) > GetMountEffectiveness(_inventory.CharacterMountSlot) && GetMountEffectiveness(item) > GetMountEffectiveness(BestMount))
                                BestMount = item;
                        }
                    }
                }
            }
        }

        public static void FindBestItems()
        {
            _characterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(CurrentCharacterName);

            NullBestItems();

            if (!SettingsLoader.Instance.Settings.IsRightPanelLocked)
            {
                FindBestItemsFromSide(_inventory.RightItemListVM);
            }
            if (!SettingsLoader.Instance.Settings.IsLeftPanelLocked)
            {
                FindBestItemsFromSide(_inventory.LeftItemListVM);
            }
                                 
            ButtonStatusUpdate();
        }

        private static bool IsCamel(SPItemVM item)
        {
            if (_inventory.CharacterMountSlot != null)
                if (!_inventory.CharacterMountSlot.ItemRosterElement.IsEmpty)
                    if (_inventory.CharacterMountSlot.ItemRosterElement.EquipmentElement.Item.HorseComponent.Monster.MonsterUsage == "camel")
                        return true;
            return false;
        }

        public static void ButtonStatusUpdate()
        {
            IsEquipAllButtonEnabled = false;

            if (BestHelm != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsHelmButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsHelmButtonEnabled = false;
            }

            if (BestCloak != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsCloakButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsCloakButtonEnabled = false;
            }

            if (BestArmor != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsArmorButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsArmorButtonEnabled = false;
            }

            if (BestGlove != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsGloveButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsGloveButtonEnabled = false;
            }

            if (BestBoot != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsBootButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsBootButtonEnabled = false;
            }

            if (BestMount != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsMountButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsMountButtonEnabled = false;
            }

            if (BestHarness != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                if (IsCamel(_inventory.CharacterMountSlot) == false)
                {
                    IsHarnessButtonEnabled = true;
                    if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                        IsEquipAllButtonEnabled = true;
                }
                else
                {
                    IsHarnessButtonEnabled = false;
                }
            }
            else
            {
                IsHarnessButtonEnabled = false;
            }

            if (BestWeapon1 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsWeapon1ButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsWeapon1ButtonEnabled = false;
            }

            if (BestWeapon2 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            { 
                IsWeapon2ButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsWeapon2ButtonEnabled = false;
            }

            if (BestWeapon3 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsWeapon3ButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsWeapon3ButtonEnabled = false;
            }

            if (BestWeapon4 != null && SettingsLoader.Instance.Settings.IsEnabledStandardButtons)
            {
                IsWeapon4ButtonEnabled = true;
                if (SettingsLoader.Instance.Settings.IsEnabledEquipAllButton)
                    IsEquipAllButtonEnabled = true;
            }
            else
            {
                IsWeapon4ButtonEnabled = false;
            }

        }

        static void NullBestItems()
        {
            BestArmor = null;
            BestBoot = null;
            BestCloak = null;
            BestGlove = null;
            BestHarness = null;
            BestHelm = null;
            BestMount = null;
            BestWeapon1 = null;
            BestWeapon2 = null;
            BestWeapon3 = null;
            BestWeapon4 = null;
        }

        private static float GetMountEffectiveness(SPItemVM item)
        {

            float value = 0f;

            if (item != null && item.ItemRosterElement.EquipmentElement.Item != null)
            {
                if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent != null)
                {
                    if (item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Monster.MonsterUsage != "camel" && item.ItemDescription != "Mule")
                        value = (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Maneuver *
                            (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Speed +
                            (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.ChargeDamage +
                            (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.HitPoints * 0.2f;
                    else
                        value = ((float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Maneuver *
                            (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Speed +
                            (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.ChargeDamage +
                            (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.HitPoints) * 0.0001f;

                }
            }

            return value;
        }

        public static void UpdateCurrentCharacterName()
        {
            if (_inventory.CurrentCharacterName != null)
                CurrentCharacterName = _inventory.CurrentCharacterName;
            else
                CurrentCharacterName = Hero.MainHero.Name.ToString();
        }

        private static float GetArmorEffectiveness(SPItemVM item)
        {
            int value = 0;

            if (item != null && item.ItemRosterElement.EquipmentElement.Item != null)
            {
                value =
                    item.ItemRosterElement.EquipmentElement.GetArmArmor() +
                    item.ItemRosterElement.EquipmentElement.GetLegArmor() +
                    item.ItemRosterElement.EquipmentElement.GetHeadArmor() +
                    item.ItemRosterElement.EquipmentElement.GetBodyArmorHuman() +
                    item.ItemRosterElement.EquipmentElement.GetBodyArmorHorse();
            }

            return value;
        }

        private static float GetWeaponEffectiveness(SPItemVM item)
        {
            float value = 0f;

            if (item != null && item.ItemRosterElement.EquipmentElement.Item != null)
            {
                value = item.ItemRosterElement.EquipmentElement.Item.Effectiveness;
            }

            return value;
        }


        public override void RefreshValues()
        {
            base.RefreshValues();
            FindBestItems();
            //InformationManager.DisplayMessage(new InformationMessage("ViewModel RefreshValue() " + Game.Current.ApplicationTime.ToString()));
        }

        public void EquipBestHelm()
        {
            _inventory.Call("ProcessEquipItem", BestHelm);
            BestHelm = null;
            //this.RefreshValues();
        }

        public void EquipBestCloak()
        {
            _inventory.Call("ProcessEquipItem", BestCloak);
            BestCloak = null;
            //this.RefreshValues();
        }

        public void EquipBestArmor()
        {
            _inventory.Call("ProcessEquipItem", BestArmor);
            BestArmor = null;
            //this.RefreshValues();
        }

        public void EquipBestGlove()
        {
            _inventory.Call("ProcessEquipItem", BestGlove);
            BestGlove = null;
            //this.RefreshValues();
        }
        public void EquipBestBoot()
        {
            _inventory.Call("ProcessEquipItem", BestBoot);
            BestBoot = null;
            //this.RefreshValues();
        }

        public void EquipBestMount()
        {
            _inventory.Call("ProcessEquipItem", BestMount);
            BestMount = null;
            //this.RefreshValues();
        }
        public void EquipBestHarness()
        {
            _inventory.Call("ProcessEquipItem", BestHarness);
            BestHarness = null;
            //this.RefreshValues();
        }

        public void EquipBestWeapon1()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon1Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon1);
            BestWeapon1 = null;
            this.RefreshValues();
        }
        public void EquipBestWeapon2()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon2Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon2);
            BestWeapon2 = null;
            this.RefreshValues();
        }

        public void EquipBestWeapon3()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon3Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon3);
            BestWeapon3 = null;
            this.RefreshValues();
        }
        public void EquipBestWeapon4()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon4Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon4);
            BestWeapon4 = null;
            this.RefreshValues();
        }

        public bool IsAllBestItemsNull()
        {
            if (BestHelm != null)
            {
                return false;
            }
            if (BestCloak != null)
            {
                return false;
            }
            if (BestArmor != null)
            {
                return false;
            }
            if (BestGlove != null)
            {
                return false;
            }
            if (BestBoot != null)
            {
                return false;
            }
            if (BestMount != null)
            {
                return false;
            }
            if (BestHarness != null)
            {
                return false;
            }
            if (BestWeapon1 != null)
            {
                return false;
            }
            if (BestWeapon2 != null)
            {
                return false;
            }
            if (BestWeapon3 != null)
            {
                return false;
            }
            if (BestWeapon4 != null)
            {
                return false;
            }
            return true;
        }

        public void EquipAll()
        {
            if (BestHelm != null)
            {
                EquipBestHelm();
                return;
            }
            if (BestCloak != null)
            {
                EquipBestCloak();
                return;
            }
            if (BestArmor != null)
            {
                EquipBestArmor();
                return;
            }
            if (BestGlove != null)
            {
                EquipBestGlove();
                return;
            }
            if (BestBoot != null)
            {
                EquipBestBoot();
                return;
            }
            if (BestMount != null)
            {
                EquipBestMount();
                return;
            }
            if (BestHarness != null)
            {
                EquipBestHarness();
                return;
            }
            if (BestWeapon1 != null)
            {
                EquipBestWeapon1();
                return;
            }
            if (BestWeapon2 != null)
            {
                EquipBestWeapon2();
                return;
            }
            if (BestWeapon3 != null)
            {
                EquipBestWeapon3();
                return;
            }
            if (BestWeapon4 != null)
            {
                EquipBestWeapon4();
                return;
            }
        }
    }
}
