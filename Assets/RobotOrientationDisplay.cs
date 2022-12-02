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


    float wrapEulerAngle(float inputAngle)
    {
        while (inputAngle > 180)
        {
            inputAngle -= 360;
        }

        while (inputAngle < -180)
        {
            inputAngle += 360;
        }
        return inputAngle;
    }

    string getHeadingAngleFromRobotRotation()
    {
        var angleToWorkWith = wrapEulerAngle(_robotTransform.eulerAngles.y);
        // 360 is N, 90 is E
        if (angleToWorkWith <= -135f || angleToWorkWith >= 135f)
        {
            return "S";
        }
        else if (angleToWorkWith >= -45f && angleToWorkWith <= 45f)
        {
            return "N";
        }
        else if (angleToWorkWith >= -135 && angleToWorkWith <= -45f)
        {
            return "E";
        }
        else if (angleToWorkWith >= 45f && angleToWorkWith <= 135f)
        {
            return "W";
        }

        return "";
    }
    
    void Update()
    {
        if (TelemetryValues.I == null)
        {
            return;
        }
        var eulerAngles = TelemetryValues.I.Orientatation.eulerAngles;

        if (_robotTransform != null)
        {
            _robotTransform.localRotation = Quaternion.Euler(-eulerAngles.x, -eulerAngles.y, eulerAngles.z);
        }

        if (Input.GetKey("1") && Input.GetKey("3"))
        {
			var currentSpin = _robotTransform.localRotation.eulerAngles.y;
            _robotOffsetRotationTransform.localRotation = Quaternion.Euler(0f, -currentSpin, 0f);
        }

        var calculatedAcceleration = TelemetryValues.I.Acceleration;

        _telemetryTextBox.text = $"{TelemetryValues.I.BatteryVoltage:0.0}\n\n" +
            $"{wrapEulerAngle(-eulerAngles.y):0.0} ({getHeadingAngleFromRobotRotation()})\n" +
            $"{wrapEulerAngle(eulerAngles.z):0.0}\n" +
            $"{wrapEulerAngle(-eulerAngles.x):0.0}\n\n" +
            $"{calculatedAcceleration.x:0.0}\n" +
            $"{calculatedAcceleration.y:0.0}\n" +
            $"{calculatedAcceleration.z:0.0}\n\n" +
            $"{TelemetryValues.I.EscTemperature:0.0}\n" +
            $"{TelemetryValues.I.EscVoltage:0.0}\n" +
            $"{TelemetryValues.I.EscCurrent:0.0}\n" +
            $"{TelemetryValues.I.EscUsedMah:0.0}\n" +
            $"{TelemetryValues.I.EscRpm:0.0}\n";
    }
}
