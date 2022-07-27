using System.Diagnostics.CodeAnalysis;
using Bannerlord.UIExtenderEx.Attributes;

namespace EquipBestItem.UIExtenderEx;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed partial class SPInventoryVMMixin
{
    [DataSourceMethod]
    public void ExecuteSwitchLeftMenu()
    {
        IsLeftMenuVisible = !IsLeftMenuVisible;
        _model.SwitchLeftMenu();
    }

    [DataSourceMethod]
    public void ExecuteSwitchRightMenu()
    {
        IsRightMenuVisible = !IsRightMenuVisible;
        _model.SwitchRightMenu();
    }

    [DataSourceMethod]
    public void ExecuteLeftPanelLock()
    {
        IsLeftPanelLocked = !IsLeftPanelLocked;
        _model.SwitchLeftPanelLock();
    }

    [DataSourceMethod]
    public void ExecuteRightPanelLock()
    {
        IsRightPanelLocked = !IsRightPanelLocked;
        _model.SwitchRightPanelLock();
    }

    [DataSourceMethod]
    public void ExecuteEquipCurrentCharacter()
    {
        _model.EquipCurrentCharacter();
    }

    [DataSourceMethod]
    public void ExecuteEquipAllCharacters()
    {
        _model.EquipAllCharacters();
    }

    [DataSourceMethod]
    public void ExecuteEquipBestItem(string equipmentIndexName)
    {
        _model.ExecuteEquipBestItem(equipmentIndexName);
    }

    [DataSourceMethod]
    public void ExecuteShowFilterSettings(string equipmentIndexName)
    {
        _model.ExecuteShowFilterSettings(equipmentIndexName);
    }

    [DataSourceMethod]
    public void ExecuteResetTranstactions()
    {
        ViewModel?.ExecuteResetTranstactions();
    }
}