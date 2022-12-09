using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoftValuesManager : MonoBehaviour
{

    [SerializeField]
    private Slider _angleToleranceSlider;
    
    [SerializeField]
    private Slider _turningMultiplierSlider;
    
    [SerializeField]
    private Slider _additiveThrottleMultiplerSlider;
    
    public float AngleToleranceNormalized = 0.5f;
    public float TurningMultiplierNormalized = 0.5f;
    public float AdditiveThrottleMultiplerNormalized = 0.5f;

    public static SoftValuesManager I;

    public void Start()
    {
        if (I != null)
        {
            Debug.LogError("Already exists a SoftValuesManager");
            return;
        }

        I = this;

        AngleToleranceNormalized = PlayerPrefs.GetFloat("AngleToleranceNormalized");
        TurningMultiplierNormalized = PlayerPrefs.GetFloat("TurningMultiplierNormalized");
        AdditiveThrottleMultiplerNormalized = PlayerPrefs.GetFloat("AdditiveThrottleMultiplerNormalized");

        _angleToleranceSlider.normalizedValue = AngleToleranceNormalized;
        _turningMultiplierSlider.normalizedValue = TurningMultiplierNormalized;
        _additiveThrottleMultiplerSlider.normalizedValue = AdditiveThrottleMultiplerNormalized;
    }

    private void Update()
    {
        AngleToleranceNormalized = _angleToleranceSlider.normalizedValue;
        TurningMultiplierNormalized = _turningMultiplierSlider.normalizedValue;
        AdditiveThrottleMultiplerNormalized = _additiveThrottleMultiplerSlider.normalizedValue;
        
        PlayerPrefs.SetFloat("AngleToleranceNormalized", AngleToleranceNormalized);
        PlayerPrefs.SetFloat("TurningMultiplierNormalized", TurningMultiplierNormalized);
        PlayerPrefs.SetFloat("AdditiveThrottleMultiplerNormalized", AdditiveThrottleMultiplerNormalized);
    }

    public static byte NormalizedFloatToByte(float inputFloat)
    {
        return (byte)(int)(Mathf.Lerp(-128, 127, inputFloat)) ;
    }
}
