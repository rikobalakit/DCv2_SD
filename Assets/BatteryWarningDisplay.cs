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
        Low = 2,
        High = 3,
        Full = 4
        

    }
    
    [SerializeField]
    private TelemetryValues _telemetryValues;

    [SerializeField]
    private List<GameObject> _lowBatteryWarningObjects;
    
    [SerializeField]
    private List<GameObject> _deadBatteryWarningObjects;
    
    [SerializeField]
    private List<GameObject> _usbPowerObjects;

    [SerializeField]
    private AudioSource _lowBatteryAudio;

    [SerializeField]
    private AudioSource _deadBatteryAudio;
    
    private BatteryStatus _currentBatteryStatus = BatteryStatus.Dead;

    private const float MINIMUM_VOLTAGE_BATTERY_FULL = 15.600f;
    private const float MINIMUM_VOLTAGE_BATTERY_LOW = 14.000f;
    private const float MINIMUM_VOLTAGE_BATTERY_DEAD = 13.200f;
    private const float MINIMUM_VOLTAGE_USING_USB = 6.000f;

    private float _audioLastPlayedTime = float.MinValue;
    private const float AUDIO_COOLDOWN_TIME_SECONDS = 2f;
    
    void Update()
    {
        if (_telemetryValues.BatteryVoltage >= MINIMUM_VOLTAGE_BATTERY_FULL)
        {
            _currentBatteryStatus = BatteryStatus.Full;
        }
        else if (_telemetryValues.BatteryVoltage >= MINIMUM_VOLTAGE_BATTERY_LOW &&  _telemetryValues.BatteryVoltage < MINIMUM_VOLTAGE_BATTERY_FULL )
        {
            _currentBatteryStatus = BatteryStatus.High;
        }
        else if (_telemetryValues.BatteryVoltage >= MINIMUM_VOLTAGE_BATTERY_DEAD &&  _telemetryValues.BatteryVoltage < MINIMUM_VOLTAGE_BATTERY_LOW )
        {
            _currentBatteryStatus = BatteryStatus.Low;
        }
        else if (_telemetryValues.BatteryVoltage < MINIMUM_VOLTAGE_BATTERY_DEAD && _telemetryValues.BatteryVoltage > MINIMUM_VOLTAGE_USING_USB)
        {
            _currentBatteryStatus = BatteryStatus.Dead;
        }
        else if (_telemetryValues.BatteryVoltage <= MINIMUM_VOLTAGE_USING_USB)
        {
            _currentBatteryStatus = BatteryStatus.USB;
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
        if (_deadBatteryAudio.isPlaying || _lowBatteryAudio.isPlaying || Time.time - _audioLastPlayedTime < AUDIO_COOLDOWN_TIME_SECONDS)
        {
            return;
        }

        if (newBatteryStatus == BatteryStatus.Low)
        {
            Debug.Log("Playing low battery audio");
            _lowBatteryAudio.Play();
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
