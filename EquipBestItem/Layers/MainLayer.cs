using TaleWorlds.Engine.GauntletUI;

namespace EquipBestItem
{
    class MainLayer : GauntletLayer
    {
        MainViewModel _viewModel;

        public MainLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _viewModel = new MainViewModel();
            this.LoadMovie("EBIInventory", this._viewModel);
        }

        bool _latestStateStatus;
        bool _leftMouseButtonStatus;
        int _delay;
        bool _firstUpdateState = false;

        private bool StateChanged()
        {

            if (_leftMouseButtonStatus != _latestStateStatus)
            {
                _latestStateStatus = _leftMouseButtonStatus;
                _delay = 0;
                return true;
            }
            _delay++;
            return false;
        }

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);

            _leftMouseButtonStatus = TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftMouseButton);

            _firstUpdateState = StateChanged();

            if (_delay == 1)
            {
                //EquipBestItemViewModel.UpdateCurrentCharacterName();
                _viewModel.RefreshValues();
            }
        }
    }
}
