using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace EquipBestItem.Settings
{
    public class SettingsLoader
    {
        private static SettingsLoader _instance;

        //New settings folder "Documents\Mount and Blade II Bannerlord\Configs\ModSettings\EquipBestItem\"
        private readonly PlatformFilePath _newCharacterSettingsFile =
            new(new PlatformDirectoryPath(EngineFilePaths.ConfigsPath.Type,
                EngineFilePaths.ConfigsPath.Path + "/ModSettings/EquipBestItem/"), "CharacterSettings.xml");

        private readonly PlatformFilePath _oldCharacterSettingsFile =
            new(EngineFilePaths.ConfigsPath, "CharacterSettings.xml");

        private readonly PlatformFilePath _settingsFile =
            new(new PlatformDirectoryPath(EngineFilePaths.ConfigsPath.Type,
                EngineFilePaths.ConfigsPath.Path + "/ModSettings/EquipBestItem/"), "Settings.xml");

        public Settings Settings { get; private set; }

        private List<CharacterSettings> CharacterSettings { get; set; }

        public static SettingsLoader Instance
        {
            get { return _instance ??= new SettingsLoader(); }
        }

        public void LoadSettings()
        {
            try
            {
                Settings = FileHelper.FileExists(_settingsFile)
                    ? Helper.Deserialize<Settings>(_settingsFile)
                    : new Settings();
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message + ". Creating a new..."));
            }
        }

        public void SaveSettings()
        {
            try
            {
                Helper.Serialize(_settingsFile, Settings);
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
        }

        public void LoadCharacterSettings()
        {
            try
            {
                if (FileHelper.FileExists(_newCharacterSettingsFile))
                {
                    CharacterSettings = Helper.Deserialize<List<CharacterSettings>>(_newCharacterSettingsFile);
                }
                else
                {
                    if (FileHelper.FileExists(_oldCharacterSettingsFile))
                        CharacterSettings = Helper.Deserialize<List<CharacterSettings>>(_oldCharacterSettingsFile);
                }
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
            finally
            {
                CharacterSettings ??= new List<CharacterSettings>
                {
                    new("default_equipbestitem"),
                    new("default_equipbestitem_civil")
                };
            }
        }

        public void SaveCharacterSettings()
        {
            try
            {
                Helper.Serialize(_newCharacterSettingsFile, CharacterSettings);
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
        }

        public CharacterSettings GetCharacterSettingsByName(string name, bool isInWarSet)
        {
            CharacterSettings charSettings = null;

            try
            {
                if (name == null) throw new MBException("CharacterSettings exception. Name = null.");
                if (!isInWarSet)
                    name = name + "_civil";

                foreach (var charSet in CharacterSettings)
                {
                    if (charSet.Name != name) continue;
                    charSettings = charSet;
                    return charSettings;
                }

                charSettings = new CharacterSettings(name);

                for (var index = EquipmentIndex.WeaponItemBeginSlot;
                     index < EquipmentIndex.NumEquipmentSetSlots;
                     index++)
                    charSettings.Filters.SetDefault(index, isInWarSet);

                CharacterSettings.Add(charSettings);
            }
            catch (MBException e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }

            return charSettings;
        }
    }
}