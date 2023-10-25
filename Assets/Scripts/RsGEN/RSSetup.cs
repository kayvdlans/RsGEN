using System;
using System.Collections.Generic;

namespace RsGEN
{
    public class RSSetup
    {
        public static event EventHandler<SetupEventArgs> SetupCompleted;

        [Flags]
        public enum GenType
        {
            Laps,
            Endurance,
            RacePresets
        }

        private GenType _genType;
        private readonly List<RSPreset> _racePresets = new();

        public RSSetup()
        {
            DataLoader.DataLoaded += OnDataLoaded;
        }

        ~RSSetup()
        {
            DataLoader.DataLoaded -= OnDataLoaded;
            SetupCompleted = null;
        }

        private void OnDataLoaded(object sender, DataLoader.DataLoadedEventArgs e)
        {
            // both the setup json file and the race settings data should be present before being able to start.
            if (sender.GetType() != typeof(DataLoader)) return;
            if (e.DataType != DataLoader.DataLoadedEventArgs.DataTypes.RaceSettingsData) return;

            //probably wanna load defaults from file, if it exists. If not, initialize with default values
            InitializeDefaultGenType();
            InitializeDefaultPresets();

            SaveSetup();
        }

        private void InitializeDefaultGenType()
        {
            _genType = GenType.RacePresets;
        }

        private void InitializeDefaultPresets()
        {
            foreach (var preset in RSPreset.DefaultPresets) _racePresets.Add(preset);
        }

        private void SaveSetup()
        {
            var args = new SetupEventArgs
            {
                GenType = _genType,
                RacePresets = _racePresets
            };

            //wanna save it to file in the future as well, might want to put it in a separate class.
            SetupCompleted?.Invoke(this, args);
        }

        public class SetupEventArgs : EventArgs
        {
            public GenType GenType { get; private set; }
            public List<RSPreset> RacePresets { get; private set; }
        }
    }
}
