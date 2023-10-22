using System.Collections.Generic;
using RsGEN.Data;
using UnityEngine;

namespace RsGEN.UI
{
    public class CarListUI : MonoBehaviour
    {
        private List<CarData> _completeCarList;
        private List<CarData> _carList;

        [SerializeField] private CarListEntry carListEntryPrefab;
        [SerializeField] private Transform carListContent;

        private void Awake()
        {
            DataLoader.DataLoaded += PrepareCarList;
            CarListController.CarListRefreshed += UpdateCarList;
        }

        private void OnDestroy()
        {
            DataLoader.DataLoaded -= PrepareCarList;
            CarListController.CarListRefreshed -= UpdateCarList;
        }

        private void PrepareCarList(object sender, DataLoader.DataLoadedEventArgs e)
        {
            if (sender.GetType() != typeof(DataLoader)) return;
            if (e.DataType != DataLoader.DataLoadedEventArgs.DataTypes.CarData) return;
            if (carListContent.childCount == e.CarData.cars.Length) return;

            Debug.Log("should prepare car list");

            _completeCarList = new List<CarData>(e.CarData.cars);

            //If for whatever reason the car list is longer than the number of cars in the data, destroy the excess entries.
            while (carListContent.childCount > e.CarData.cars.Length)
                Destroy(carListContent.GetChild(carListContent.childCount - 1).gameObject);

            while (carListContent.childCount < e.CarData.cars.Length) Instantiate(carListEntryPrefab, carListContent);

            RefreshCarList();

            Debug.Log("Car list prepared");
        }

        private void UpdateCarList(object sender, CarListController.CarListRefreshedEventArgs e)
        {
            if (sender.GetType() != typeof(CarListController)) return;

            _carList = e.CarList;

            RefreshCarList();

            Debug.Log("Car list updated");
        }

        private void RefreshCarList()
        {
            if (_carList == null || _carList.Count == 0) _carList = _completeCarList;

            for (var i = 0; i < carListContent.childCount; i++)
                if (i < _carList.Count)
                    EnableCarListEntry(i);
                else if (!DisableCarListEntry(i))
                    //avoid looping through the rest of the list if we've already disabled all the remaining entries
                    break;
        }

        private void EnableCarListEntry(int index)
        {
            var childObject = carListContent.GetChild(index).gameObject;
            childObject.GetComponent<CarListEntry>().SetCarData(_carList[index], index);
            childObject.SetActive(true);
        }

        private bool DisableCarListEntry(int index)
        {
            var childObject = carListContent.GetChild(index).gameObject;
            if (childObject.activeSelf)
            {
                childObject.SetActive(false);
                return true;
            }

            return false;
        }
    }
}