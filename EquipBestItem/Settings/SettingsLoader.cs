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

        private SettingsLoader()
        {
            LoadSettings();
            LoadCharacterSettings();
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

        private void LoadSettings()
        { 
            try
            {
                Settings = Helper.Deserialize<Settings>(_filePathSettings);
            }
            catch(Exception e)
            {
                //MessageBox.Show("Не удалось открыть файл Settings.xml. " + e.Message);
                Settings = new Settings();
                SaveSettings();
            }
        }

        public void SaveSettings()
        {
            Helper.Serialize<Settings>(_filePathSettings, Settings);
        }

        private void LoadCharacterSettings()
        {
            try
            {
                CharacterSettings = Helper.Deserialize<List<CharacterSettings>>(_filePathCharacterSettings);
            }
            catch (Exception e)
            {
                //MessageBox.Show("Не удалось открыть файл CharacterSettings.xml. " + e.Message);
                CharacterSettings = new List<CharacterSettings>();
                SaveCharacterSettings();
            }
        }

        public void SaveCharacterSettings()
        {
            Helper.Serialize<List<CharacterSettings>>(_filePathCharacterSettings, CharacterSettings);
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
