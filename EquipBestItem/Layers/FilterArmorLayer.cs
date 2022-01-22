using EquipBestItem.ViewModels;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;

namespace EquipBestItem.Layers
{
    internal class FilterArmorLayer : GauntletLayer
    {
        private FilterArmorVM _vm;
        
        public FilterArmorLayer(int localOrder, string header, SPInventoryVM inventory, EquipmentIndex selectedIndex, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _vm = new FilterArmorVM(inventory, header, selectedIndex);
            LoadMovie("EBI_Filter_Armor", _vm);
        }

        protected override void OnFinalize()
        {
            _vm = null;
            base.OnFinalize();
        }
    }
}