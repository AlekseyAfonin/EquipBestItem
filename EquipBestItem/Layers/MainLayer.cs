using EquipBestItem.ViewModels;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;

namespace EquipBestItem.Layers
{
    internal class MainLayer : GauntletLayer
    {
        private bool _leftMouseButtonWasReleased;
        private MainVM _vm;

        public MainLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _vm = new MainVM();
            LoadMovie("EBI_Main", _vm);
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
        }

        protected override void OnFinalize()
        {
            _vm = null;
            base.OnFinalize();
        }
    }
}