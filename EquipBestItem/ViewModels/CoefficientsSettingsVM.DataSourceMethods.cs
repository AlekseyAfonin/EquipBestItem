using System.Threading.Tasks;
using Bannerlord.UIExtenderEx.Attributes;
using EquipBestItem.Models;

namespace EquipBestItem.ViewModels;

internal partial class CoefficientsSettingsVM
{
    [DataSourceMethod]
    public void ExecuteDefault()
    {
        Task.Run(() => _model.DefaultClick());
    }
    
    [DataSourceMethod]
    public void ExecuteLock()
    {
        Task.Run(() => _model.LockClick());
    }
    
    [DataSourceMethod]
    public void ExecuteClose()
    {
        CoefficientsSettings.CloseClick();
    }

    [DataSourceMethod]
    public void ExecuteCheckboxSetDefault(string paramName)
    {
        _model.CheckboxClick(paramName);
    }
}