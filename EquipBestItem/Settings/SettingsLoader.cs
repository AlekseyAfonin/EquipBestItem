using System;
using System.Collections.Generic;
using EquipBestItem.Behaviors;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace EquipBestItem.Settings
{
    public class SettingsLoader
    {
        //New settings folder "Documents\Mount and Blade II Bannerlord\Configs\"
        private PlatformFilePath _settingsFile = 
            new PlatformFilePath(EngineFilePaths.ConfigsPath, "Settings.xml");

        private PlatformFilePath _characterSettingsFile =
            new PlatformFilePath(EngineFilePaths.ConfigsPath, "CharacterSettings.xml");

        private static SettingsLoader _instance;

        public Settings Settings { get; private set; }

        private List<CharacterSettings> CharacterSettings { get; set; }

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
                if (FileHelper.FileExists(_settingsFile))
                {
                    Settings = Helper.Deserialize<Settings>(_settingsFile);
                }
                else
                {
                    Settings = new Settings();
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
                if (Instance.Settings.Debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message + e.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
        }

        public void LoadCharacterSettings()
        {
            try
            {
                if (FileHelper.FileExists(_characterSettingsFile))
                {
                    CharacterSettings = Helper.Deserialize<List<CharacterSettings>>(_characterSettingsFile);
                }
            }
            catch (MBException e)
            {
                if (Instance.Settings.Debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message + e.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }
            finally
            {
                if (CharacterSettings == null)
                {
                    CharacterSettings = new List<CharacterSettings>();
                    CharacterSettings.Add(new CharacterSettings("default_equipbestitem"));
                    CharacterSettings.Add(new CharacterSettings("default_equipbestitem_civil"));
                }
            }
        }

        public void SaveCharacterSettings()
        {
            try
            {
                Helper.Serialize(_characterSettingsFile, CharacterSettings);
            }
            catch (MBException e)
            {
                if (Instance.Settings.Debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message + e.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
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

                for (var index = EquipmentIndex.WeaponItemBeginSlot; index < EquipmentIndex.NumEquipmentSetSlots; index++)
                    charSettings.Filters.SetDefault(index, isInWarSet);

                CharacterSettings.Add(charSettings);
            }
            catch (MBException e)
            {
                if (Instance.Settings.Debug)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message + e.StackTrace);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                InformationManager.DisplayMessage(new InformationMessage(e.Message));
            }

            return charSettings;
        }
    }
}

