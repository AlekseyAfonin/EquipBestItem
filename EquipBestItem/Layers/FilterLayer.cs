using EquipBestItem.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;

namespace EquipBestItem.Layers
{
    internal class FilterLayer : GauntletLayer
    {
        FilterViewModel _viewModel;

        public FilterLayer(int localOrder, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _viewModel = new FilterViewModel();
            this.LoadMovie("FiltersLayer", this._viewModel);
        }

        bool IsAltPressed = false;
        bool _lastSetState;

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);

            if (_lastSetState != EquipBestItemViewModel._inventory.IsInWarSet)
            {
                _viewModel.RefreshValues();
                _lastSetState = EquipBestItemViewModel._inventory.IsInWarSet;
            }

            if (EquipBestItemViewModel.CurrentCharacterName != _viewModel.CharacterSettings.Name)
                _viewModel.RefreshValues();

            if (EquipBestItemViewModel.IsEquipCurrentCharacterButtonEnabled != _viewModel.IsEquipCurrentCharacterButtonEnabled)
                _viewModel.RefreshValues();

            if (TaleWorlds.InputSystem.Input.IsKeyDown(TaleWorlds.InputSystem.InputKey.LeftAlt) && !IsAltPressed)
            {
                IsAltPressed = true;
                if (this._viewModel.IsHiddenFilterLayer && (!this._viewModel.IsArmorSlotHidden || !this._viewModel.IsMountSlotHidden || !this._viewModel.IsWeaponSlotHidden))
                    this._viewModel.IsArmorSlotHidden = this._viewModel.IsMountSlotHidden = this._viewModel.IsWeaponSlotHidden = true;
                if (!this._viewModel.IsHiddenFilterLayer)
                {
                    this._viewModel.IsHiddenFilterLayer = true;
                }

                this._viewModel.IsLayerHidden = true;


                if (SettingsLoader.Debug)
                    InformationManager.DisplayMessage(new InformationMessage("Layout IsKeyDown() " + Game.Current.ApplicationTime.ToString()));

            }
            if (TaleWorlds.InputSystem.Input.IsKeyReleased(TaleWorlds.InputSystem.InputKey.LeftAlt) && IsAltPressed)
            {
                IsAltPressed = false;
                if (this._viewModel.IsHiddenFilterLayer && (!this._viewModel.IsArmorSlotHidden || !this._viewModel.IsMountSlotHidden || !this._viewModel.IsWeaponSlotHidden)) this._viewModel.IsHiddenFilterLayer = false;
                this._viewModel.IsLayerHidden = false;

                if (SettingsLoader.Debug)
                    InformationManager.DisplayMessage(new InformationMessage("Layout IsKeyReleased() " + Game.Current.ApplicationTime.ToString()));
            }
        }
    }
}
