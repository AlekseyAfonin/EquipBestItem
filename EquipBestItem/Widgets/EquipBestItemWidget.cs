using System;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Engine.GauntletUI;
using EquipBestItem.Behaviors;
using TaleWorlds.InputSystem;

namespace EquipBestItem
{
    public class EquipBestItemWidget : ButtonWidget
    {
        EquipBestItemViewModel _viewModel;

        public EquipBestItemWidget(UIContext context) : base(context)
        {
            _viewModel = new EquipBestItemViewModel();
        }

        protected override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);


            if (this.Id == "EquipBestItemHelmButton" && this.IsEnabled != EquipBestItemViewModel.IsHelmButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsHelmButtonEnabled;
            if (this.Id == "EquipBestItemCloakButton" && this.IsEnabled != EquipBestItemViewModel.IsCloakButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsCloakButtonEnabled;
            if (this.Id == "EquipBestItemArmorButton" && this.IsEnabled != EquipBestItemViewModel.IsArmorButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsArmorButtonEnabled;
            if (this.Id == "EquipBestItemGloveButton" && this.IsEnabled != EquipBestItemViewModel.IsGloveButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsGloveButtonEnabled;
            if (this.Id == "EquipBestItemBootButton" && this.IsEnabled != EquipBestItemViewModel.IsBootButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsBootButtonEnabled;
            if (this.Id == "EquipBestItemMountButton" && this.IsEnabled != EquipBestItemViewModel.IsMountButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsMountButtonEnabled;
            if (this.Id == "EquipBestItemHarnessButton" && this.IsEnabled != EquipBestItemViewModel.IsHarnessButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsHarnessButtonEnabled;
            if (this.Id == "EquipBestItemWeapon1Button" && this.IsEnabled != EquipBestItemViewModel.IsWeapon1ButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsWeapon1ButtonEnabled;
            if (this.Id == "EquipBestItemWeapon2Button" && this.IsEnabled != EquipBestItemViewModel.IsWeapon2ButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsWeapon2ButtonEnabled;
            if (this.Id == "EquipBestItemWeapon3Button" && this.IsEnabled != EquipBestItemViewModel.IsWeapon3ButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsWeapon3ButtonEnabled;
            if (this.Id == "EquipBestItemWeapon4Button" && this.IsEnabled != EquipBestItemViewModel.IsWeapon4ButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsWeapon4ButtonEnabled;
            if (this.Id == "EquipBestItemAllButton" && this.IsEnabled != EquipBestItemViewModel.IsEquipAllButtonEnabled)
                this.IsEnabled = EquipBestItemViewModel.IsEquipAllButtonEnabled;


        }

        protected override void OnClick()
        {
            _viewModel.RefreshValues();
            //InformationManager.DisplayMessage(new InformationMessage(this.Id + " OnClick() " + Game.Current.ApplicationTime.ToString()));
        }
    }
}
