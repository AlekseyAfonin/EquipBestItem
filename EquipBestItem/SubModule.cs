using EquipBestItem.Settings;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace EquipBestItem
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            try
            {
                base.OnGameStart(game, gameStarterObject);

                SettingsLoader.Instance.LoadSettings();

                EquipBestItemManager.Instance.SubscribeEvents();
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage("EquipBestItem error " + e.Message));
            }
        }

        public override void OnGameEnd(Game game)
        {
            EquipBestItemManager.Instance.UnsubscribeEvents();
            base.OnGameEnd(game);
        }
    }
}