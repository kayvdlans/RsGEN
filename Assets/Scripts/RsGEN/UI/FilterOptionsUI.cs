using System;
using System.Collections.Generic;
using RsGEN.Data;
using UnityEngine;

namespace RsGEN.UI
{
    //TODO: POSSIBLY NEEDED IN THE FUTURE, REFRESH FILTER OPTIONS LIST WHEN DATA IS PROCESSED (DELETE OLD AND ADD NEW)
    public class FilterOptionsUI : MonoBehaviour
    {
        public static event EventHandler<RequestRefreshEventArgs> OnRequestRefresh;

        private CarDataProcessor _dataProcessor;
        private readonly List<(string type, string name)> _checkedOptions = new();

        [SerializeField] private FilterOption optionPrefab;
        [SerializeField] private Transform containerCountryOptions;
        [SerializeField] private Transform containerManufacturerOptions;
        [SerializeField] private Transform containerCategoryOptions;
        [SerializeField] private Transform containerDrivetrainOptions;
        [SerializeField] private Transform containerAspirationOptions;
        [SerializeField] private Transform containerTagOptions;

        private void Awake()
        {
            CarDataProcessor.DataProcessed += OnDataProcessed;
        }

        private void OnDestroy()
        {
            CarDataProcessor.DataProcessed -= OnDataProcessed;
            OnRequestRefresh = null;
        }

        private void OnDataProcessed(object sender, EventArgs args)
        {
            if (sender.GetType() != typeof(CarDataProcessor)) return;

            _dataProcessor = (CarDataProcessor)sender;

            CreateOptions();
        }

        private void CreateOptions()
        {
            if (_dataProcessor == null) return;

            CreateOptions(CarListController.COUNTRY, _dataProcessor.CarsByCountry, containerCountryOptions);
            CreateOptions(CarListController.MANUFACTURER, _dataProcessor.CarsByManufacturer,
                containerManufacturerOptions);
            CreateOptions(CarListController.CATEGORY, _dataProcessor.CarsByCategory, containerCategoryOptions);
            CreateOptions(CarListController.DRIVETRAIN, _dataProcessor.CarsByDrivetrain, containerDrivetrainOptions);
            CreateOptions(CarListController.ASPIRATION, _dataProcessor.CarsByAspiration, containerAspirationOptions);
            CreateOptions(CarListController.TAGS, _dataProcessor.CarsByTag, containerTagOptions);
        }

        private void CreateOptions(string type, Dictionary<string, List<CarData>> dictionary, Transform container)
        {
            foreach (var key in dictionary.Keys)
            {
                var option = Instantiate(optionPrefab, container);
                option.OnChecked += RequestRefresh;
                option.SetOption(type, key);
            }
        }

        private void RequestRefresh(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(FilterOption)) return;

            var option = (FilterOption)sender;

            if (option.Checked && !_checkedOptions.Contains((option.Type, option.OptionName)))
                _checkedOptions.Add((option.Type, option.OptionName));
            else if (!option.Checked && _checkedOptions.Contains((option.Type, option.OptionName)))
                _checkedOptions.Remove((option.Type, option.OptionName));

            var args = new RequestRefreshEventArgs
            {
                CheckedOptions = _checkedOptions
            };

            OnRequestRefresh?.Invoke(this, args);
        }

        public class RequestRefreshEventArgs : EventArgs
        {
            public List<(string type, string name)> CheckedOptions { get; set; }
        }
    }
}