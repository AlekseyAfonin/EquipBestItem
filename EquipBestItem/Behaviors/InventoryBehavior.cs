using EquipBestItem.Layers;
using SandBox.GauntletUI;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;

namespace EquipBestItem
{
    class InventoryBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            Game.Current.EventManager.RegisterEvent(new Action<TutorialContextChangedEvent>(this.AddNewInventoryLayer));
        }

        public static SPInventoryVM Inventory;
        InventoryGauntletScreen _inventoryScreen;
        GauntletLayer _mainLayer;
        FilterLayer _filterLayer;

        private void AddNewInventoryLayer(TutorialContextChangedEvent tutorialContextChangedEvent)
        {
            try
            {
                if (tutorialContextChangedEvent.NewContext == TutorialContexts.InventoryScreen)
                {
                    if (ScreenManager.TopScreen is InventoryGauntletScreen)
                    {
                        _inventoryScreen = ScreenManager.TopScreen as InventoryGauntletScreen;
                        Inventory = _inventoryScreen.GetField("_dataSource") as SPInventoryVM;

                        this._mainLayer = new MainLayer(1000, "GauntletLayer");
                        _inventoryScreen.AddLayer(this._mainLayer);
                        this._mainLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);

                        _filterLayer = new FilterLayer(1001, "GauntletLayer");
                        _inventoryScreen.AddLayer(this._filterLayer);
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
                        if (_inventoryScreen != null && this._mainLayer != null)
                        {

                            _inventoryScreen.RemoveLayer(this._mainLayer);
                            this._mainLayer = null;
                            SettingsLoader.Instance.SaveSettings();
                            SettingsLoader.Instance.SaveCharacterSettings();
                        }

                        if (_inventoryScreen != null && this._filterLayer != null)
                        {
                            _inventoryScreen.RemoveLayer(this._filterLayer);
                            this._filterLayer = null;
                        }
                    }
                }
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
        }

        public override void SyncData(IDataStore dataStore)
        {

        }
    }
}
