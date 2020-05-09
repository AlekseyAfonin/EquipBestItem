using EquipBestItem.Behaviors;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace EquipBestItem
{

    public class SubModule : MBSubModuleBase
    {
        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            base.OnMissionBehaviourInitialize(mission);
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            try
            {
                base.OnGameStart(game, gameStarterObject);
                SettingsLoader.Instance.LoadSettings();
                SettingsLoader.Instance.LoadCharacterSettings();

                AddBehaviours(gameStarterObject as CampaignGameStarter);
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage("SubModule " + e.Message));
            }
        }



        private void AddBehaviours(CampaignGameStarter gameStarterObject)
        {
            try
            {
                if (gameStarterObject != null)
                {
                    gameStarterObject.AddBehavior(new InventoryBehavior());
                }
            }
            catch
            { 
                throw;
            }

        }


    }

}
