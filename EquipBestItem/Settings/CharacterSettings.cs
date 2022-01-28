using System;

namespace EquipBestItem.Settings
{
    [Serializable]
    public class CharacterSettings
    {
        private Filters _filters;

        public CharacterSettings(string name)
        {
            Name = name;

            if (name == "default_equipbestitem" || name == "default_equipbestitem_civil")
            {
                //_filters = new Filters(true);
            }

            _filters = new Filters();
        }

        public string Name { get; set; }

        public Filters Filters
        {
            get => _filters;
            set => _filters = value;
        }
    }
}