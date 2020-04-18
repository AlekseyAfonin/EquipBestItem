using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipBestItem.Settings
{
    [Serializable]
    public class Settings
    {
        public bool IsEnabledEquipAllButton { get; set; } = true;

        public bool IsEnabledStandardButtons { get; set; } = true;

        public bool IsEnabledLocks { get; set; } = true;

        public bool IsLeftPanelLocked { get; set; }

        public bool IsRightPanelLocked { get; set; }

    }
}
