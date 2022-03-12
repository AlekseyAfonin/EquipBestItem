using EquipBestItem.Settings;
using TaleWorlds.GauntletUI;

namespace EquipBestItem.Widgets
{
    public class LockRightPanelWidget : ButtonWidget
    {
        public LockRightPanelWidget(UIContext context) : base(context)
        {
            IsRightPanelLocked = SettingsLoader.Instance.Settings.IsRightPanelLocked;
            IsSelected = IsRightPanelLocked;
        }

        public bool IsRightPanelLocked { get; set; }

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
        }
    }
}