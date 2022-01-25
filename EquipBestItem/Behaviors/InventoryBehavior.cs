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
        //TODO
        public static InventoryGauntletScreen InventoryScreen;

        private void AddNewInventoryLayer(TutorialContextChangedEvent tutorialContextChangedEvent)
        {
            try
            {
                if (tutorialContextChangedEvent.NewContext == TutorialContexts.InventoryScreen)
                {
                    if (ScreenManager.TopScreen is InventoryGauntletScreen)
                    {
                        InventoryScreen = ScreenManager.TopScreen as InventoryGauntletScreen;
                        
                        _mainLayer = new MainLayer(17);
                        InventoryScreen?.AddLayer(_mainLayer);
                        _mainLayer.InputRestrictions.SetInputRestrictions();

                        _filterLayer = new FilterLayer(17);
                        InventoryScreen?.AddLayer(_filterLayer);
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
                        if (InventoryScreen != null && _mainLayer != null)
                        {
                            SettingsLoader.Instance.SaveSettings();
                            SettingsLoader.Instance.SaveCharacterSettings();
                            InventoryScreen.RemoveLayer(_mainLayer);
                            InventoryScreen = null;
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
