using System;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace EquipBestItem
{
    public class EquipBestItemAllWidget : ButtonWidget
    {
        SPInventoryVM _inventory;
        InventoryGauntletScreen _inventoryScreen;

        bool IsButtonEquipAllItemWasPress = false;

        public EquipBestItemAllWidget(UIContext context) : base(context)
        {

            if (ScreenManager.TopScreen is InventoryGauntletScreen)
            {
                _inventoryScreen = (InventoryGauntletScreen)ScreenManager.TopScreen;
            }
            _inventory = (SPInventoryVM)_inventoryScreen.GetField("_dataSource");
        }
        
        protected override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
            AllButtonUpdate();

            if (IsAnyButtonEnable() && IsButtonEquipAllItemWasPress)
            {
                if (EquipBestItemWidget.BestArmor != null)
                {
                    EquipBestItemArmorButton();
                    return;
                }
                if (EquipBestItemWidget.BestBoot != null)
                {
                    EquipBestItemBootButton();
                    return;
                }
                if (EquipBestItemWidget.BestCloak != null)
                {
                    EquipBestItemCloakButton();
                    return;
                }
                if (EquipBestItemWidget.BestGlove != null)
                {
                    EquipBestItemGloveButton();
                    return;
                }
                if (EquipBestItemWidget.BestHelmet != null)
                {
                    EquipBestItemHelmButton();
                    return;
                }
                if (EquipBestItemWidget.BestMount != null)
                {
                    EquipBestItemMountButton();
                    return;
                }
                if (EquipBestItemWidget.BestHarness != null)
                {
                    EquipBestItemHarnessButton();
                    return;
                }
                if (EquipBestItemWidget.BestWeapon1 != null)
                {
                    EquipBestItemWeapon1Button();
                    return;
                }
                if (EquipBestItemWidget.BestWeapon2 != null)
                {
                    EquipBestItemWeapon2Button();
                    return;
                }
                if (EquipBestItemWidget.BestWeapon3 != null)
                {
                    EquipBestItemWeapon3Button();
                    return;
                }
                if (EquipBestItemWidget.BestWeapon4 != null)
                {
                    EquipBestItemWeapon4Button();
                    return;
                }
            }
        }

        private bool IsAnyButtonEnable()
        {
            
            if (EquipBestItemWidget.BestArmor != null) return true;
            if (EquipBestItemWidget.BestBoot != null) return true;
            if (EquipBestItemWidget.BestCloak != null) return true;
            if (EquipBestItemWidget.BestGlove != null) return true;
            if (EquipBestItemWidget.BestHelmet != null) return true;
            if (EquipBestItemWidget.BestMount != null) return true;
            if (EquipBestItemWidget.BestHarness != null) return true;
            if (EquipBestItemWidget.BestWeapon1 != null) return true;
            if (EquipBestItemWidget.BestWeapon2 != null) return true;
            if (EquipBestItemWidget.BestWeapon3 != null) return true;
            if (EquipBestItemWidget.BestWeapon4 != null) return true;

            return false;
        }


        private void AllButtonUpdate()
        {
            if (IsAnyButtonEnable() && Settings.EBISettings.IsEnabledEquipAllButton)
            {
                if (this.IsEnabled == false) this.IsEnabled = true;
            }
            else
            {
                this.IsEnabled = false;
                IsButtonEquipAllItemWasPress = false;
            }
        }
        
        protected override void OnClick()
        {
            IsButtonEquipAllItemWasPress = true;

        }

        private void EquipBestItemWeapon4Button()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon4Slot);
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestWeapon4);
            EquipBestItemWidget.BestWeapon4 = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }

        private void EquipBestItemWeapon3Button()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon3Slot);
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestWeapon3);
            EquipBestItemWidget.BestWeapon3 = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }

        private void EquipBestItemWeapon2Button()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon2Slot);
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestWeapon2);
            EquipBestItemWidget.BestWeapon2 = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }

        private void EquipBestItemWeapon1Button()
        {
            _inventory.Call("UnequipEquipment", _inventory.CharacterWeapon1Slot);
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestWeapon1);
            EquipBestItemWidget.BestWeapon1 = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }

        private void EquipBestItemHarnessButton()
        {
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestHarness);
            EquipBestItemWidget.BestHarness = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }

        private void EquipBestItemMountButton()
        {
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestMount);
            EquipBestItemWidget.BestMount = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }

        private void EquipBestItemBootButton()
        {
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestBoot);
            EquipBestItemWidget.BestBoot = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }

        private void EquipBestItemGloveButton()
        {
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestGlove);
            EquipBestItemWidget.BestGlove = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }

        private void EquipBestItemArmorButton()
        {
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestArmor);
            EquipBestItemWidget.BestArmor = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }

        private void EquipBestItemCloakButton()
        {
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestCloak);
            EquipBestItemWidget.BestCloak = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }

        private void EquipBestItemHelmButton()
        {
            _inventory.Call("ProcessEquipItem", EquipBestItemWidget.BestHelmet);
            EquipBestItemWidget.BestHelmet = null;
            EquipBestItemWidget.StateItemsWasChanged = true;
        }
    }
}

