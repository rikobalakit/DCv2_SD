using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryWarningDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    private enum BatteryStatus
    {
        USB = 0,
        Dead = 1,
        Critical= 2,
        Low = 3,
        High = 4,
        Full = 5
        

    }
    
    [SerializeField]
    private TelemetryValues _telemetryValues;

    [SerializeField]
    private List<GameObject> _lowBatteryWarningObjects;
    
    [SerializeField]
    private List<GameObject> _criticalBatteryWarningObjects;
    
    [SerializeField]
    private List<GameObject> _deadBatteryWarningObjects;
    
    [SerializeField]
    private List<GameObject> _usbPowerObjects;

    [SerializeField]
    private AudioSource _lowBatteryAudio;
    
    [SerializeField]
    private AudioSource _criticalBatteryAudio;

    [SerializeField]
    private AudioSource _deadBatteryAudio;
    
    private BatteryStatus _currentBatteryStatus = BatteryStatus.Dead;

    private const float MINIMUM_VOLTAGE_BATTERY_FULL = 16.600f;
    private const float MINIMUM_VOLTAGE_BATTERY_HIGH = 15.200f;
    private const float MINIMUM_VOLTAGE_BATTERY_LOW = 14.000f;
    private const float MINIMUM_VOLTAGE_BATTERY_DEAD = 13.200f;
    private const float MINIMUM_VOLTAGE_USING_USB = 6.000f;

    private float _audioLastPlayedTime = float.MinValue;
    private float _audioCooldownTime = 2f;

    private float _lastTimeBatteryWasStable = float.MinValue;
    private const float BATTERY_STABILITY_PERIOD_SECONDS = 0.5f;
    
    void Update()
    {
        BatteryStatus newBatteryStatus = BatteryStatus.USB;
        
        if (_telemetryValues.BatteryVoltage >= MINIMUM_VOLTAGE_BATTERY_FULL)
        {
            newBatteryStatus = BatteryStatus.Full;
        }
        else if (_telemetryValues.BatteryVoltage >= MINIMUM_VOLTAGE_BATTERY_HIGH &&  _telemetryValues.BatteryVoltage < MINIMUM_VOLTAGE_BATTERY_FULL )
        {
            newBatteryStatus = BatteryStatus.High;
        }
        else if (_telemetryValues.BatteryVoltage >= MINIMUM_VOLTAGE_BATTERY_LOW &&  _telemetryValues.BatteryVoltage < MINIMUM_VOLTAGE_BATTERY_HIGH )
        {
            newBatteryStatus = BatteryStatus.Low;
        }
        else if (_telemetryValues.BatteryVoltage >= MINIMUM_VOLTAGE_BATTERY_DEAD &&  _telemetryValues.BatteryVoltage < MINIMUM_VOLTAGE_BATTERY_LOW )
        {
            newBatteryStatus = BatteryStatus.Critical;
        }
        else if (_telemetryValues.BatteryVoltage < MINIMUM_VOLTAGE_BATTERY_DEAD && _telemetryValues.BatteryVoltage > MINIMUM_VOLTAGE_USING_USB)
        {
            newBatteryStatus = BatteryStatus.Dead;
        }
        else if (_telemetryValues.BatteryVoltage <= MINIMUM_VOLTAGE_USING_USB)
        {
            newBatteryStatus = BatteryStatus.USB;
        }

        if (_currentBatteryStatus != newBatteryStatus)
        {
            if (Time.time - _lastTimeBatteryWasStable > BATTERY_STABILITY_PERIOD_SECONDS)
            {
                _currentBatteryStatus = newBatteryStatus;
                _lastTimeBatteryWasStable = Time.time;
            }
        }
        else
        {
            _lastTimeBatteryWasStable = Time.time;
        }

        UpdateDisplay(_currentBatteryStatus);
        UpdateAudio(_currentBatteryStatus);
    }

    private void UpdateDisplay(BatteryStatus newBatteryStatus)
    {
        foreach (var lowBatteryObject in _lowBatteryWarningObjects)
        {
            lowBatteryObject.SetActive(newBatteryStatus == BatteryStatus.Low);
        }
        
        foreach (var criticalBatteryObject in _criticalBatteryWarningObjects)
        {
            criticalBatteryObject.SetActive(newBatteryStatus == BatteryStatus.Critical);
        }
            
        foreach (var deadBatteryObject in _deadBatteryWarningObjects)
        {
            deadBatteryObject.SetActive(newBatteryStatus == BatteryStatus.Dead);
        }
            
        foreach (var usbPowerObject in _usbPowerObjects)
        {
            usbPowerObject.SetActive(newBatteryStatus == BatteryStatus.USB);
        }
    }

    private void UpdateAudio(BatteryStatus newBatteryStatus)
    {
        switch (newBatteryStatus)
        {

            case BatteryStatus.USB:
                _audioCooldownTime = 10f;
                break;
            case BatteryStatus.Dead:
                _audioCooldownTime = 2f;
                break;
            case BatteryStatus.Critical:
                _audioCooldownTime = 2f;
                break;
            case BatteryStatus.Low:
                _audioCooldownTime = 10f;
                break;
            case BatteryStatus.High:
                _audioCooldownTime = 10f;
                break;
            case BatteryStatus.Full:
                _audioCooldownTime = 10f;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newBatteryStatus), newBatteryStatus, null);
        }
        
        if (_deadBatteryAudio.isPlaying || _lowBatteryAudio.isPlaying || Time.time - _audioLastPlayedTime < _audioCooldownTime)
        {
            return;
        }

        if (newBatteryStatus == BatteryStatus.Low)
        {
            Debug.Log("Playing low battery audio");
            _lowBatteryAudio.Play();
            _audioLastPlayedTime = Time.time;
        }
        
        if (newBatteryStatus == BatteryStatus.Critical)
        {
            Debug.Log("Playing critical battery audio");
            _criticalBatteryAudio.Play();
            _audioLastPlayedTime = Time.time;
        }
        
        if (newBatteryStatus == BatteryStatus.Dead)
        {
            Debug.Log("Playing dead battery audio");
            _deadBatteryAudio.Play();
            _audioLastPlayedTime = Time.time;
        }
    }
}
