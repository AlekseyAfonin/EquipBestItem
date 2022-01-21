using EquipBestItem.Settings;
using TaleWorlds.GauntletUI;

namespace EquipBestItem.Widgets
{
    public class LockRightPanelWidget : ButtonWidget
    {
        public bool IsRightPanelLocked { get; set; }

        public LockRightPanelWidget(UIContext context) : base(context)
        {
            IsRightPanelLocked = SettingsLoader.Instance.Settings.IsRightPanelLocked;
            IsSelected = IsRightPanelLocked;
        }

        protected override void OnClick()
        {
            base.OnClick();
            IsRightPanelLocked = !IsRightPanelLocked;
            IsSelected = IsRightPanelLocked;
            SettingsLoader.Instance.Settings.IsRightPanelLocked = IsRightPanelLocked;
        }

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);
            //InformationManager.DisplayMessage(new InformationMessage("IsRightPanelLocked: " + this.IsRightPanelLocked.ToString()));
        }

    }
}
