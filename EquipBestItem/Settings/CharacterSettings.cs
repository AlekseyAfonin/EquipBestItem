using System;

namespace EquipBestItem.Settings
{
    [Serializable]
    public class CharacterSettings
    {
        public string Name { get; set; }

        private Filters _filters;
        public Filters Filters
        {
            get => _filters;
            set => _filters = value;
        }

        public CharacterSettings(string name)
        {
            Name = name;

            if (name == "default_equipbestitem" || name == "default_equipbestitem_civil")
            {
                _filters = new Filters(true);
            }
            else
            {
                _filters = new Filters();
            }
        }

        public CharacterSettings()
        {
            Console.WriteLine("CharacterSettings() Constructor call");
        }
    }
}
