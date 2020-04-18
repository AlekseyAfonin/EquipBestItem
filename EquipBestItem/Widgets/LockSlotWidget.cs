using EquipBestItem.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.GauntletUI;

namespace EquipBestItem
{
    internal class LockSlotWidget : ButtonWidget
    {
        public static CharacterSettings CharacterSettings { get; set; }

        public LockSlotWidget(UIContext context) : base(context)
        {
            CharacterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(EquipBestItemViewModel.CurrentCharacterName);
            LockStateUpdate();
            this.IsEnabled = SettingsLoader.Instance.Settings.IsEnabledLocks;
        }


        private void LockStateUpdate()
        {
            if (this.Id == "HelmLock" && this.IsSelected != CharacterSettings.IsHelmLocked) this.IsSelected = CharacterSettings.IsHelmLocked;
            if (this.Id == "CloakLock" && this.IsSelected != CharacterSettings.IsCloakLocked) this.IsSelected = CharacterSettings.IsCloakLocked;
            if (this.Id == "ArmorLock" && this.IsSelected != CharacterSettings.IsArmorLocked) this.IsSelected = CharacterSettings.IsArmorLocked;
            if (this.Id == "GloveLock" && this.IsSelected != CharacterSettings.IsGloveLocked) this.IsSelected = CharacterSettings.IsGloveLocked;
            if (this.Id == "BootLock" && this.IsSelected != CharacterSettings.IsBootLocked) this.IsSelected = CharacterSettings.IsBootLocked;
            if (this.Id == "MountLock" && this.IsSelected != CharacterSettings.IsMountLocked) this.IsSelected = CharacterSettings.IsMountLocked;
            if (this.Id == "HarnessLock" && this.IsSelected != CharacterSettings.IsHarnessLocked) this.IsSelected = CharacterSettings.IsHarnessLocked;
            if (this.Id == "Weapon1Lock" && this.IsSelected != CharacterSettings.IsWeapon1Locked) this.IsSelected = CharacterSettings.IsWeapon1Locked;
            if (this.Id == "Weapon2Lock" && this.IsSelected != CharacterSettings.IsWeapon2Locked) this.IsSelected = CharacterSettings.IsWeapon2Locked;
            if (this.Id == "Weapon3Lock" && this.IsSelected != CharacterSettings.IsWeapon2Locked) this.IsSelected = CharacterSettings.IsWeapon3Locked;
            if (this.Id == "Weapon4Lock" && this.IsSelected != CharacterSettings.IsWeapon4Locked) this.IsSelected = CharacterSettings.IsWeapon4Locked;
        }

        protected override void OnClick()
        {
            base.OnClick();

            if (this.Id == "HelmLock")
            {
                CharacterSettings.IsHelmLocked = !CharacterSettings.IsHelmLocked;
                this.IsSelected = CharacterSettings.IsHelmLocked;
            }
            if (this.Id == "CloakLock")
            {
                CharacterSettings.IsCloakLocked = !CharacterSettings.IsCloakLocked;
                this.IsSelected = CharacterSettings.IsCloakLocked;
            }
            if (this.Id == "ArmorLock")
            {
                CharacterSettings.IsArmorLocked = !CharacterSettings.IsArmorLocked;
                this.IsSelected = CharacterSettings.IsArmorLocked;
            }
            if (this.Id == "GloveLock")
            {
                CharacterSettings.IsGloveLocked = !CharacterSettings.IsGloveLocked;
                this.IsSelected = CharacterSettings.IsGloveLocked;
            }
            if (this.Id == "BootLock")
            {
                CharacterSettings.IsBootLocked = !CharacterSettings.IsBootLocked;
                this.IsSelected = CharacterSettings.IsBootLocked;
            }
            if (this.Id == "MountLock")
            {
                CharacterSettings.IsMountLocked = !CharacterSettings.IsMountLocked;
                this.IsSelected = CharacterSettings.IsMountLocked;
            }
            if (this.Id == "HarnessLock")
            {
                CharacterSettings.IsHarnessLocked = !CharacterSettings.IsHarnessLocked;
                this.IsSelected = CharacterSettings.IsHarnessLocked;
            }
            if (this.Id == "Weapon1Lock")
            {
                CharacterSettings.IsWeapon1Locked = !CharacterSettings.IsWeapon1Locked;
                this.IsSelected = CharacterSettings.IsWeapon1Locked;
            }
            if (this.Id == "Weapon2Lock")
            {
                CharacterSettings.IsWeapon2Locked = !CharacterSettings.IsWeapon2Locked;
                this.IsSelected = CharacterSettings.IsWeapon2Locked;
            }
            if (this.Id == "Weapon3Lock")
            {
                CharacterSettings.IsWeapon3Locked = !CharacterSettings.IsWeapon3Locked;
                this.IsSelected = CharacterSettings.IsWeapon3Locked;
            }
            if (this.Id == "Weapon4Lock")
            {
                CharacterSettings.IsWeapon4Locked = !CharacterSettings.IsWeapon4Locked;
                this.IsSelected = CharacterSettings.IsWeapon4Locked;
            }
        }

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);
            if (EquipBestItemViewModel.CurrentCharacterName != CharacterSettings.Name)
            {
                CharacterSettings = SettingsLoader.Instance.GetCharacterSettingsByName(EquipBestItemViewModel.CurrentCharacterName);
            }
            LockStateUpdate();
            //InformationManager.DisplayMessage(new InformationMessage("IsRightPanelLocked: " + this.IsRightPanelLocked.ToString()));
        }
    }
}
