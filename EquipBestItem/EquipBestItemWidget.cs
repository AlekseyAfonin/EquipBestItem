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
        string _lastCharacterName;
        MBBindingList<SPItemVM> _lastInventory;
        bool _lastWarSetState = true;
        InventoryGauntletScreen _inventoryScreen;
        SPItemVM _bestHelmet;
        SPItemVM _bestCloak;
        SPItemVM _bestArmor;
        SPItemVM _bestGlove;
        SPItemVM _bestBoot;
        SPItemVM _bestMount;
        SPItemVM _bestHarness;

        public EquipBestItemWidget(UIContext context) : base(context)
        {

            if (ScreenManager.TopScreen is InventoryGauntletScreen)
            {
                _inventoryScreen = (InventoryGauntletScreen)ScreenManager.TopScreen;
            }
            _inventory = (SPInventoryVM)_inventoryScreen.GetField("_dataSource");

            _lastInventory = new MBBindingList<SPItemVM>();

            RightItemListVMCopy();

        }

        public void RightItemListVMCopy()
        {
            _lastInventory.Clear();

            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                _lastInventory.Add(item);
            }
        }

        public bool IsRightItemListVMIdentity()
        {
            int i = 0;

            if (_inventory.RightItemListVM.Count < 2 || _lastInventory.Count < 2)
                return false;

            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (!_lastInventory[i].Equals(item))
                    return false;
            }
            return true;
        }

        public void BestItemsClear()
        {
            if (_bestHelmet != null || _bestCloak != null || _bestArmor != null || _bestGlove != null || _bestBoot != null || _bestMount != null || _bestHarness != null)
            _bestHelmet = null;
            _bestCloak = null;
            _bestArmor = null;
            _bestGlove = null;
            _bestBoot = null;
            _bestMount = null;
            _bestHarness = null;
        }

        public void DisableButtons()
        {
            if (this.IsEnabled == true)
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

        protected override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);


            if (_inventory.RightItemListVM.Count == 0)
            {
                BestItemsClear();
                DisableButtons();
            }

            if (_lastCharacterName != _inventory.CurrentCharacterName || !IsRightItemListVMIdentity() || _lastWarSetState != _inventory.IsInWarSet)
            {
                this.IsEnabled = false;

                FindBestHelm();
                if (_bestHelmet != null && GetFullArmor(_bestHelmet) > GetFullArmor(_inventory.CharacterHelmSlot) && this.Id == "EquipBestItemHelmButton")
                    this.IsEnabled = true;

                FindBestCloak();
                if (_bestCloak != null && GetFullArmor(_bestCloak) > GetFullArmor(_inventory.CharacterCloakSlot) && this.Id == "EquipBestItemCloakButton")
                    this.IsEnabled = true;

                FindBestArmor();
                if (_bestArmor != null && GetFullArmor(_bestArmor) > GetFullArmor(_inventory.CharacterTorsoSlot) && this.Id == "EquipBestItemArmorButton")
                    this.IsEnabled = true;

                FindBestGlove();
                if (_bestGlove != null && GetFullArmor(_bestGlove) > GetFullArmor(_inventory.CharacterGloveSlot) && this.Id == "EquipBestItemGloveButton")
                    this.IsEnabled = true;

                FindBestBoot();
                if (_bestBoot != null && GetFullArmor(_bestBoot) > GetFullArmor(_inventory.CharacterBootSlot) && this.Id == "EquipBestItemBootButton")
                    this.IsEnabled = true;

                FindBestMount();
                if (_bestMount != null && GetEffectiveness(_bestMount) > GetEffectiveness(_inventory.CharacterMountSlot) && this.Id == "EquipBestItemMountButton")
                    this.IsEnabled = true;

                FindBestHarness();
                if (_bestHarness != null && GetFullArmor(_bestHarness) > GetFullArmor(_inventory.CharacterMountArmorSlot) && this.Id == "EquipBestItemHarnessButton")
                    this.IsEnabled = true;

                _lastWarSetState = _inventory.IsInWarSet;
                _lastCharacterName = _inventory.CurrentCharacterName;
                RightItemListVMCopy();
            }


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
                default:
                    break;
            }
        }

        private void EquipBestItemHarnessButton()
        {
            _inventory.Call("ProcessEquipItem", _bestHarness);
            this.IsEnabled = false;
            _bestHarness = null;
        }

        private void EquipBestItemMountButton()
        {
            _inventory.Call("ProcessEquipItem", _bestMount);
            this.IsEnabled = false;
            _bestMount = null;
        }

        private void EquipBestItemBootButton()
        {
            _inventory.Call("ProcessEquipItem", _bestBoot);
            this.IsEnabled = false;
            _bestBoot = null;
        }

        private void EquipBestItemGloveButton()
        {
            _inventory.Call("ProcessEquipItem", _bestGlove);
            this.IsEnabled = false;
            _bestGlove = null;
        }

        private void EquipBestItemArmorButton()
        {
            _inventory.Call("ProcessEquipItem", _bestArmor);
            this.IsEnabled = false;
            _bestArmor = null;
        }

        private void EquipBestItemCloakButton()
        {
            _inventory.Call("ProcessEquipItem", _bestCloak);
            this.IsEnabled = false;
            _bestCloak = null;
        }

        private void EquipBestItemHelmButton()
        {
            _inventory.Call("ProcessEquipItem", _bestHelmet);
            this.IsEnabled = false;
            _bestHelmet = null;
        }



        public void FindBestHelm()
        {
            _bestHelmet = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HeadArmor && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (_bestHelmet != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(_bestHelmet))
                        {
                            _bestHelmet = item;
                        }
                    }
                    else
                    {
                        _bestHelmet = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HeadArmor && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (_bestHelmet != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(_bestHelmet))
                            {
                                _bestHelmet = item;
                            }
                        }
                        else
                        {
                            _bestHelmet = item;
                        }
                    }
                }
            }
        }

        public void FindBestCloak()
        {
            _bestCloak = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.Cape && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (_bestCloak != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(_bestCloak))
                        {
                            _bestCloak = item;
                        }
                    }
                    else
                    {
                        _bestCloak = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.Cape && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (_bestCloak != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(_bestCloak))
                            {
                                _bestCloak = item;
                            }
                        }
                        else
                        {
                            _bestCloak = item;
                        }
                    }
                }
            }
        }

        public void FindBestArmor()
        {
            _bestArmor = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.BodyArmor && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (_bestArmor != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(_bestArmor))
                        {
                            _bestArmor = item;
                        }
                    }
                    else
                    {
                        _bestArmor = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.BodyArmor && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (_bestArmor != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(_bestArmor))
                            {
                                _bestArmor = item;
                            }
                        }
                        else
                        {
                            _bestArmor = item;
                        }
                    }
                }
            }
        }

        public void FindBestGlove()
        {
            _bestGlove = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HandArmor && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (_bestGlove != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(_bestGlove))
                        {
                            _bestGlove = item;
                        }
                    }
                    else
                    {
                        _bestGlove = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HandArmor && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (_bestGlove != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(_bestGlove))
                            {
                                _bestGlove = item;
                            }
                        }
                        else
                        {
                            _bestGlove = item;
                        }
                    }
                }
            }
        }

        public void FindBestBoot()
        {
            _bestBoot = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.LegArmor && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (_bestBoot != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(_bestBoot))
                        {
                            _bestBoot = item;
                        }
                    }
                    else
                    {
                        _bestBoot = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.LegArmor && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (_bestBoot != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(_bestBoot))
                            {
                                _bestBoot = item;
                            }
                        }
                        else
                        {
                            _bestBoot = item;
                        }
                    }
                }
            }
        }

        public void FindBestMount()
        {
            _bestMount = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.Horse && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (_bestMount != null)
                    {
                        if (item.ItemRosterElement.EquipmentElement.Item.Effectiveness > _bestMount.ItemRosterElement.EquipmentElement.Item.Effectiveness)
                        {
                            _bestMount = item;
                        }
                    }
                    else
                    {
                        _bestMount = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.Horse && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (_bestMount != null)
                        {
                            if (item.ItemRosterElement.EquipmentElement.Item.Effectiveness > _bestMount.ItemRosterElement.EquipmentElement.Item.Effectiveness)
                            {
                                _bestMount = item;
                            }
                        }
                        else
                        {
                            _bestMount = item;
                        }
                    }
                }
            }
        }

        public void FindBestHarness()
        {
            _bestHarness = null;
            foreach (SPItemVM item in _inventory.RightItemListVM)
            {
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HorseHarness && item.CanCharacterUseItem && _inventory.IsInWarSet)
                {
                    if (_bestHarness != null)
                    {
                        if (GetFullArmor(item) > GetFullArmor(_bestHarness))
                        {
                            _bestHarness = item;
                        }
                    }
                    else
                    {
                        _bestHarness = item;
                    }
                }
                if (item.TypeId == (int)ItemObject.ItemTypeEnum.HorseHarness && item.CanCharacterUseItem && !_inventory.IsInWarSet)
                {
                    if (item.IsCivilianItem)
                    {
                        if (_bestHarness != null)
                        {
                            if (GetFullArmor(item) > GetFullArmor(_bestHarness))
                            {
                                _bestHarness = item;
                            }
                        }
                        else
                        {
                            _bestHarness = item;
                        }
                    }
                }
            }
        }
    }
}
