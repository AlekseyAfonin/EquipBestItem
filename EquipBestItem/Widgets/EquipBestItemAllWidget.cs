using System;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace EquipBestItem
{
    public class EquipBestItemAllWidget : ButtonWidget
    {
        //EquipBestItemViewModel _viewModel;

        public EquipBestItemAllWidget(UIContext context) : base(context)
        {
            //_viewModel = new EquipBestItemViewModel();
            this.IsEnabled = Settings.SettingsLoader.Instance.Settings.IsEnabledEquipAllButton;
        }
        
        protected override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
            //if (this.IsEnabled != EquipBestItemViewModel.IsEquipAllButtonEnabled) this.IsEnabled = EquipBestItemViewModel.IsEquipAllButtonEnabled;

            //if (this.Children[0].IsDisabled != this.IsDisabled) this.Children[0].IsDisabled = this.IsDisabled;

            //if (EquipBestItemViewModel.IsEquipAllActivated)
            //{
            //    EquipBestItemViewModel.EquipAll();
            //}

            //if (EquipBestItemViewModel.IsAllBestItemsNull())
            //{
            //    if (EquipBestItemViewModel.IsEquipAllActivated)
            //    {
            //        EquipBestItemViewModel.IsEquipAllActivated = false;
            //        EquipBestItemViewModel.UpdateValues();
            //    }
            //}
        }
        
        protected override void OnClick()
        {
            EquipBestItemViewModel.EquipEveryCharacter();
            //InformationManager.DisplayMessage(new InformationMessage(this.Id + " OnClick()"));
        }
    }
}

