using EquipBestItem.Models;
using EquipBestItem.ViewModels;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Localization;

namespace EquipBestItem.Layers
{
    public class FiltersSettingsLayer : GauntletLayer
    {
        private FiltersSettingsVM _vm;
        public FiltersModel Model;
        
        public FiltersSettingsLayer(int localOrder, SPInventoryVM inventory, EquipmentIndex selectedSlot, FiltersModel model, string categoryId = "GauntletLayer") : base(localOrder, categoryId)
        {
            Model = model;
            _vm = new FiltersSettingsVM(inventory, selectedSlot, this);
            LoadMovie("EBI_FiltersSettings", _vm);
            
            switch (selectedSlot)
            {
                case EquipmentIndex.Head:
                {
                    _vm.HeaderText = new TextObject("{=O3dhjtOS}Head Armor").ToString();
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
                    _vm.HeaderText = new TextObject("{=k8QpbFnj}Cape").ToString();
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
                    _vm.HeaderText = new TextObject("{=HkfY3Ds5}Body Armor").ToString();
                    HideHorse();
                    HideHorseHarness();
                    HideWeapon();
                    break;
                }
                case EquipmentIndex.Gloves:
                {
                    _vm.HeaderText = new TextObject("{=kx7q8ybD}Arm Armor").ToString();
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
                    _vm.HeaderText = new TextObject("{=11aiaODt}Foot Armor").ToString();
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
                    _vm.HeaderText = new TextObject("{=mountnoun}Mount").ToString();
                    _vm.IsHiddenBodyArmor = true;
                    _vm.IsHiddenWeight = true;
                    HideArmor();
                    HideHorseHarness();
                    HideWeapon();
                    break;
                }
                case EquipmentIndex.HorseHarness:
                {
                    _vm.HeaderText = new TextObject("{=b5t34yLX}Horse Harness").ToString();
                    _vm.BodyArmorText = new TextObject("{=305cf7f98458b22e9af72b60a131714f}Horse Armor: ").ToString();
                    HideHorseHarness();  //Not implemented in game function (maybe the developers will add this in the future)
                    HideHorse();
                    HideArmor();
                    HideWeapon();
                    break;
                }
                case EquipmentIndex.Weapon0:
                {
                    _vm.HeaderText = new TextObject("{=2RIyK1bp}Weapons") + " 1";
                    _vm.IsHiddenBodyArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideArmor();
                    break;
                }
                case EquipmentIndex.Weapon1:
                {
                    _vm.HeaderText = new TextObject("{=2RIyK1bp}Weapons") + " 2";
                    _vm.IsHiddenBodyArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideArmor();
                    break;
                }
                case EquipmentIndex.Weapon2:
                {
                    _vm.HeaderText = new TextObject("{=2RIyK1bp}Weapons") + " 3";
                    _vm.IsHiddenBodyArmor = true;
                    HideHorse();
                    HideHorseHarness();
                    HideArmor();
                    break;
                }
                case EquipmentIndex.Weapon3:
                {
                    _vm.HeaderText = new TextObject("{=2RIyK1bp}Weapons") + " 4";
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
                _vm.IsHiddenMissileDamage = true;
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