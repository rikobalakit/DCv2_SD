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

        _telemetryTextBox.text = $"{TelemetryValues.I.BatteryVoltage:00.0}\n" +
            $"{TelemetryValues.I.Orientatation.w:00.0}\n" +
            $"{TelemetryValues.I.Orientatation.x:00.0}\n" +
            $"{TelemetryValues.I.Orientatation.y:00.0}\n" +
            $"{TelemetryValues.I.Orientatation.z:00.0}\n" +
            $"{calculatedAcceleration.x:00.0}\n" +
            $"{calculatedAcceleration.y:00.0}\n" +
            $"{calculatedAcceleration.z:00.0}\n" +
            $"{TelemetryValues.I.EscTemperature:00.0}\n" +
            $"{TelemetryValues.I.EscVoltage:00.0}\n" +
            $"{TelemetryValues.I.EscCurrent:00.0}\n" +
            $"{TelemetryValues.I.EscUsedMah:00.0}\n" +
            $"{TelemetryValues.I.EscRpm:00.0}\n";
    }
}
