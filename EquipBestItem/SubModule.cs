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
using System.Windows.Forms;
using EquipBestItem.Behaviors;

namespace EquipBestItem
{

    public class SubModule : MBSubModuleBase
    {
		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			try
			{
				base.OnGameStart(game, gameStarterObject);
				AddBehaviours(gameStarterObject as CampaignGameStarter);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
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
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
			
		}
	}

}
