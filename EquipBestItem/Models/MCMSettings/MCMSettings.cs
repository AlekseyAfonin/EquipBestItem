using System.Collections.Generic;
using System.Linq;
using EquipBestItem.Models.BestItemCalculator;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Dropdown;
using MCM.Abstractions.Settings.Base.Global;

namespace EquipBestItem.Models.MCMSettings;

internal class MCMSettings : AttributeGlobalSettings<MCMSettings>
{
    public override string Id => "EquipBestItemSettings";
    public override string DisplayName => "Equip Best Item";
    public override string FolderName => "EquipBestItem";
    public override string FormatType => "json";

    private static readonly IEnumerable<BestItemCalculatorBase> Calculators = Helper.GetEnumerableOfType<BestItemCalculatorBase>(new object[1]);
    
    [SettingPropertyDropdown("Search method", RequireRestart = false)]
    public DropdownDefault<string> SearchMethod { get; set; } =
        new(Calculators.Select(c => c.Name), selectedIndex: 0);

    [SettingPropertyDropdown("Enable culture", RequireRestart = false)]
    public bool IsCultureEnabled { get; set; } = true;
}