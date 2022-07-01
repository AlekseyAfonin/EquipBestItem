using System;
using System.Reflection;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using Bannerlord.UIExtenderEx;
using HarmonyLib;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.TwoDimension;

namespace EquipBestItem
{
    public class SubModule : MBSubModuleBase
    {
        //private static Harmony _harmony;
        //private static Assembly _assembly;
        private readonly UIExtender _uiExtender = new("EquipBestItem");
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            try 
            {
                _uiExtender.Register(typeof(SubModule).Assembly);
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