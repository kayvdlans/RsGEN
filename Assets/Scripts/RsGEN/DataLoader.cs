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

        public DataLoader(TextAsset carDataJson, TextAsset trackDataJson, TextAsset raceSettingsDataJson)
        {
            LoadCarData(carDataJson);
            LoadTrackData(trackDataJson);
            LoadRaceSettings(raceSettingsDataJson);

            Debug.Log("Data loaded");
        }

        ~DataLoader()
        {
            DataLoaded = null;
        }

        private void LoadCarData(TextAsset json)
        {
            _carData = JsonUtility.FromJson<CarDataCollection>(json.text);

            var args = new DataLoadedEventArgs
            {
                DataType = DataLoadedEventArgs.DataTypes.CarData,
                CarData = _carData
            };

            DataLoaded?.Invoke(this, args);
        }

        private void LoadTrackData(TextAsset json)
        {
            _trackData = JsonUtility.FromJson<TrackDataCollection>(json.text);

            var args = new DataLoadedEventArgs
            {
                DataType = DataLoadedEventArgs.DataTypes.TrackData,
                TrackData = _trackData
            };

            DataLoaded?.Invoke(this, args);
        }

        private void LoadRaceSettings(TextAsset json)
        {
            _raceSettingsData = JsonUtility.FromJson<RaceSettingsData>(json.text);

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