using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelemetryValues : MonoBehaviour
{

    public static TelemetryValues I;

    public float BatteryVoltage;

    public Quaternion Orientatation;
    public Vector3 BnoAcceleration;
    public Vector3 LisAcceleration;

    public float EscTemperature;
    public float EscVoltage;
    public float EscCurrent;
    public float EscUsedMah;
    public float EscRpm;
    
    public float BnoTemp;

    public int BnoCalibrationSystem;
    public int BnoCalibrationGyro;
    public int BnoCalibrationAccelerometer;
    public int BnoCalibrationMagnetometer;

    public int SmartHeadingOffset;

    public float WeaponThrottle;

    public string PcLogFileName;
    public int PcLogLinesLength;
    //public bool SomethingChangedSinceLastLog = true;

    public Vector3 OrientationEuler => Orientatation.eulerAngles;

    public Vector3 Acceleration
    {
        get
        {
            float x;
            float y;
            float z;
            
            if (Mathf.Abs(BnoAcceleration.x) < 10)
            {
                x = BnoAcceleration.x;
            }
            else
            {
                x = LisAcceleration.x;
            }
            
            if (Mathf.Abs(BnoAcceleration.y) < 10)
            {
                y = BnoAcceleration.y;
            }
            else
            {
                y = LisAcceleration.y;
            }
            
            if (Mathf.Abs(BnoAcceleration.z) < 10)
            {
                z = BnoAcceleration.z;
            }
            else
            {
                z = LisAcceleration.z;
            }

            return new Vector3(x, y, z);
        }
    }

    public void Start()
    {
        if (I != null)
        {
            Debug.LogError("TelemetryValues already initialized");
        }
        else
        {
            I = this;
        }
    }

}