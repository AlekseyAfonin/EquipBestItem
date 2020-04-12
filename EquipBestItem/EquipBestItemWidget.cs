using System;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace EquipBestItem
{
    public class EquipBestItemWidget : ButtonWidget
    {
        SPInventoryVM _inventory;
        InventoryGauntletScreen _inventoryScreen;
        public static SPItemVM BestHelmet;
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
        public static bool StateItemsWasChanged;

        bool _firstUpdateState = false;
        bool _leftMouseButtonStatus;
        bool _rightMouseButtonStatus;
        int _delay;

        public EquipBestItemWidget(UIContext context) : base(context)
        {

            if (ScreenManager.TopScreen is InventoryGauntletScreen)
            {
                _inventoryScreen = (InventoryGauntletScreen)ScreenManager.TopScreen;
            }
            _inventory = (SPInventoryVM)_inventoryScreen.GetField("_dataSource");
            
        }
        

        public int GetFullArmor(SPItemVM item)
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

        public float GetEffectiveness(SPItemVM item)
        {
            float value = 0f;

            if (item != null && item.ItemRosterElement.EquipmentElement.Item != null)
            {
                value = item.ItemRosterElement.EquipmentElement.Item.Effectiveness;
            }

            return value;
        }

        public float GetMountEffectiveness(SPItemVM item)
        {
            float value = 0f;

            if (item != null && item.ItemRosterElement.EquipmentElement.Item != null)
            {
                //value = item.ItemRosterElement.EquipmentElement.Item.Effectiveness;
                if (item.ItemRosterElement.EquipmentElement.Item.HasHorseComponent)
                    value = (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Maneuver *
                        (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.Speed +
                        (float)item.ItemRosterElement.EquipmentElement.Item.HorseComponent.ChargeDamage;
            }

            return value;
        }

        bool _latestStateStatus = false;

        private bool StateChanged()
        {

            if (_rightMouseButtonStatus != _latestStateStatus || _leftMouseButtonStatus != _latestStateStatus)
            {
                _latestStateStatus = _rightMouseButtonStatus;
                _delay = 0;
                return true;
            }
            _delay++;
            return false;
        }
        

        protected override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);

            _rightMouseButtonStatus = EventManager.InputContext.IsKeyReleased(TaleWorlds.InputSystem.InputKey.RightMouseButton);
            _leftMouseButtonStatus = EventManager.InputContext.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftMouseButton);


            if (StateChanged() || !_firstUpdateState || _delay == 1 || StateItemsWasChanged)
            {
                //InformationManager.DisplayMessage(new InformationMessage("Update() " + dt, Color.FromUint(4282569842U)));
                if (this.Id == "EquipBestItemHelmButton") HelmButtonUpdate();
                if (this.Id == "EquipBestItemCloakButton") CloakButtonUpdate();
                if (this.Id == "EquipBestItemArmorButton") ArmorButtonUpdate();
                if (this.Id == "EquipBestItemGloveButton") GloveButtonUpdate();
                if (this.Id == "EquipBestItemBootButton") BootButtonUpdate();
                if (this.Id == "EquipBestItemMountButton") MountButtonUpdate();
                if (this.Id == "EquipBestItemHarnessButton") HarnessButtonUpdate();
                if (this.Id == "EquipBestItemWeapon1Button") Weapon1ButtonUpdate();
                if (this.Id == "EquipBestItemWeapon2Button") Weapon2ButtonUpdate();
                if (this.Id == "EquipBestItemWeapon3Button") Weapon3ButtonUpdate();
                if (this.Id == "EquipBestItemWeapon4Button") Weapon4ButtonUpdate();

                _firstUpdateState = true;
            }
        }

        private bool IsAnyButtonEnable()
        {

            if (BestArmor != null) return true;
            if (BestBoot != null) return true;
            if (BestCloak != null) return true;
            if (BestGlove != null) return true;
            if (BestHarness != null) return true;
            if (BestHelmet != null) return true;
            if (BestMount != null) return true;
            if (BestWeapon1 != null) return true;
            if (BestWeapon2 != null) return true;
            if (BestWeapon3 != null) return true;
            if (BestWeapon4 != null) return true;

            return false;
        }
        

        private void Weapon4ButtonUpdate()
        {
            FindBestWeapon4();
            this.IsEnabled = (BestWeapon4 == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
            
            StateItemsWasChanged = false;
        }

        private void Weapon3ButtonUpdate()
        {
            FindBestWeapon3();
            this.IsEnabled = (BestWeapon3 == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
        }

        private void Weapon2ButtonUpdate()
        {
            FindBestWeapon2();
            this.IsEnabled = (BestWeapon2 == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
        }

        private void Weapon1ButtonUpdate()
        {
            FindBestWeapon1();
            this.IsEnabled = (BestWeapon1 == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
        }

        private void HarnessButtonUpdate()
        {
            FindBestHarness();
            this.IsEnabled = (BestHarness == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
        }

        private void MountButtonUpdate()
        {
            FindBestMount();
            this.IsEnabled = (BestMount == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
        }

        private void BootButtonUpdate()
        {
            FindBestBoot();
            this.IsEnabled = (BestBoot == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
        }

        private void GloveButtonUpdate()
        {
            FindBestGlove();
            this.IsEnabled = (BestGlove == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
        }

        private void ArmorButtonUpdate()
        {
            FindBestArmor();
            this.IsEnabled = (BestArmor == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
        }

        private void CloakButtonUpdate()
        {
            FindBestCloak();
            this.IsEnabled = (BestCloak == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
        }

        private void HelmButtonUpdate()
        {
            FindBestHelm();
            this.IsEnabled = (BestHelmet == null || !Settings.EBISettings.IsEnabledStandartButtons) ? false : true;
        }



        private void FindBestWeapon4()
        {
            BestWeapon4 = null;

            if (_inventory.CharacterWeapon4Slot == null)
            {
                return;
            }

            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == _inventory.CharacterWeapon4Slot.TypeId &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon4Slot) &&
                    item.CanCharacterUseItem && 
                    _inventory.IsInWarSet)
                {
                    if (BestWeapon4 != null)
                    {
                        if (GetEffectiveness(item) > GetEffectiveness(BestWeapon4))
                        {
                            BestWeapon4 = item;
                        }
                    }
                    else
                    {
                        BestWeapon4 = item;
                    }
                }
                if (item.TypeId == _inventory.CharacterWeapon4Slot.TypeId &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon4Slot) &&
                    item.CanCharacterUseItem && 
                    !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestWeapon4 != null)
                        {
                            if (GetEffectiveness(item) > GetEffectiveness(BestWeapon4))
                            {
                                BestWeapon4 = item;
                            }
                        }
                        else
                        {
                            BestWeapon4 = item;
                        }
                    }
                }
            }
            if (GetEffectiveness(_inventory.CharacterWeapon4Slot) >= GetEffectiveness(BestWeapon4))
            {
                BestWeapon4 = null;
            }

        }

        private void FindBestWeapon3()
        {
            BestWeapon3 = null;

            if (_inventory.CharacterWeapon3Slot == null)
            {
                return;
            }

            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == _inventory.CharacterWeapon3Slot.TypeId &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon3Slot) &&
                    item.CanCharacterUseItem && 
                    _inventory.IsInWarSet)
                {
                    if (BestWeapon3 != null)
                    {
                        if (GetEffectiveness(item) > GetEffectiveness(BestWeapon3))
                        {
                            BestWeapon3 = item;
                        }
                    }
                    else
                    {
                        BestWeapon3 = item;
                    }
                }
                if (item.TypeId == _inventory.CharacterWeapon3Slot.TypeId &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon3Slot) &&
                    item.CanCharacterUseItem && 
                    !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestWeapon3 != null)
                        {
                            if (GetEffectiveness(item) > GetEffectiveness(BestWeapon3))
                            {
                                BestWeapon3 = item;
                            }
                        }
                        else
                        {
                            BestWeapon3 = item;
                        }
                    }
                }
            }
            if (GetEffectiveness(_inventory.CharacterWeapon3Slot) >= GetEffectiveness(BestWeapon3))
            {
                BestWeapon3 = null;
            }
        }

        private void FindBestWeapon2()
        {
            BestWeapon2 = null;

            if (_inventory.CharacterWeapon2Slot == null)
            {
                return;
            }

            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == _inventory.CharacterWeapon2Slot.TypeId && 
                    item.CanCharacterUseItem && 
                    _inventory.IsInWarSet &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon2Slot))
                {
                    if (BestWeapon2 != null)
                    {
                        if (GetEffectiveness(item) > GetEffectiveness(BestWeapon2))
                        {
                            BestWeapon2 = item;
                        }
                    }
                    else
                    {
                        BestWeapon2 = item;
                    }
                }
                if (item.TypeId == _inventory.CharacterWeapon2Slot.TypeId &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon2Slot) &&
                    item.CanCharacterUseItem && 
                    !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestWeapon2 != null)
                        {
                            if (GetEffectiveness(item) > GetEffectiveness(BestWeapon2))
                            {
                                BestWeapon2 = item;
                            }
                        }
                        else
                        {
                            BestWeapon2 = item;
                        }
                    }
                }
            }
            if (GetEffectiveness(_inventory.CharacterWeapon2Slot) >= GetEffectiveness(BestWeapon2))
            {
                BestWeapon2 = null;
            }
        }

        private void FindBestWeapon1()
        {
            BestWeapon1 = null;

            if (_inventory.CharacterWeapon1Slot == null)
            {
                return;
            }

            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == _inventory.CharacterWeapon1Slot.TypeId &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon1Slot) &&
                    item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (BestWeapon1 != null)
                    {
                        if (GetEffectiveness(item) > GetEffectiveness(BestWeapon1))
                        {
                            BestWeapon1 = item;
                        }
                    }
                    else
                    {
                        BestWeapon1 = item;
                    }
                }
                if (item.TypeId == _inventory.CharacterWeapon1Slot.TypeId &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon1Slot) &&
                    item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestWeapon1 != null)
                        {
                            if (GetEffectiveness(item) > GetEffectiveness(BestWeapon1))
                            {
                                BestWeapon1 = item;
                            }
                        }
                        else
                        {
                            BestWeapon1 = item;
                        }
                    }
                }
            }
            if (GetEffectiveness(_inventory.CharacterWeapon1Slot) >= GetEffectiveness(BestWeapon1))
            {
                BestWeapon1 = null;
            }
        }

        private WeaponClass GetWeaponClass(SPItemVM item)
        {
            if (item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
                return item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass;
            return item.ItemRosterElement.EquipmentElement.Item.WeaponComponent.PrimaryWeapon.WeaponClass;
        }

        protected override void OnClick()
        {
            switch (this.Id)
            {
                case "EquipBestItemHelmButton":
                    EquipBestItemHelmButton();
                    break;
                case "EquipBestItemCloakButton":
                    EquipBestItemCloakButton();
                    break;
                case "EquipBestItemArmorButton":
                    EquipBestItemArmorButton();
                    break;
                case "EquipBestItemGloveButton":
                    EquipBestItemGloveButton();
                    break;
                case "EquipBestItemBootButton":
                    EquipBestItemBootButton();
                    break;
                case "EquipBestItemMountButton":
                    EquipBestItemMountButton();
                    break;
                case "EquipBestItemHarnessButton":
                    EquipBestItemHarnessButton();
                    break;
                case "EquipBestItemWeapon1Button":
                    EquipBestItemWeapon1Button();
                    break;
                case "EquipBestItemWeapon2Button":
                    EquipBestItemWeapon2Button();
                    break;
                case "EquipBestItemWeapon3Button":
                    EquipBestItemWeapon3Button();
                    break;
                case "EquipBestItemWeapon4Button":
                    EquipBestItemWeapon4Button();
                    break;
                default:
                    break; 
            }
        }

        private void EquipBestItemWeapon4Button()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon4Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon4);
            BestWeapon4 = null;
            StateItemsWasChanged = true;
        }

        private void EquipBestItemWeapon3Button()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon3Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon3);
            BestWeapon3 = null;
            StateItemsWasChanged = true;
        }

        private void EquipBestItemWeapon2Button()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon2Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon2);
            BestWeapon2 = null;
            StateItemsWasChanged = true;
        }

        private void EquipBestItemWeapon1Button()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon1Slot);
            _inventory.Call("ProcessEquipItem", BestWeapon1);
            BestWeapon1 = null;
            StateItemsWasChanged = true;
        }

        private void EquipBestItemHarnessButton()
        {
            _inventory.Call("ProcessEquipItem", BestHarness);
            BestHarness = null;
            StateItemsWasChanged = true;
        }

        private void EquipBestItemMountButton()
        {
            _inventory.Call("ProcessEquipItem", BestMount);
            BestMount = null;
            StateItemsWasChanged = true;
        }

        private void EquipBestItemBootButton()
        {
            _inventory.Call("ProcessEquipItem", BestBoot);
            BestBoot = null;
            StateItemsWasChanged = true;
        }

        private void EquipBestItemGloveButton()
        {
            _inventory.Call("ProcessEquipItem", BestGlove);
            BestGlove = null;
            StateItemsWasChanged = true;
        }

        private void EquipBestItemArmorButton()
        {
            _inventory.Call("ProcessEquipItem", BestArmor);
            BestArmor = null;
            StateItemsWasChanged = true;
        }

        private void EquipBestItemCloakButton()
        {
            _inventory.Call("ProcessEquipItem", BestCloak);
            BestCloak = null;
            StateItemsWasChanged = true;
        }

        private void EquipBestItemHelmButton()
        {
            _inventory.Call("ProcessEquipItem", BestHelmet);
            BestHelmet = null;
            StateItemsWasChanged = true;
        }



        public void FindBestHelm()
        {
            BestHelmet = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HeadArmor && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (BestHelmet != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(BestHelmet))
                        {
                            BestHelmet = item;
                        }
                    }
                    else
                    {
                        BestHelmet = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HeadArmor && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestHelmet != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(BestHelmet))
                            {
                                BestHelmet = item;
                            }
                        }
                        else
                        {
                            BestHelmet = item;
                        }
                    }
                }
            }

            if (GetFullArmor(_inventory.CharacterHelmSlot) >= GetFullArmor(BestHelmet))
            {
                BestHelmet = null;
            }
        }

        public void FindBestCloak()
        {
            BestCloak = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.Cape && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (BestCloak != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(BestCloak))
                        {
                            BestCloak = item;
                        }
                    }
                    else
                    {
                        BestCloak = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.Cape && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestCloak != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(BestCloak))
                            {
                                BestCloak = item;
                            }
                        }
                        else
                        {
                            BestCloak = item;
                        }
                    }
                }
            }
            if (GetFullArmor(_inventory.CharacterCloakSlot) >= GetFullArmor(BestCloak))
            {
                BestCloak = null;
            }
        }

        public void FindBestArmor()
        {
            BestArmor = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.BodyArmor && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (BestArmor != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(BestArmor))
                        {
                            BestArmor = item;
                        }
                    }
                    else
                    {
                        BestArmor = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.BodyArmor && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestArmor != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(BestArmor))
                            {
                                BestArmor = item;
                            }
                        }
                        else
                        {
                            BestArmor = item;
                        }
                    }
                }
            }
            if (GetFullArmor(_inventory.CharacterTorsoSlot) >= GetFullArmor(BestArmor))
            {
                BestArmor = null;
            }
        }

        public void FindBestGlove()
        {
            BestGlove = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HandArmor && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (BestGlove != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(BestGlove))
                        {
                            BestGlove = item;
                        }
                    }
                    else
                    {
                        BestGlove = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HandArmor && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestGlove != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(BestGlove))
                            {
                                BestGlove = item;
                            }
                        }
                        else
                        {
                            BestGlove = item;
                        }
                    }
                }
            }
            if (GetFullArmor(_inventory.CharacterGloveSlot) >= GetFullArmor(BestGlove))
            {
                BestGlove = null;
            }
        }

        public void FindBestBoot()
        {
            BestBoot = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.LegArmor && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (BestBoot != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(BestBoot))
                        {
                            BestBoot = item;
                        }
                    }
                    else
                    {
                        BestBoot = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.LegArmor && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestBoot != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(BestBoot))
                            {
                                BestBoot = item;
                            }
                        }
                        else
                        {
                            BestBoot = item;
                        }
                    }
                }
            }
            if (GetFullArmor(_inventory.CharacterBootSlot) >= GetFullArmor(BestBoot))
            {
                BestBoot = null;
            }
        }

        public void FindBestMount()
        {
            BestMount = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.Horse && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (BestMount != null)
                    {
                        if (GetMountEffectiveness(item) > GetMountEffectiveness(BestMount))
                        {
                            BestMount = item;
                        }
                    }
                    else
                    {
                        BestMount = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.Horse && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestMount != null)
                        {
                            if (GetMountEffectiveness(item) > GetMountEffectiveness(BestMount))
                            {
                                BestMount = item;
                            }
                        }
                        else
                        {
                            BestMount = item;
                        }
                    }
                }
            }
            if (GetMountEffectiveness(_inventory.CharacterMountSlot) >= GetMountEffectiveness(BestMount)) 
            {
                BestMount = null;
            }
        }

        public void FindBestHarness()
        {
            BestHarness = null;

            if (_inventory.CharacterMountSlot.StringId == "special_camel")
                return;

            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HorseHarness && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (BestHarness != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(BestHarness))
                        {
                            BestHarness = item;
                        }
                    }
                    else
                    {
                        BestHarness = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HorseHarness && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestHarness != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(BestHarness))
                            {
                                BestHarness = item;
                            }
                        }
                        else
                        {
                            BestHarness = item;
                        }
                    }
                }
            }
            if (GetFullArmor(_inventory.CharacterMountArmorSlot) >= GetFullArmor(BestHarness))
            {
                BestHarness = null;
            }
        }
    }
}
