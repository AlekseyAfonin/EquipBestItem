using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using System.Runtime.InteropServices;
using System;
using EquipBestItem.Behaviors;
using EquipBestItem.Settings;

namespace EquipBestItem
{
    public class SubModule : MBSubModuleBase
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            try
            {
                base.OnGameStart(game, gameStarterObject);
                
                SettingsLoader.Instance.LoadSettings();
                
                // Console enabled if in debug mode
                if (SettingsLoader.Instance.Settings.Debug)
                {
                    AllocConsole();
                }
                
                AddBehaviours(gameStarterObject as CampaignGameStarter);
            }
            catch (MBException e)
            {
                if (SettingsLoader.Instance.Settings.Debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message + e.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
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
            catch (MBException e)
            {
                if (SettingsLoader.Instance.Settings.Debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message + e.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                InformationManager.DisplayMessage(new InformationMessage("SubModule " + e.Message));
            }
        }
    }
}
