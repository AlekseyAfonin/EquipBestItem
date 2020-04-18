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
    internal class LockRightPanelWidget : ButtonWidget
    {
        public bool IsRightPanelLocked { get; set; }

        public LockRightPanelWidget(UIContext context) : base(context)
        {
            IsRightPanelLocked = SettingsLoader.Instance.Settings.IsRightPanelLocked;
            this.IsSelected = IsRightPanelLocked;
        }

        protected override void OnClick()
        {
            base.OnClick();
            IsRightPanelLocked = !IsRightPanelLocked;
            this.IsSelected = IsRightPanelLocked;
            SettingsLoader.Instance.Settings.IsRightPanelLocked = IsRightPanelLocked;
        }

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);
            //InformationManager.DisplayMessage(new InformationMessage("IsRightPanelLocked: " + this.IsRightPanelLocked.ToString()));
        }

    }
}
