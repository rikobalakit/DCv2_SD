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
        var angleToWorkWith = wrapEulerAngle(_robotTransform.eulerAngles.y + TelemetryValues.I.SmartHeadingOffset);
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
            return "W";
        }
        else if (angleToWorkWith >= 45f && angleToWorkWith <= 135f)
        {
            return "E";
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
            
            _robotTransform.localRotation = Quaternion.Slerp(_robotTransform.localRotation, Quaternion.Euler(-eulerAngles.x, -eulerAngles.y + TelemetryValues.I.SmartHeadingOffset, eulerAngles.z), 0.8f)  ;
        }

        var calculatedAcceleration = TelemetryValues.I.Acceleration;

        _telemetryTextBox.text = $"{TelemetryValues.I.BatteryVoltage:0.0} V\n\n" +
            $"{wrapEulerAngle(-eulerAngles.y + TelemetryValues.I.SmartHeadingOffset):0.0}° ({getHeadingAngleFromRobotRotation()})\n" +
            $"{wrapEulerAngle(eulerAngles.z):0.0}°\n" +
            $"{wrapEulerAngle(-eulerAngles.x):0.0}°\n\n" +
            $"{calculatedAcceleration.x/9.8:0.0} G\n" +
            $"{calculatedAcceleration.y/9.8:0.0} G\n" +
            $"{calculatedAcceleration.z/9.8:0.0} G\n\n" +
            $"{TelemetryValues.I.EscTemperature:0.0} C\n" +
            $"{TelemetryValues.I.EscVoltage:0.0} V\n" +
            $"{TelemetryValues.I.EscCurrent:0.0} A\n" +
            $"{TelemetryValues.I.EscUsedMah:0.0} mAH\n" +
            $"{TelemetryValues.I.EscRpm:0.0} eRPM\n\n" +
            $"{TelemetryValues.I.BnoTemp:0.0} C\n\n" +
            $"{TelemetryValues.I.BnoCalibrationSystem:0}\n" +
            $"{TelemetryValues.I.BnoCalibrationGyro:0}\n" +
            $"{TelemetryValues.I.BnoCalibrationAccelerometer:0}\n" +
            $"{TelemetryValues.I.BnoCalibrationMagnetometer:0}\n";
    }
}
