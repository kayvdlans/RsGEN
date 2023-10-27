using RsGEN.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RsGEN.Mono.UI
{
    public class CarListEntry : MonoBehaviour
    {
        private static readonly Color32 COLOR_EVEN_INDEX = new(167, 167, 200, 255);
        private static readonly Color32 COLOR_UNEVEN_INDEX = new(178, 178, 200, 255);

        [SerializeField] private Image background;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI countryText;
        [SerializeField] private TextMeshProUGUI manufacturerText;
        [SerializeField] private TextMeshProUGUI yearText;
        
        // [SerializeField] private TextMeshPro basePpText;
        // [SerializeField] private TextMeshPro categoryText;
        // [SerializeField] private TextMeshPro drivetrainText;
        // [SerializeField] private TextMeshPro powerBhpText;
        // [SerializeField] private TextMeshPro weightKgText;
        // [SerializeField] private TextMeshPro aspirationText;
        // [SerializeField] private TextMeshPro tagsText;

        public void SetCarData(CarData carData, int index)
        {
            background.color = index % 2 == 0 ? COLOR_EVEN_INDEX : COLOR_UNEVEN_INDEX;
            nameText.SetText(carData.name);
            countryText.SetText(carData.country);
            manufacturerText.SetText(carData.manufacturer);
            yearText.SetText(TransformYear(carData.year));
        }

        private string TransformYear(short year)
        {
            switch (year)
            {
                case -2:
                    return "Invalid";
                case -1:
                    return "Unknown";
                default:
                    return year.ToString();
            }
        }
    }
}