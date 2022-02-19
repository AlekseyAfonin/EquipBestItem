using EquipBestItem.ViewModels;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;

namespace EquipBestItem.Layers
{
    internal class FilterLayer : GauntletLayer
    {
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
            _vm.IsLayerHidden = EquipBestItemManager.Instance.IsLayersHidden;

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