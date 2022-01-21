using System;
using System.Collections.Generic;
using EquipBestItem.Layers;
using EquipBestItem.Settings;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection;

namespace EquipBestItem.Behaviors
{
    class InventoryBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            Game.Current.EventManager.RegisterEvent(new Action<TutorialContextChangedEvent>(AddNewInventoryLayer));
        }

        public static SPInventoryVM Inventory;
        InventoryGauntletScreen _inventoryScreen;
        GauntletLayer _mainLayer;
        FilterLayer _filterLayer;
        private FilterArmorLayer _filterArmorLayer;

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

                        if (SettingsLoader.Instance.CharacterSettings == null)
                        {
                            //SettingsLoader.Instance.LoadSettings();
                            SettingsLoader.Instance.LoadCharacterSettings();
                        }

                        _mainLayer = new MainLayer(1000, "GauntletLayer");
                        _inventoryScreen.AddLayer(_mainLayer);
                        _mainLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);

                        _filterLayer = new FilterLayer(1001, "GauntletLayer");
                        _inventoryScreen.AddLayer(_filterLayer);
                        _filterLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
                        
                        _filterArmorLayer = new FilterArmorLayer(1002, "GauntletLayer");
                        _inventoryScreen.AddLayer(_filterArmorLayer);
                        _filterArmorLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
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
                            _mainLayer = null;
                            SettingsLoader.Instance.SaveSettings();
                            SettingsLoader.Instance.SaveCharacterSettings();
                        }

                        if (_inventoryScreen != null && _filterLayer != null)
                        {
                            _inventoryScreen.RemoveLayer(_filterLayer);
                            _filterLayer = null;
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
