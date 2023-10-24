using System;
using RsGEN.Data;
using UnityEngine;

namespace RsGEN
{
    public class DataLoader
    {
        public static event EventHandler<DataLoadedEventArgs> DataLoaded;

        private CarDataCollection _carData;
        private TrackDataCollection _trackData;
        private RaceSettingsData _raceSettingsData;

        private readonly TextAsset _carDataJson;
        private readonly TextAsset _trackDataJson;
        private readonly TextAsset _raceSettingsDataJson;

        public DataLoader(TextAsset carDataJson, TextAsset trackDataJson, TextAsset raceSettingsDataJson)
        {
            _carDataJson = carDataJson;
            _trackDataJson = trackDataJson;
            _raceSettingsDataJson = raceSettingsDataJson;
        }

        ~DataLoader()
        {
            DataLoaded = null;
        }

        public void LoadCarData()
        {
            _carData = JsonUtility.FromJson<CarDataCollection>(_carDataJson.text);

            var args = new DataLoadedEventArgs
            {
                DataType = DataLoadedEventArgs.DataTypes.CarData,
                CarData = _carData
            };

            DataLoaded?.Invoke(this, args);
        }

        public void LoadTrackData()
        {
            _trackData = JsonUtility.FromJson<TrackDataCollection>(_trackDataJson.text);

            var args = new DataLoadedEventArgs
            {
                DataType = DataLoadedEventArgs.DataTypes.TrackData,
                TrackData = _trackData
            };

            DataLoaded?.Invoke(this, args);
        }

        public void LoadRaceSettings()
        {
            _raceSettingsData = JsonUtility.FromJson<RaceSettingsData>(_raceSettingsDataJson.text);

            var args = new DataLoadedEventArgs
            {
                DataType = DataLoadedEventArgs.DataTypes.RaceSettingsData,
                RaceSettingsData = _raceSettingsData
            };

            DataLoaded?.Invoke(this, args);
        }

        public class DataLoadedEventArgs : EventArgs
        {
            public enum DataTypes
            {
                CarData,
                TrackData,
                RaceSettingsData
            }

            public DataTypes DataType { get; set; }
            public CarDataCollection CarData { get; set; }
            public TrackDataCollection TrackData { get; set; }
            public RaceSettingsData RaceSettingsData { get; set; }
        }
    }
}