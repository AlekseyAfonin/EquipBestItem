using SandBox.GauntletUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using System.Xml.Serialization;

namespace EquipBestItem
{

    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleFolderName = "EquipBestItem";


        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
        }



        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
        }



        protected override void OnApplicationTick(float dt)
        {
        }



    }

}
