using EquipBestItem.Settings;
using TaleWorlds.GauntletUI;

namespace EquipBestItem.Widgets
{
    public class LockLeftPanelWidget : ButtonWidget
    {
        public bool IsLeftPanelLocked { get; set; }

        public LockLeftPanelWidget(UIContext context) : base(context)
        {
            IsLeftPanelLocked = SettingsLoader.Instance.Settings.IsLeftPanelLocked;
            IsSelected = IsLeftPanelLocked;
        }

        protected override void OnClick()
        {
            base.OnClick();
            IsLeftPanelLocked = !IsLeftPanelLocked;
            IsSelected = IsLeftPanelLocked;
            SettingsLoader.Instance.Settings.IsLeftPanelLocked = IsLeftPanelLocked;
        }

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);
            //InformationManager.DisplayMessage(new InformationMessage("IsRightPanelLocked: " + this.IsRightPanelLocked.ToString()));
        }

    }
}
