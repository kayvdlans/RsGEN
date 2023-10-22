using System;
using System.Collections.Generic;
using RsGEN.Data;

namespace RsGEN
{
    public class CarDataProcessor
    {
        public static event EventHandler DataProcessed;

        public CarDataProcessor()
        {
            DataLoader.DataLoaded += OnDataLoaded;
        }

        ~CarDataProcessor()
        {
            DataLoader.DataLoaded -= OnDataLoaded;
            DataProcessed = null;
        }

        private void OnDataLoaded(object sender, DataLoader.DataLoadedEventArgs args)
        {
            if (args.DataType != DataLoader.DataLoadedEventArgs.DataTypes.CarData) return;

            foreach (var car in args.CarData.cars)
            {
                UpdateDictionary(CarsByCountry, car, car.country);
                UpdateDictionary(CarsByManufacturer, car, car.manufacturer);
                UpdateDictionary(CarsByCategory, car, car.category);
                UpdateDictionary(CarsByDrivetrain, car, car.drivetrain);
                UpdateDictionary(CarsByAspiration, car, car.aspiration);

                foreach (var tag in car.tags) UpdateDictionary(CarsByTag, car, tag);
            }

            DataProcessed?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateDictionary<T>(Dictionary<T, List<CarData>> dictionary, CarData car, T key)
        {
            if (!dictionary.ContainsKey(key)) dictionary.Add(key, new List<CarData>());

            dictionary[key].Add(car);
        }

        public Dictionary<string, List<CarData>> CarsByCountry { get; } = new();

        public Dictionary<string, List<CarData>> CarsByManufacturer { get; } = new();

        public Dictionary<string, List<CarData>> CarsByCategory { get; } = new();

        public Dictionary<string, List<CarData>> CarsByDrivetrain { get; } = new();

        public Dictionary<string, List<CarData>> CarsByAspiration { get; } = new();

        public Dictionary<string, List<CarData>> CarsByTag { get; } = new();
    }
}