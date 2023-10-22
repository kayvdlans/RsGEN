using System;
using System.Collections.Generic;
using System.Linq;
using RsGEN.Data;
using RsGEN.UI;

namespace RsGEN
{
    public class CarListController
    {
        public static event EventHandler<CarListRefreshedEventArgs> CarListRefreshed;

        public const string COUNTRY = "Country";
        public const string MANUFACTURER = "Manufacturer";
        public const string CATEGORY = "Category";
        public const string DRIVETRAIN = "Drivetrain";
        public const string ASPIRATION = "Aspiration";
        public const string TAGS = "Tags";

        private CarDataProcessor _dataProcessor;
        private List<CarData> _carSelection;

        public CarListController()
        {
            CarDataProcessor.DataProcessed += OnDataProcessed;
        }

        ~CarListController()
        {
            CarDataProcessor.DataProcessed += OnDataProcessed;
            CarListRefreshed = null;
        }

        private void OnDataProcessed(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(CarDataProcessor)) return;

            _dataProcessor = (CarDataProcessor)sender;

            FilterOptionsUI.OnRequestRefresh += OnRequestRefresh;
        }

        private void OnRequestRefresh(object sender, FilterOptionsUI.RequestRefreshEventArgs e)
        {
            if (sender.GetType() != typeof(FilterOptionsUI)) return;

            _carSelection = FilterCars(e.CheckedOptions);

            var args = new CarListRefreshedEventArgs
            {
                CarList = _carSelection
            };

            CarListRefreshed?.Invoke(this, args);
        }

        private List<CarData> FilterCars(List<(string type, string name)> checkedOptions)
        {
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

        public class CarListRefreshedEventArgs : EventArgs
        {
            public List<CarData> CarList { get; set; }
        }
    }
}