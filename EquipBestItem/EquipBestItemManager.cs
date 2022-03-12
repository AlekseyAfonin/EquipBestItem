using System;
using System.Collections.Generic;
using System.ComponentModel;
using EquipBestItem.Layers;
using EquipBestItem.Settings;
using EquipBestItem.ViewModels;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Engine.Screens;

namespace EquipBestItem
{
    public class EquipBestItemManager
    {
        public SPInventoryVM Inventory;
        public InventoryGauntletScreen InventoryScreen;

        public static EquipBestItemManager Instance { get; } = new();

        private IReadOnlyList<ScreenLayer> ScreenLayers => InventoryScreen?.Layers;

        public event Action HideLayers = delegate { };

        private void OnHideLayers()
        {
            HideLayers();
        }

        private void ScreenManager_OnPushScreen(ScreenBase pushedScreen)
        {
            if (pushedScreen is not InventoryGauntletScreen screen) return;

            InventoryScreen = screen;
            Inventory = InventoryScreen.GetField("_dataSource") as SPInventoryVM;

            AddLayer(new MainLayer(17));
            AddLayer(new FilterLayer(17));

            ((MainVM) FindLayer<MainLayer>()._moviesAndDatasources[0].Item2).PropertyChanged +=
                IsLayerHidden_PropertyChanged;
        }

        private void IsLayerHidden_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainVM.IsLayerHidden))
                OnHideLayers();
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