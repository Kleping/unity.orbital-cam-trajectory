using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace View
{
    public class RecordItem : MonoBehaviour, IRecordItem
    {
        [Space(5)]
        [SerializeField] private TextMeshProUGUI _label;

        private UnityAction _onClick;
        
        
        public void Init(long binary, UnityAction onClick)
        {
            _label.text = DateTime.FromBinary(binary).ToString(CultureInfo.InvariantCulture);
            _onClick = onClick;
            gameObject.SetActive(true);
        }

        public void Clear()
        {
            _label.text = string.Empty;
            _onClick = null;
            gameObject.SetActive(false);
        }

        
        public void OnClick()
        {
            _onClick?.Invoke();
        }
    }
}
