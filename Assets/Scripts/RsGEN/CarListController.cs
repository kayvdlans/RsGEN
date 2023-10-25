using System;
using System.Collections.Generic;
using System.Linq;
using RsGEN.Data;
using RsGEN.UI;

namespace RsGEN
{
    public class CarListController
    {
        public static event EventHandler<CarListEventArgs> CarListRefreshed;
        public static event EventHandler<CarListEventArgs> CarListSaved;

        public const string COUNTRY = "Country";
        public const string MANUFACTURER = "Manufacturer";
        public const string CATEGORY = "Category";
        public const string DRIVETRAIN = "Drivetrain";
        public const string ASPIRATION = "Aspiration";
        public const string TAGS = "Tags";

        private CarDataProcessor _dataProcessor;
        private List<CarData> _completeCarList;
        private List<CarData> _currentCarList;

        public CarListController()
        {
            DataLoader.DataLoaded += OnDataLoaded;
            CarDataProcessor.DataProcessed += OnDataProcessed;
        }

        ~CarListController()
        {
            DataLoader.DataLoaded -= OnDataLoaded;
            CarDataProcessor.DataProcessed += OnDataProcessed;
            CarListRefreshed = null;
            CarListSaved = null;
        }

        private void OnDataLoaded(object sender, DataLoader.DataLoadedEventArgs e)
        {
            if (sender.GetType() != typeof(DataLoader)) return;
            if (e.DataType != DataLoader.DataLoadedEventArgs.DataTypes.CarData) return;

            _completeCarList = new List<CarData>(e.CarData.cars);
            _currentCarList = _completeCarList;

            var args = new CarListEventArgs
            {
                CarList = _currentCarList
            };

            CarListRefreshed?.Invoke(this, args);
        }

        private void OnDataProcessed(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(CarDataProcessor)) return;

            _dataProcessor = (CarDataProcessor)sender;

            FilterOptionsUI.OnRequestRefresh += OnRequestRefresh;
            CarListUI.OnSave += OnSave;
        }

        private void OnRequestRefresh(object sender, FilterOptionsUI.RequestRefreshEventArgs e)
        {
            if (sender.GetType() != typeof(FilterOptionsUI)) return;

            _currentCarList = FilterCars(e.CheckedOptions);

            var args = new CarListEventArgs
            {
                CarList = _currentCarList
            };

            CarListRefreshed?.Invoke(this, args);
        }

        private void OnSave(object sender, EventArgs args)
        {
            if (sender.GetType() != typeof(CarListUI)) return;

            var carListArgs = new CarListEventArgs
            {
                CarList = _currentCarList
            };

            CarListSaved?.Invoke(this, carListArgs);
        }

        private List<CarData> FilterCars(List<(string type, string name)> checkedOptions)
        {
            if (checkedOptions.Count == 0) return _completeCarList;
            
            var carSelection = new List<CarData>();
            foreach (var option in checkedOptions)
                switch (option.type)
                {
                    case COUNTRY:
                        carSelection = CombineFilteredCars(option.name, _dataProcessor.CarsByCountry, carSelection);
                        break;
                    case MANUFACTURER:
                        carSelection =
                            CombineFilteredCars(option.name, _dataProcessor.CarsByManufacturer, carSelection);
                        break;
                    case CATEGORY:
                        carSelection = CombineFilteredCars(option.name, _dataProcessor.CarsByCategory, carSelection);
                        break;
                    case DRIVETRAIN:
                        carSelection = CombineFilteredCars(option.name, _dataProcessor.CarsByDrivetrain, carSelection);
                        break;
                    case ASPIRATION:
                        carSelection = CombineFilteredCars(option.name, _dataProcessor.CarsByAspiration, carSelection);
                        break;
                    case TAGS:
                        carSelection = CombineFilteredCars(option.name, _dataProcessor.CarsByTag, carSelection);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            return carSelection;
        }

        private List<CarData> CombineFilteredCars(string option, Dictionary<string, List<CarData>> dict,
            List<CarData> currentSelection)
        {
            dict.TryGetValue(option, out var dictValues);

            if (dictValues != null) currentSelection = currentSelection.Union(dictValues).ToList();

            return currentSelection;
        }

        public class CarListEventArgs : EventArgs
        {
            public List<CarData> CarList { get; private set; }
        }
    }
}
