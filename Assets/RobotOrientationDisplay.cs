using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotOrientationDisplay : MonoBehaviour
{
    [SerializeField]
    private Transform _robotOffsetRotationTransform;

    [SerializeField]
    private Transform _robotTransform;

    [SerializeField]
    private Text _telemetryTextBox;
    
    void Update()
    {
        if (TelemetryValues.I == null)
        {
            return;
        }
        
        if (_robotTransform != null)
        {
            var eulerAngles = TelemetryValues.I.Orientatation.eulerAngles;
            
            _robotTransform.localRotation = Quaternion.Euler(-eulerAngles.x, -eulerAngles.y, eulerAngles.z);
        }
        
        if (Input.GetKey("1") && Input.GetKey("3"))
        {
			var currentSpin = _robotTransform.localRotation.eulerAngles.y;
            _robotOffsetRotationTransform.localRotation = Quaternion.Euler(0f, -currentSpin, 0f);
        }

        var calculatedAcceleration = TelemetryValues.I.Acceleration;

        _telemetryTextBox.text = $"{TelemetryValues.I.BatteryVoltage}\n" +
            $"{TelemetryValues.I.Orientatation.w}\n" +
            $"{TelemetryValues.I.Orientatation.x}\n" +
            $"{TelemetryValues.I.Orientatation.y}\n" +
            $"{TelemetryValues.I.Orientatation.z}\n" +
            $"{calculatedAcceleration.x}\n" +
            $"{calculatedAcceleration.y}\n" +
            $"{calculatedAcceleration.z}\n" +
            $"{TelemetryValues.I.EscTemperature}\n" +
            $"{TelemetryValues.I.EscVoltage}\n" +
            $"{TelemetryValues.I.EscCurrent}\n" +
            $"{TelemetryValues.I.EscUsedMah}\n" +
            $"{TelemetryValues.I.EscRpm}\n";
    }
}
