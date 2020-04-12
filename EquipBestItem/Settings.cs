using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TaleWorlds.Library;

namespace EquipBestItem
{
    public class Settings
    {
        private static Settings _instance;

        private EquipBestItemSettings _equipBestItemSettings;

        public static EquipBestItemSettings EBISettings
        {
            get
            {
                return Instance._equipBestItemSettings;
            }
            set
            {
                Instance._equipBestItemSettings = value;
            }
        }

        private Settings()
        {
            Load();
        }

        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }

        void Load()
        {
            string path = Path.Combine(BasePath.Name, "Modules", SubModule.ModuleFolderName, "ModuleData", "settings.xml");

            _equipBestItemSettings = Helper.Deserialize<EquipBestItemSettings>(path);
        }

        public void Save()
        {
            EquipBestItemSettings settingsObj = Settings.Instance._equipBestItemSettings;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            XmlSerializer serializer = new XmlSerializer(typeof(EquipBestItemSettings));
            string path = Path.Combine(BasePath.Name, "Modules", SubModule.ModuleFolderName, "ModuleData", "settings.xml");
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, settingsObj, ns);
            }
        }
    }

    [Serializable]
    public class EquipBestItemSettings
    {
        private bool _isEnabledEquipAllButton = false;

        public bool IsEnabledEquipAllButton
        {
            get
            {
                return _isEnabledEquipAllButton;
            }
            set
            {
                _isEnabledEquipAllButton = value;
            }
        }

        private bool _isEnabledStandartButtons = true;

        public bool IsEnabledStandartButtons
        {
            get
            {
                return _isEnabledStandartButtons;
            }
            set
            {
                _isEnabledStandartButtons = value;
            }
        }

        public EquipBestItemSettings()
        {
        }
    }

    
}
