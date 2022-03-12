using System;

namespace EquipBestItem.Settings
{
    [Serializable]
    public class Settings
    {
        public bool IsLeftPanelLocked { get; set; } = true;
        public bool IsRightPanelLocked { get; set; }
        public bool Debug { get; set; } = false;
    }
}