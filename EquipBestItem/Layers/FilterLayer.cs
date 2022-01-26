using EquipBestItem.ViewModels;
using SandBox.GauntletUI;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;

namespace EquipBestItem.Layers
{
    internal class FilterLayer : GauntletLayer
    {
        private FiltersVM _vm;


        public FilterLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _vm = new FiltersVM(ScreenManager.TopScreen as InventoryGauntletScreen);
            LoadMovie("EBI_Filters", _vm);
        }

        //bool IsAltPressed = false;
        //bool _lastSetState;

        //TODO refactor
        bool _leftMouseButtonWasReleased;
        
        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);
            
            if (TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && !_leftMouseButtonWasReleased)
            {
                _vm.RefreshValues();
                _leftMouseButtonWasReleased = true;
            }

            if (TaleWorlds.InputSystem.Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && _leftMouseButtonWasReleased)
            {
                _leftMouseButtonWasReleased = false;
            }

            // if (TaleWorlds.InputSystem.Input.IsKeyDown(TaleWorlds.InputSystem.InputKey.LeftAlt) && !IsAltPressed)
            // {
            //     IsAltPressed = true;
            //     if (_vm.IsHiddenFilterLayer && (!_vm.IsArmorSlotHidden || !_vm.IsMountSlotHidden || !_vm.IsMeleeWeaponSlotHidden || !_vm.IsRangeWeaponSlotHidden))
            //         _vm.IsArmorSlotHidden = _vm.IsMountSlotHidden = _vm.IsMeleeWeaponSlotHidden = _vm.IsRangeWeaponSlotHidden = true;
            //     if (!_vm.IsHiddenFilterLayer)
            //     {
            //         _vm.IsHiddenFilterLayer = true;
            //     }
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
