using System.Collections.Generic;
using RsGEN.Data;
using RsGEN.Utils;
using UnityEngine;

namespace RsGEN
{
    public class RaceGen
    {
        private List<CarData> _carSelection;
        private List<TrackData> _trackSelection;

        private CarData _currentCar;
        private TrackData _currentTrack;
        private TrackVariant _currentTrackVariant;

        public RaceGen()
        {
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
            CarListController.CarListSaved -= OnCarListSaved;
        }

        private void OnCarListSaved(object sender, CarListController.CarListEventArgs e)
        {
            if (sender.GetType() != typeof(CarListController)) return;

            Debug.Log(e.CarList);
            _carSelection = e.CarList;

            //temporary until generate UI is implemented
            GenerateRace();
        }

        private void GenerateRace()
        {
            _currentCar = Helper.GetRandomValueFromCollection(_carSelection);
            _currentTrack = Helper.GetRandomValueFromCollection(_trackSelection);
            _currentTrackVariant = Helper.GetRandomValueFromCollection(_currentTrack.variants);

            Debug.Log(_currentCar.name + " on " + _currentTrack.name + ", " + _currentTrackVariant.name);
        }
    }
}