using System;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using Bannerlord.UIExtenderEx;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.TwoDimension;

namespace EquipBestItem
{
    public class SubModule : MBSubModuleBase
    {
        private readonly UIExtender _uiExtender = new("EquipBestItem");
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            try 
            {
                _uiExtender.Register(typeof(SubModule).Assembly);
                _uiExtender.Enable();
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