using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalizedToActualValueLabel : MonoBehaviour
{

    [SerializeField]
    private Text _labelText;

    [SerializeField]
    private float _min;

    [SerializeField]
    private float _max;

    [SerializeField]
    private Slider _slider;

    private void Start()
    {
        if (_slider == null)
        {
            _slider = transform.GetComponent<Slider>();
        }

        if (_slider == null)
        {
            Debug.LogError($"[{gameObject.name}]Couldn't find the slider...");
        }
    }

    void Update()
    {
        if (_labelText != null)
        {
            float expandedValue = Mathf.Lerp(_min, _max, _slider.normalizedValue);
            _labelText.text = $"{expandedValue}";
        }
    }
}
