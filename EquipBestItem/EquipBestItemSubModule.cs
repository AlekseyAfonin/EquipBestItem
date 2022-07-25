using System;
using Bannerlord.UIExtenderEx;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.TwoDimension;

namespace EquipBestItem;

public class EquipBestItemSubModule : MBSubModuleBase
{
    private readonly UIExtender _uiExtender = new("EquipBestItem");
    private SpriteCategory _category = null!;

    protected override void OnSubModuleLoad()
    {
        base.OnSubModuleLoad();

        try
        {
            var spriteData = UIResourceManager.SpriteData;
            var resourceContext = UIResourceManager.ResourceContext;
            var resourceDepot = UIResourceManager.UIResourceDepot;

            _category = spriteData.SpriteCategories["ui_equipbestitem"];
            _category.Load(resourceContext, resourceDepot);

            _uiExtender.Register(typeof(EquipBestItemSubModule).Assembly);
            _uiExtender.Enable();
        }
        catch (Exception exception)
        {
            Helper.ShowMessage($"EquipBestItem failed to apply UIExtender patches {exception.Message})", Colors.Red);
        }
    }
}