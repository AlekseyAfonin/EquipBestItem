using EquipBestItem.ViewModels;
using TaleWorlds.Engine.GauntletUI;

namespace EquipBestItem.Layers
{
    class MainLayer : GauntletLayer
    {
        MainViewModel _viewModel;

        public MainLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _viewModel = new MainViewModel();
            LoadMovie("EBI_Buttons", _viewModel);
        }

        bool _leftMouseButtonWasReleased = false;

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);

            if (TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && !_leftMouseButtonWasReleased)
            {
                _viewModel.RefreshValues();
                _leftMouseButtonWasReleased = true;
            }

            if (TaleWorlds.InputSystem.Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.LeftMouseButton) && _leftMouseButtonWasReleased)
            {
                _leftMouseButtonWasReleased = false;
            }

        }
    }
}
