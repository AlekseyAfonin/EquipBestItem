using System.Collections.Generic;
using EquipBestItem.Layers;
using EquipBestItem.Settings;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;

namespace EquipBestItem
{
    public class EquipBestItemManager
    {
        public SPInventoryVM Inventory;
        private InventoryGauntletScreen InventoryScreen;
        public MBList<ViewModel> ViewModels = new();

        public static EquipBestItemManager Instance { get; } = new();

        private IReadOnlyList<ScreenLayer> ScreenLayers => InventoryScreen?.Layers;

        private void ScreenManager_OnPushScreen(ScreenBase pushedScreen)
        {
            if (pushedScreen is not InventoryGauntletScreen screen) return;

            InventoryScreen = screen;
            Inventory = InventoryScreen.GetField("_dataSource") as SPInventoryVM;

            AddLayer(new MainLayer(17));
            AddLayer(new FilterLayer(17));
        }

        public void SubscribeEvents()
        {
            ScreenManager.OnPushScreen += ScreenManager_OnPushScreen;
            ScreenManager.OnPopScreen += ScreenManager_OnPopScreen;
        }

        public void UnsubscribeEvents()
        {
            ScreenManager.OnPushScreen -= ScreenManager_OnPushScreen;
            ScreenManager.OnPopScreen -= ScreenManager_OnPopScreen;
        }

        private void ScreenManager_OnPopScreen(ScreenBase poppedScreen)
        {
            if (poppedScreen is not InventoryGauntletScreen) return;

            SettingsLoader.Instance.SaveSettings();
            SettingsLoader.Instance.SaveCharacterSettings();
            RemoveLayer(FindLayer<MainLayer>());
            RemoveLayer(FindLayer<FilterLayer>());
            InventoryScreen = null;
        }

        public T FindLayer<T>() where T : ScreenLayer
        {
            foreach (var layer1 in ScreenLayers)
                if (layer1 is T layer2)
                    return layer2;
            return default;
        }

        public void AddLayer(ScreenLayer screenLayer)
        {
            InventoryScreen?.AddLayer(screenLayer);
            screenLayer.InputRestrictions.SetInputRestrictions();
        }

        public void RemoveLayer(ScreenLayer screenLayer)
        {
            InventoryScreen.RemoveLayer(screenLayer);
        }
    }
}