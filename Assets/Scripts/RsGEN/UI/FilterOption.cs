using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RsGEN.UI
{
    public class FilterOption : MonoBehaviour
    {
        public event EventHandler OnChecked;

        [SerializeField] private Toggle toggle;
        [SerializeField] private TextMeshProUGUI textOptionName;

        private void Awake()
        {
            toggle.onValueChanged.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveListener(OnClick);
            OnChecked = null;
        }

        public void SetOption(string type, string optionName)
        {
            Type = type;

            textOptionName.SetText(optionName);
        }

        private void OnClick(bool value)
        {
            OnChecked?.Invoke(this, EventArgs.Empty);
        }

        public bool Checked => toggle.isOn;
        public string Type { get; private set; }
        public string OptionName => textOptionName.text;
    }
}
