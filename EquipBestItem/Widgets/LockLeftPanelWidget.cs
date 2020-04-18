using EquipBestItem.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace EquipBestItem
{
    internal class LockLeftPanelWidget : ButtonWidget
    {
        public bool IsLeftPanelLocked { get; set; }

        public LockLeftPanelWidget(UIContext context) : base(context)
        {
            IsLeftPanelLocked = SettingsLoader.Instance.Settings.IsLeftPanelLocked;
            this.IsSelected = IsLeftPanelLocked;
        }

        protected override void OnClick()
        {
            base.OnClick();
            IsLeftPanelLocked = !IsLeftPanelLocked;
            this.IsSelected = IsLeftPanelLocked;
            SettingsLoader.Instance.Settings.IsLeftPanelLocked = IsLeftPanelLocked;
        }

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);
            //InformationManager.DisplayMessage(new InformationMessage("IsRightPanelLocked: " + this.IsRightPanelLocked.ToString()));
        }

    }
}
