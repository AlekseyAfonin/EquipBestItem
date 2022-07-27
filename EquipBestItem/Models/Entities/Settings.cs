namespace EquipBestItem.Models.Entities;

public class Settings : BaseEntity
{
    public const string IsLeftPanelLocked = "IsLeftPanelLocked";
    public const string IsRightPanelLocked = "IsRightPanelLocked";
    public const string IsLeftMenuVisible = "IsLeftMenuVisible";
    public const string IsRightMenuVisible = "IsRightMenuVisible";
    public bool Value { get; set; }
}