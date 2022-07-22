using System;
using TaleWorlds.MountAndBlade;
using Bannerlord.UIExtenderEx;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.ModuleManager;
using TaleWorlds.TwoDimension;

namespace EquipBestItem
{
    public class EquipBestItemSubModule : MBSubModuleBase
    {
        //private static Harmony _harmony;
        //private static Assembly _assembly;
        private readonly UIExtender _uiExtender = new("EquipBestItem");
        private SpriteCategory _category;
        
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            try 
            {
                
                var spriteData = UIResourceManager.SpriteData;
                var resourceContext = UIResourceManager.ResourceContext;
                var resourceDepot = UIResourceManager.UIResourceDepot;

                _category = spriteData.SpriteCategories["ui_equipbestitem"]; // select which category to load, put your category name here
                _category.Load(resourceContext, resourceDepot); // load the selected category
                
                _uiExtender.Register(typeof(EquipBestItemSubModule).Assembly);
                _uiExtender.Enable();
                
                //_harmony = new Harmony("EquipBestItem");
                //_assembly = Assembly.GetExecutingAssembly();
                //_harmony.PatchAll(_assembly);
            }
            catch (Exception exception)
            {
                //MessageBox.Show($"EquipBestItem failed to apply UIExtender patches {exception.Message})");
            }
        }

        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            base.InitializeGameStarter(game, starterObject);
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
        }
    }
}