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

            this.IsEnabled = false;
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
            if ((GetItemUsage(_inventory.CharacterWeapon4Slot) == "long_bow" || GetItemUsage(_inventory.CharacterWeapon4Slot) == "bow") && GetItemUsage(BestWeapon4) != GetItemUsage(_inventory.CharacterWeapon4Slot))
                BestWeapon4 = null;
            else if (Settings.EBISettings.IsEnableWeapon4Buttom)
                this.IsEnabled = (BestWeapon4 == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestWeapon4 = null;
            
            StateItemsWasChanged = false;
        }

        private void Weapon3ButtonUpdate()
        {
            FindBestWeapon3();
            if ((GetItemUsage(_inventory.CharacterWeapon3Slot) == "long_bow" || GetItemUsage(_inventory.CharacterWeapon3Slot) == "bow") && GetItemUsage(BestWeapon3) != GetItemUsage(_inventory.CharacterWeapon3Slot))
                BestWeapon3 = null;
            else if (Settings.EBISettings.IsEnableWeapon3Buttom)
                this.IsEnabled = (BestWeapon3 == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestWeapon3 = null;
        }

        private void Weapon2ButtonUpdate()
        {
            FindBestWeapon2();
            if ((GetItemUsage(_inventory.CharacterWeapon2Slot) == "long_bow" || GetItemUsage(_inventory.CharacterWeapon2Slot) == "bow") && GetItemUsage(BestWeapon2) != GetItemUsage(_inventory.CharacterWeapon2Slot))
                BestWeapon2 = null;
            else if (Settings.EBISettings.IsEnableWeapon2Buttom)
                this.IsEnabled = (BestWeapon2 == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestWeapon2 = null;
        }

        private void Weapon1ButtonUpdate()
        {
            FindBestWeapon1();
            if ((GetItemUsage(_inventory.CharacterWeapon1Slot) == "long_bow" || GetItemUsage(_inventory.CharacterWeapon1Slot) == "bow") && GetItemUsage(BestWeapon1) != GetItemUsage(_inventory.CharacterWeapon1Slot))
                BestWeapon1 = null;
            else if (Settings.EBISettings.IsEnableWeapon1Buttom)
                this.IsEnabled = (BestWeapon1 == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestWeapon1 = null;
        }

        private void HarnessButtonUpdate()
        {
            FindBestHarness();
            if (Settings.EBISettings.IsEnableHarnessButtom)
                this.IsEnabled = (BestHarness == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestHarness = null;
        }

        private void MountButtonUpdate()
        {
            FindBestMount();
            if (Settings.EBISettings.IsEnableMountButtom)
                this.IsEnabled = (BestMount == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestMount = null;
            
        }

        private void BootButtonUpdate()
        {
            FindBestBoot();
            if (Settings.EBISettings.IsEnableBootButtom)
                this.IsEnabled = (BestBoot == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestBoot = null;
            
        }

        private void GloveButtonUpdate()
        {
            FindBestGlove();
            if (Settings.EBISettings.IsEnableGloveButtom)
                this.IsEnabled = (BestGlove == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestGlove = null;
            
        }

        private void ArmorButtonUpdate()
        {
            FindBestArmor();
            
            if (Settings.EBISettings.IsEnableArmorButtom)
                this.IsEnabled = (BestArmor == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestArmor = null;
        }

        private void CloakButtonUpdate()
        {
            FindBestCloak();
            if (Settings.EBISettings.IsEnableCloakButtom)
                this.IsEnabled = (BestCloak == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestCloak = null;
            
        }

        private void HelmButtonUpdate()
        {
            FindBestHelm();
            if (Settings.EBISettings.IsEnableHelmButtom)
                this.IsEnabled = (BestHelmet == null || !Settings.EBISettings.IsEnabledStandardButtons) ? false : true;
            else
                BestHelmet = null;
            
        }

        private float GetShieldEffectiveness(SPItemVM item)
        {
            if (item == null)
                return 0f;
            if (item.ItemRosterElement.IsEmpty)
                return 0f;
            if (item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
            {
                WeaponComponentData primaryWeapon = item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon;
                float num = 1f;
                switch (primaryWeapon.WeaponClass)
                {
                    case WeaponClass.SmallShield:
                        num = 0.4f;
                        break;
                    case WeaponClass.LargeShield:
                        num = 0.5f;
                        break;
                }
                
                float value = ((float)primaryWeapon.MaxDataValue + 3f * (float)primaryWeapon.BodyArmor + (float)primaryWeapon.ThrustSpeed) / (6f + primaryWeapon.Item.Weight) * 0.13f * num;
                //((float)primaryWeapon.BodyArmor * 60f + (float)primaryWeapon.ThrustSpeed * 10f + (float)primaryWeapon.MaxDataValue * 0.5f + (float)primaryWeapon.WeaponLength * 20f - (float)primaryWeapon.Item.Weight * 10f) * num;
                return value;
            }
            return 0f;
        }

        private bool IsShield(SPItemVM item)
        {
            if (item == null)
                return false;
            if (item.ItemRosterElement.IsEmpty)
                return false;
            if (item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
            {
                if (item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.IsShield)
                    return true;
            }
            return false;
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
                if ((GetItemUsage(item) == "long_bow" || GetItemUsage(item) == "bow") && GetItemUsage(item) != GetItemUsage(_inventory.CharacterWeapon4Slot))
                    continue;
                if (item.TypeId == _inventory.CharacterWeapon4Slot.TypeId &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon4Slot) &&
                    item.CanCharacterUseItem && 
                    _inventory.IsInWarSet && 
                    !item.ItemRosterElement.IsEmpty && 
                    !item.ItemRosterElement.EquipmentElement.IsEmpty &&
                    item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
                {
                    if (BestWeapon4 != null)
                    {
                        if (IsShield(item))
                        {
                            if (GetShieldEffectiveness(item) > GetShieldEffectiveness(BestWeapon4)) BestWeapon4 = item;
                        }
                        else if (GetEffectiveness(item) > GetEffectiveness(BestWeapon4))
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
                    !_inventory.IsInWarSet &&
                    !item.ItemRosterElement.IsEmpty &&
                    !item.ItemRosterElement.EquipmentElement.IsEmpty &&
                    item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestWeapon4 != null)
                        {
                            if (IsShield(item))
                            {
                                if (GetShieldEffectiveness(item) > GetShieldEffectiveness(BestWeapon4)) BestWeapon4 = item;
                            }
                            else if (GetEffectiveness(item) > GetEffectiveness(BestWeapon4))
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
            if (IsShield(_inventory.CharacterWeapon4Slot))
            {
                if (GetShieldEffectiveness(_inventory.CharacterWeapon4Slot) >= GetShieldEffectiveness(BestWeapon4)) 
                    BestWeapon4 = null;
            } else if (GetEffectiveness(_inventory.CharacterWeapon4Slot) >= GetEffectiveness(BestWeapon4))
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
                if ((GetItemUsage(item) == "long_bow" || GetItemUsage(item) == "bow") && GetItemUsage(item) != GetItemUsage(_inventory.CharacterWeapon3Slot))
                    continue;
                if (item.TypeId == _inventory.CharacterWeapon3Slot.TypeId &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon3Slot) &&
                    item.CanCharacterUseItem && 
                    _inventory.IsInWarSet &&
                    !item.ItemRosterElement.IsEmpty &&
                    !item.ItemRosterElement.EquipmentElement.IsEmpty &&
                    item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
                {
                    if (BestWeapon3 != null)
                    {
                        if (IsShield(item))
                        {
                            if (GetShieldEffectiveness(item) > GetShieldEffectiveness(BestWeapon3)) BestWeapon3 = item;
                        }
                        else if (GetEffectiveness(item) > GetEffectiveness(BestWeapon3))
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
                    !_inventory.IsInWarSet &&
                    !item.ItemRosterElement.IsEmpty &&
                    !item.ItemRosterElement.EquipmentElement.IsEmpty &&
                    item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestWeapon3 != null)
                        {
                            if (IsShield(item))
                            {
                                if (GetShieldEffectiveness(item) > GetShieldEffectiveness(BestWeapon3)) BestWeapon3 = item;
                            }
                            else if (GetEffectiveness(item) > GetEffectiveness(BestWeapon3))
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
            if (IsShield(_inventory.CharacterWeapon3Slot))
            {
                if (GetShieldEffectiveness(_inventory.CharacterWeapon3Slot) >= GetShieldEffectiveness(BestWeapon3))
                    BestWeapon3 = null;
            }
            else if (GetEffectiveness(_inventory.CharacterWeapon3Slot) >= GetEffectiveness(BestWeapon3))
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
                if ((GetItemUsage(item) == "long_bow" || GetItemUsage(item) == "bow") && GetItemUsage(item) != GetItemUsage(_inventory.CharacterWeapon2Slot))
                    continue;
                if (item.TypeId == _inventory.CharacterWeapon2Slot.TypeId && 
                    item.CanCharacterUseItem && 
                    _inventory.IsInWarSet &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon2Slot) &&
                    !item.ItemRosterElement.IsEmpty &&
                    !item.ItemRosterElement.EquipmentElement.IsEmpty &&
                    item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
                {
                    if (BestWeapon2 != null)
                    {
                        if (IsShield(item))
                        {
                            if (GetShieldEffectiveness(item) > GetShieldEffectiveness(BestWeapon2)) BestWeapon2 = item;
                        }
                        else if (GetEffectiveness(item) > GetEffectiveness(BestWeapon2))
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
                    !_inventory.IsInWarSet &&
                    !item.ItemRosterElement.IsEmpty &&
                    !item.ItemRosterElement.EquipmentElement.IsEmpty &&
                    item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestWeapon2 != null)
                        {
                            if (IsShield(item))
                            {
                                if (GetShieldEffectiveness(item) > GetShieldEffectiveness(BestWeapon2)) BestWeapon2 = item;
                            }
                            else if (GetEffectiveness(item) > GetEffectiveness(BestWeapon2))
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
            if (IsShield(_inventory.CharacterWeapon2Slot))
            {
                if (GetShieldEffectiveness(_inventory.CharacterWeapon2Slot) >= GetShieldEffectiveness(BestWeapon2))
                    BestWeapon2 = null;
            }
            else if (GetEffectiveness(_inventory.CharacterWeapon2Slot) >= GetEffectiveness(BestWeapon2))
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
                if ((GetItemUsage(item) == "long_bow" || GetItemUsage(item) == "bow") && GetItemUsage(item) != GetItemUsage(_inventory.CharacterWeapon1Slot))
                    continue;
                if (item.TypeId == _inventory.CharacterWeapon1Slot.TypeId &&
                    GetWeaponClass(item) == GetWeaponClass(_inventory.CharacterWeapon1Slot) &&
                    item.CanCharacterUseItem && _inventory.IsInWarSet &&
                    !item.ItemRosterElement.IsEmpty &&
                    !item.ItemRosterElement.EquipmentElement.IsEmpty &&
                    item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
                {
                    if (BestWeapon1 != null)
                    {
                        if (IsShield(item))
                        {
                            if (GetShieldEffectiveness(item) > GetShieldEffectiveness(BestWeapon1)) BestWeapon1 = item;
                        }
                        else if (GetEffectiveness(item) > GetEffectiveness(BestWeapon1))
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
                    item.CanCharacterUseItem && !_inventory.IsInWarSet &&
                    !item.ItemRosterElement.IsEmpty &&
                    !item.ItemRosterElement.EquipmentElement.IsEmpty &&
                    item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
                {
                    if (item.IsCivilianItem)
                    {
                        if (BestWeapon1 != null)
                        {
                            if (IsShield(item))
                            {
                                if (GetShieldEffectiveness(item) > GetShieldEffectiveness(BestWeapon1)) BestWeapon1 = item;
                            }
                            else if (GetEffectiveness(item) > GetEffectiveness(BestWeapon1))
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

            if (IsShield(_inventory.CharacterWeapon1Slot))
            {
                if (GetShieldEffectiveness(_inventory.CharacterWeapon1Slot) >= GetShieldEffectiveness(BestWeapon1))
                    BestWeapon1 = null;
            }
            else if (GetEffectiveness(_inventory.CharacterWeapon1Slot) >= GetEffectiveness(BestWeapon1))
            {
                BestWeapon1 = null;
            }
        }

        private string GetItemUsage(SPItemVM item)
        {
            if (item == null || item.ItemRosterElement.IsEmpty || item.ItemRosterElement.EquipmentElement.IsEmpty || !item.ItemRosterElement.EquipmentElement.Item.HasWeaponComponent)
                return "";
            string value = item.ItemRosterElement.EquipmentElement.Item.PrimaryWeapon.ItemUsage;
            return value;
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
