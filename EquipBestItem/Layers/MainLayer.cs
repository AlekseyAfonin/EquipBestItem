using EquipBestItem.ViewModels;
using SandBox.GauntletUI;
using SandBox.View.Map;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.MountAndBlade.View.Missions;
using TaleWorlds.MountAndBlade.View.Screen;

namespace EquipBestItem.Layers
{
    class MainLayer : GauntletLayer
    {
        private MainVM _vm;

        public MainLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            
            _vm = new MainVM(ScreenManager.TopScreen as InventoryGauntletScreen);
            LoadMovie("EBI_Main", _vm);
        }

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

        }

        protected override void OnFinalize()
        {
            base.OnFinalize();
            _vm = null;
        }
    }
}
