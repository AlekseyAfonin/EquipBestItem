using EquipBestItem.Layers;
using EquipBestItem.Settings;
using SandBox.GauntletUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;

namespace EquipBestItem.Behaviors
{
    class InventoryBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            Game.Current.EventManager.RegisterEvent(new Action<TutorialContextChangedEvent>(this.AddNewInventoryLayer));
        }

        //Layer with locks and main viewmodel
        GauntletLayer _gauntletLayer;
        EquipBestItemViewModel _viewModel;

        //Layer with filters
        GauntletLayer _gauntletFiltersLayer;
        FilterViewModel _filterViewModel;
        FilterLayer _filterLayer;

        private void AddNewInventoryLayer(TutorialContextChangedEvent tutorialContextChangedEvent)
        {
            try
            {
                if (tutorialContextChangedEvent.NewContext == TutorialContexts.InventoryScreen)
                {
                    if (ScreenManager.TopScreen is InventoryGauntletScreen)
                    {
                        EquipBestItemViewModel.InventoryScreen = ScreenManager.TopScreen as InventoryGauntletScreen;
                        _viewModel = new EquipBestItemViewModel();
                        this._gauntletLayer = new GauntletLayer(1000, "GauntletLayer");
                        this._gauntletLayer.LoadMovie("EBIInventory", _viewModel);
                        EquipBestItemViewModel.InventoryScreen.AddLayer(this._gauntletLayer);
                        this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);

                        _filterLayer = new FilterLayer(1001, "GauntletLayer");
                        _filterViewModel = new FilterViewModel();
                        this._gauntletFiltersLayer = new GauntletLayer(1001, "GauntletLayer");
                        this._gauntletFiltersLayer.LoadMovie("FiltersLayer", _filterViewModel);
                        EquipBestItemViewModel.InventoryScreen.AddLayer(this._filterLayer);
                        this._filterLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
                    }

                    //Temporarily disabled clearing settings file for characters
                    //foreach (CharacterSettings charSettings in SettingsLoader.Instance.CharacterSettings.ToList())
                    //{
                    //    bool flag = false;
                    //    foreach (TroopRosterElement element in EquipBestItemViewModel._inventory.TroopRoster)
                    //    {
                    //        if (charSettings.Name == element.Character.Name.ToString())
                    //        {
                    //            flag = true;
                    //            break;
                    //        }
                    //    }
                    //    if (!flag)
                    //    {
                    //        SettingsLoader.Instance.CharacterSettings.Remove(charSettings);
                    //    }
                    //}
                }
                else
                {
                    if (tutorialContextChangedEvent.NewContext == TutorialContexts.None)
                    {
                        if (EquipBestItemViewModel.InventoryScreen != null && this._gauntletLayer != null)
                        {
                            
                            EquipBestItemViewModel.InventoryScreen.RemoveLayer(this._gauntletLayer);
                            this._gauntletLayer = null;
                            SettingsLoader.Instance.SaveSettings();
                            SettingsLoader.Instance.SaveCharacterSettings();
                        }

                        if (EquipBestItemViewModel.InventoryScreen != null && this._filterLayer != null)
                        {

                            EquipBestItemViewModel.InventoryScreen.RemoveLayer(this._filterLayer);
                            this._filterLayer = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }

        public override void SyncData(IDataStore dataStore)
        {

        }
    }
}
