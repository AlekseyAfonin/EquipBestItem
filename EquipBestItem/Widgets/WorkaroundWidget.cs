using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.GauntletUI;
using TaleWorlds.InputSystem;

//Workaround to update view model state. Once every release mouse button.

namespace EquipBestItem
{
    internal class WorkaroundWidget : Widget
    {
        EquipBestItemViewModel _viewModel;

        public WorkaroundWidget(UIContext context) : base(context)
        {
            _viewModel = new EquipBestItemViewModel();
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

            _leftMouseButtonStatus = Input.IsKeyReleased(InputKey.LeftMouseButton);

            _firstUpdateState = StateChanged();

            if (_delay == 1)
            {
                EquipBestItemViewModel.UpdateCurrentCharacterName();
                _viewModel.RefreshValues();
            }



        }
    }
}
