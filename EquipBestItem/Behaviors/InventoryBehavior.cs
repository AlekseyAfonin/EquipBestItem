using System;
using EquipBestItem.Layers;
using EquipBestItem.Settings;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;

namespace EquipBestItem.Behaviors
{
    class InventoryBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            Game.Current.EventManager.RegisterEvent(new Action<TutorialContextChangedEvent>(AddNewInventoryLayer));
        }


        private MainLayer _mainLayer;
        private FilterLayer _filterLayer;
        private InventoryGauntletScreen _inventoryScreen;

        private void AddNewInventoryLayer(TutorialContextChangedEvent tutorialContextChangedEvent)
        {
            try
            {
                if (tutorialContextChangedEvent.NewContext == TutorialContexts.InventoryScreen)
                {
                    if (ScreenManager.TopScreen is InventoryGauntletScreen)
                    {
                        _inventoryScreen = ScreenManager.TopScreen as InventoryGauntletScreen;
                        
                        if (SettingsLoader.Instance.CharacterSettings == null)
                        {
                            SettingsLoader.Instance.LoadCharacterSettings();
                        }
                        
                        _mainLayer = new MainLayer(16);
                        _inventoryScreen?.AddLayer(_mainLayer);
                        _mainLayer.InputRestrictions.SetInputRestrictions();

                        _filterLayer = new FilterLayer(16);
                        _inventoryScreen?.AddLayer(_filterLayer);
                        _filterLayer.InputRestrictions.SetInputRestrictions();
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
                        if (_inventoryScreen != null && _mainLayer != null)
                        {
                            _inventoryScreen.RemoveLayer(_mainLayer);
                            _inventoryScreen = null;
                            SettingsLoader.Instance.SaveSettings();
                            SettingsLoader.Instance.SaveCharacterSettings();
                        }
                    }
                }
            }
            catch (MBException e)
            {
                if (SettingsLoader.Instance.Settings.Debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message + e.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}
