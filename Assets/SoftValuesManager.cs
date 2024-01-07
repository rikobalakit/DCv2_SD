using System;
using System.Collections;
using System.Collections.Generic;
using PearlSoft.Scripts.Runtime.ScreenUI.OutputElements;
using UnityEngine;
using UnityEngine.UI;

public class SoftValuesManager : MonoBehaviour
{
    
    [SerializeField]
    private MonospaceTextLogOutput _console;
    
    [SerializeField]
    private Slider _maxWeaponThrottleSlider;

    [SerializeField]
    private Slider _angleToleranceSlider;
    
    [SerializeField]
    private Slider _turningMultiplierSlider;
    
    [SerializeField]
    private Slider _additiveThrottleMultiplerSlider;
    
    [SerializeField]
    private Slider _weaponThrottleMultiplerSlider;
    
    public float MaxWeaponThrottleNormalized = 0.5f;
    public float AngleToleranceNormalized = 0.5f;
    public float TurningMultiplierNormalized = 0.5f;
    public float AdditiveThrottleMultiplerNormalized = 0.5f;
    public float WeaponThrottleMultiplerNormalized = 0.5f;

    public static SoftValuesManager I;

    public void Start()
    {
        if (I != null)
        {
            Debug.LogError("Already exists a SoftValuesManager");
            return;
        }

        I = this;
        
        LoadSlot(1);
    }

    private void Update()
    {
        MaxWeaponThrottleNormalized = _maxWeaponThrottleSlider.normalizedValue;
        AngleToleranceNormalized = _angleToleranceSlider.normalizedValue;
        TurningMultiplierNormalized = _turningMultiplierSlider.normalizedValue;
        AdditiveThrottleMultiplerNormalized = _additiveThrottleMultiplerSlider.normalizedValue;
        WeaponThrottleMultiplerNormalized = _weaponThrottleMultiplerSlider.normalizedValue;
    }

    public void SaveSlot(int slot)
    {
        FBPP.SetFloat($"MaxWeaponThrottleNormalized{slot}", MaxWeaponThrottleNormalized);
        FBPP.SetFloat($"AngleToleranceNormalized{slot}", AngleToleranceNormalized);
        FBPP.SetFloat($"TurningMultiplierNormalized{slot}", TurningMultiplierNormalized);
        FBPP.SetFloat($"AdditiveThrottleMultiplerNormalized{slot}", AdditiveThrottleMultiplerNormalized);
        FBPP.SetFloat($"WeaponThrottleMultiplerNormalized{slot}", WeaponThrottleMultiplerNormalized);
        _console.LogText($"Saved Slot {slot}");

    }

    public void LoadSlot(int slot)
    {
        MaxWeaponThrottleNormalized = FBPP.GetFloat($"MaxWeaponThrottleNormalized{slot}");
        AngleToleranceNormalized = FBPP.GetFloat($"AngleToleranceNormalized{slot}");
        TurningMultiplierNormalized = FBPP.GetFloat($"TurningMultiplierNormalized{slot}");
        AdditiveThrottleMultiplerNormalized = FBPP.GetFloat($"AdditiveThrottleMultiplerNormalized{slot}");
        WeaponThrottleMultiplerNormalized = FBPP.GetFloat($"WeaponThrottleMultiplerNormalized{slot}");
        
        _maxWeaponThrottleSlider.normalizedValue = MaxWeaponThrottleNormalized;
        _angleToleranceSlider.normalizedValue = AngleToleranceNormalized;
        _turningMultiplierSlider.normalizedValue = TurningMultiplierNormalized;
        _additiveThrottleMultiplerSlider.normalizedValue = AdditiveThrottleMultiplerNormalized;
        _weaponThrottleMultiplerSlider.normalizedValue = WeaponThrottleMultiplerNormalized;
        
        MaxWeaponThrottleNormalized = _maxWeaponThrottleSlider.normalizedValue;
        AngleToleranceNormalized = _angleToleranceSlider.normalizedValue;
        TurningMultiplierNormalized = _turningMultiplierSlider.normalizedValue;
        AdditiveThrottleMultiplerNormalized = _additiveThrottleMultiplerSlider.normalizedValue;
        WeaponThrottleMultiplerNormalized = _weaponThrottleMultiplerSlider.normalizedValue;
        
        _console.LogText($"Loaded Slot {slot}");
    }

    public static byte NormalizedFloatToByte(float inputFloat)
    {
        return (byte)(int)(Mathf.Lerp(-128, 127, inputFloat)) ;
    }
}
