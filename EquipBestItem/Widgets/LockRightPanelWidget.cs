using TaleWorlds.GauntletUI;

namespace EquipBestItem
{
    public class LockRightPanelWidget : ButtonWidget
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
