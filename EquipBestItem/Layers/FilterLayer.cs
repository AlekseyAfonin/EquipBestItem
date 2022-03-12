﻿using EquipBestItem.ViewModels;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;

namespace EquipBestItem.Layers
{
    internal class FilterLayer : GauntletLayer
    {
        private bool _lastSetState;

        //TODO refactor
        private bool _leftMouseButtonWasReleased;
        private FiltersVM _vm;

        private bool IsAltPressed = false;


        public FilterLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _vm = new FiltersVM();
            LoadMovie("EBI_Filters", _vm);
        }

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);

            if (TaleWorlds.InputSystem.Input.IsKeyReleased(InputKey.LeftMouseButton) && !_leftMouseButtonWasReleased)
            {
                _vm.RefreshValues();
                _leftMouseButtonWasReleased = true;
            }

            if (TaleWorlds.InputSystem.Input.IsKeyPressed(InputKey.LeftMouseButton) && _leftMouseButtonWasReleased)
                _leftMouseButtonWasReleased = false;

            // if (TaleWorlds.InputSystem.Input.IsKeyDown(TaleWorlds.InputSystem.InputKey.LeftAlt) && !IsAltPressed)
            // {
            //     IsAltPressed = true;
            //     if (!_vm.IsHiddenFilterLayer)
            //     {
            //         _vm.IsHiddenFilterLayer = true;
            //     }
            //     EventManager<InventoryBehavior>.DelegateEvent.
            //
            //     _vm.IsLayerHidden = true;
            // }
            // if (TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftAlt) && IsAltPressed)
            // {
            //     IsAltPressed = false;
            //     if (_vm.IsHiddenFilterLayer && (!_vm.IsArmorSlotHidden || !_vm.IsMountSlotHidden || !_vm.IsMeleeWeaponSlotHidden || !_vm.IsRangeWeaponSlotHidden)) _vm.IsHiddenFilterLayer = false;
            //     _vm.IsLayerHidden = false;
            // }
        }

        protected override void OnFinalize()
        {
            _vm = null;
            base.OnFinalize();
        }
    }
}