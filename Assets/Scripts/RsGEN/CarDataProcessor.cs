using System;
using System.Collections.Generic;
using RsGEN.Data;

namespace RsGEN
{
    public class CarDataProcessor
    {
        public static event EventHandler<DataProcessedEventArgs> DataProcessed;

        private readonly Dictionary<string, List<CarData>> _carsByCountry = new();
        private readonly Dictionary<string, List<CarData>> _carsByManufacturer = new();
        private readonly Dictionary<string, List<CarData>> _carsByCategory = new();
        private readonly Dictionary<string, List<CarData>> _carsByDrivetrain = new();
        private readonly Dictionary<string, List<CarData>> _carsByAspiration = new();
        private readonly Dictionary<string, List<CarData>> _carsByTag = new();

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
                UpdateDictionary(_carsByCountry, car, car.country);
                UpdateDictionary(_carsByManufacturer, car, car.manufacturer);
                UpdateDictionary(_carsByCategory, car, car.category);
                UpdateDictionary(_carsByDrivetrain, car, car.drivetrain);
                UpdateDictionary(_carsByAspiration, car, car.aspiration);

                foreach (var tag in car.tags) UpdateDictionary(CarsByTag, car, tag);
            }

            DataProcessedEventArgs args = new DataProcessedEventArgs() 
            {
                CarsByCountry = _carsByCountry,
                CarsByManufacturer = _carsByManufacturer,
                CarsByCategory = _carsByCategory,
                CarsByDrivertrain = _carsByDrivetrain,
                CarsByAspiration = _carsByAspiration
            };
            
            DataProcessed?.Invoke(this, args);
        }

        private void UpdateDictionary<T>(Dictionary<T, List<CarData>> dictionary, CarData car, T key)
        {
            if (!dictionary.ContainsKey(key)) dictionary.Add(key, new List<CarData>());

            dictionary[key].Add(car);
        }

        public class DataProcessedEventArgs : EventArgs 
        {
            public Dictioniary<string, List<CarData>> CarsByCountry { get; private set; }
            public Dictioniary<string, List<CarData>> CarsByManufacturer { get; private set; }
            public Dictioniary<string, List<CarData>> CarsByCategory { get; private set; }
            public Dictioniary<string, List<CarData>> CarsByDrivetrain { get; private set; }
            public Dictioniary<string, List<CarData>> CarsByAspiration { get; private set; }
        }
    }
}
