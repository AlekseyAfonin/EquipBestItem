using System;
using Bannerlord.UIExtenderEx;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace EquipBestItem;

public class EquipBestItemSubModule : MBSubModuleBase
{
    private readonly UIExtender _uiExtender = new("EquipBestItem");

    protected override void OnSubModuleLoad()
    {
        base.OnSubModuleLoad();

        try
        {
            _uiExtender.Register(typeof(EquipBestItemSubModule).Assembly);
            _uiExtender.Enable();
        }
        catch (Exception exception)
        {
            Helper.ShowMessage($"EquipBestItem failed to apply UIExtender patches {exception.Message})", Colors.Red);
        }
    }
}