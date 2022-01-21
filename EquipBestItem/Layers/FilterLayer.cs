using System;
using EquipBestItem.Behaviors;
using EquipBestItem.ViewModels;
using TaleWorlds.Engine.GauntletUI;

namespace EquipBestItem.Layers
{
    internal class FilterLayer : GauntletLayer
    {
        private FilterVM _vm;


        public FilterLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _vm = new FilterVM();

            LoadMovie("EBI_Filter_Buttons", _vm);
        }

        bool IsAltPressed = false;
        bool _lastSetState;

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);

            if (_lastSetState != InventoryBehavior.Inventory.IsInWarSet)
            {
                _vm.RefreshValues();
                _lastSetState = InventoryBehavior.Inventory.IsInWarSet;
            }

            if (InventoryBehavior.Inventory.IsInWarSet)
            {
                if (_vm.CharacterSettings.Name != InventoryBehavior.Inventory.CurrentCharacterName)
                    _vm.RefreshValues();
            }
            else
            {
                if (_vm.CharacterSettings.Name.Replace("_civil", null) != InventoryBehavior.Inventory.CurrentCharacterName)
                    _vm.RefreshValues();
            }

            if (TaleWorlds.InputSystem.Input.IsKeyDown(TaleWorlds.InputSystem.InputKey.LeftAlt) && !IsAltPressed)
            {
                IsAltPressed = true;
                if (_vm.IsHiddenFilterLayer && (!_vm.IsArmorSlotHidden || !_vm.IsMountSlotHidden || !_vm.IsMeleeWeaponSlotHidden || !_vm.IsRangeWeaponSlotHidden))
                    _vm.IsArmorSlotHidden = _vm.IsMountSlotHidden = _vm.IsMeleeWeaponSlotHidden = _vm.IsRangeWeaponSlotHidden = true;
                if (!_vm.IsHiddenFilterLayer)
                {
                    _vm.IsHiddenFilterLayer = true;
                }

                _vm.IsLayerHidden = true;
            }
            if (TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftAlt) && IsAltPressed)
            {
                IsAltPressed = false;
                if (_vm.IsHiddenFilterLayer && (!_vm.IsArmorSlotHidden || !_vm.IsMountSlotHidden || !_vm.IsMeleeWeaponSlotHidden || !_vm.IsRangeWeaponSlotHidden)) _vm.IsHiddenFilterLayer = false;
                _vm.IsLayerHidden = false;
            }
        }
    }
}
