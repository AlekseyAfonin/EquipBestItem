using EquipBestItem.ViewModels;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;

namespace EquipBestItem.Layers
{
    internal class FiltersSettingsLayer : GauntletLayer
    {
        private FiltersSettingsVM _vm;
        
        public FiltersSettingsLayer(int localOrder, SPInventoryVM inventory, EquipmentIndex selectedSlot, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            _vm = new FiltersSettingsVM(inventory, selectedSlot);
            LoadMovie("EBI_FiltersSettings", _vm);
            
            switch (selectedSlot)
            {
                case EquipmentIndex.Head:
                {
                    _vm.HeaderText = "Helmet";
                    _vm.IsHiddenBodyArmor = true;
                    _vm.IsHiddenLegArmor = true;
                    _vm.IsHiddenArmArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideWeapon();
                    break;
                }
                case EquipmentIndex.Cape:
                {
                    _vm.HeaderText = "Cloak";
                    _vm.IsHiddenHeadArmor = true;
                    _vm.IsHiddenLegArmor = true;
                    _vm.IsHiddenArmArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideWeapon();
                    break;
                }
                case EquipmentIndex.Body:
                {
                    _vm.HeaderText = "Body Armor";
                    HideHorse();
                    HideHorseHarness();
                    HideWeapon();
                    break;
                }
                case EquipmentIndex.Gloves:
                {
                    _vm.HeaderText = "Gloves";
                    _vm.IsHiddenHeadArmor = true;
                    _vm.IsHiddenLegArmor = true;
                    _vm.IsHiddenBodyArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideWeapon();
                    break;
                }
                case EquipmentIndex.Leg:
                {
                    _vm.HeaderText = "Boots";
                    _vm.IsHiddenBodyArmor = true;
                    _vm.IsHiddenArmArmor = true;
                    _vm.IsHiddenHeadArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideWeapon();
                    break;
                }
                case EquipmentIndex.Horse:
                {
                    _vm.HeaderText = "Mount";
                    _vm.IsHiddenBodyArmor = true;
                    HideArmor();
                    HideHorseHarness();
                    HideWeapon();
                    break;
                }
                case EquipmentIndex.HorseHarness:
                {
                    _vm.HeaderText = "Mount harness";
                    HideHorse();
                    HideArmor();
                    HideWeapon();
                    break;
                }
                case EquipmentIndex.Weapon0:
                {
                    _vm.HeaderText = "Weapon 1";
                    _vm.IsHiddenBodyArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideArmor();
                    break;
                }
                case EquipmentIndex.Weapon1:
                {
                    _vm.HeaderText = "Weapon 2";
                    _vm.IsHiddenBodyArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideArmor();
                    break;
                }
                case EquipmentIndex.Weapon2:
                {
                    _vm.HeaderText = "Weapon 3";
                    _vm.IsHiddenBodyArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideArmor();
                    break;
                }
                case EquipmentIndex.Weapon3:
                {
                    _vm.HeaderText = "Weapon 4";
                    _vm.IsHiddenBodyArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideArmor();
                    break;
                }
            }

            void HideHorse()
            {
                _vm.IsHiddenChargeDamage = true;
                _vm.IsHiddenHitPoints = true;
                _vm.IsHiddenManeuver = true;
                _vm.IsHiddenSpeed = true;
            }

            void HideHorseHarness()
            {
                _vm.IsHiddenChargeBonus = true;
                _vm.IsHiddenManeuverBonus = true;
                _vm.IsHiddenSpeedBonus = true;
            }

            void HideWeapon()
            {
                _vm.IsHiddenMaxData = true;
                _vm.IsHiddenThrustSpeed = true;
                _vm.IsHiddenSwingSpeed = true;
                _vm.IsHiddenMissileSpeed = true;
                _vm.IsHiddenWeaponLength = true;
                _vm.IsHiddenThrustDamage = true;
                _vm.IsHiddenSwingDamage = true;
                _vm.IsHiddenAccuracy = true;
                _vm.IsHiddenHandling = true;
                _vm.IsHiddenWeaponBodyArmor = true;
            }

            void HideArmor()
            {
                _vm.IsHiddenHeadArmor = true;
                _vm.IsHiddenLegArmor = true;
                _vm.IsHiddenArmArmor = true;
            }
            
            
        }

        protected override void OnFinalize()
        {
            _vm = null;
            base.OnFinalize();
        }
    }
}