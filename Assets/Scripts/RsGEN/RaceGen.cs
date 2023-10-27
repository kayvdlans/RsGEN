using System.Collections.Generic;
using RsGEN.Data;
using RsGEN.Utils;
using UnityEngine;

namespace RsGEN
{
    public class RaceGen
    {
        private List<RSPreset> _racePresets;
        private List<CarData> _carSelection;
        private List<TrackData> _trackSelection;

        private RSPreset _currentPreset;
        private CarData _currentCar;
        private TrackData _currentTrack;
        private TrackVariant _currentTrackVariant;

        public RaceGen()
        {
            RSSetup.SetupCompleted += OnSetupCompleted;
            CarListController.CarListSaved += OnCarListSaved;

            //temporary until proper selection UI is implemented
            DataLoader.DataLoaded += (sender, args) =>
            {
                if (args.DataType != DataLoader.DataLoadedEventArgs.DataTypes.TrackData) return;

                _trackSelection = new List<TrackData>(args.TrackData.tracks);
            };
        }

        ~RaceGen()
        {
            RSSetup.SetupCompleted -= OnSetupCompleted;
            CarListController.CarListSaved -= OnCarListSaved;
        }

        private void OnSetupCompleted(object sender, RSSetup.SetupEventArgs e)
        {
            if (sender.GetType() != typeof(RSSetup)) return;

            _racePresets = e.RacePresets;
        }

        private void OnCarListSaved(object sender, List<CarData> e)
        {
            if (sender.GetType() != typeof(CarListController)) return;

            _carSelection = e;

            //temporary until generate UI is implemented
            GenerateRace();
        }

        private void GenerateRace()
        {
            _currentPreset = Helper.GetRandomValueFromCollection(_racePresets);
            _currentCar = Helper.GetRandomValueFromCollection(_carSelection);
            _currentTrack = Helper.GetRandomValueFromCollection(_trackSelection);
            _currentTrackVariant = Helper.GetRandomValueFromCollection(_currentTrack.variants);

            Debug.Log(_currentPreset.Name + ": " + _currentCar.name + " on " + _currentTrack.name + ", " +
                      _currentTrackVariant.name);
        }
    }
}