using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace EquipBestItem.Settings
{
    public class SettingsLoader
    {
        private string _filePathSettings = Path.Combine(BasePath.Name, "Modules", "EquipBestItem", "ModuleData", "Settings.xml");
        private string _filePathCharacterSettings = Path.Combine(BasePath.Name, "Modules", "EquipBestItem", "ModuleData", "CharacterSettings.xml");

        private static SettingsLoader _instance = null;

        public Settings Settings { get; private set; }

        public List<CharacterSettings> CharacterSettings { get; private set; }

        public static bool Debug = false;

        private SettingsLoader()
        {
        }

        public static SettingsLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SettingsLoader();
                }
                return _instance;
            }
        }

        public void LoadSettings()
        { 
            try
            {
                Settings = Helper.Deserialize<Settings>(_filePathSettings);

                if (SettingsLoader.Debug)
                    InformationManager.DisplayMessage(new InformationMessage("LoadSettings()"));
            }
            catch(Exception e)
            {
                if (SettingsLoader.Debug)
                    MessageBox.Show("Cant load Settings.xml. " + e.Message + e.StackTrace);
                InformationManager.DisplayMessage(new InformationMessage("I can't find Settings.xml, create a new..."));
                Settings = new Settings();
                SaveSettings();
            }
        }

        public void SaveSettings()
        {
            try
            {
                Helper.Serialize<Settings>(_filePathSettings, Settings);

                if (SettingsLoader.Debug)
                    InformationManager.DisplayMessage(new InformationMessage("SaveSettings()"));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
            }
        }

        public void LoadCharacterSettings()
        {
            try
            {
                CharacterSettings = Helper.Deserialize<List<CharacterSettings>>(_filePathCharacterSettings);
            }
            catch (Exception e)
            {
                if (SettingsLoader.Debug)
                    MessageBox.Show("Cant load CharacterSettings.xml. " + e.Message);
                InformationManager.DisplayMessage(new InformationMessage("I can't find CharacterSettings.xml, create a new..."));
                CharacterSettings = new List<CharacterSettings>();
                SaveCharacterSettings();
            }
        }

        public void SaveCharacterSettings()
        {
            try
            {
                Helper.Serialize<List<CharacterSettings>>(_filePathCharacterSettings, CharacterSettings);

                if (SettingsLoader.Debug)
                    InformationManager.DisplayMessage(new InformationMessage("SaveCharacterSettings()"));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
            }
        }

        public CharacterSettings GetCharacterSettingsByName(string name)
        {
            foreach (CharacterSettings charSettings in CharacterSettings)
            {
                if (charSettings.Name == name)
                    return charSettings;
            }

            CharacterSettings characterSettings;
            characterSettings = new CharacterSettings(name);
            CharacterSettings.Add(characterSettings);

            return characterSettings;
        }
    }
}
