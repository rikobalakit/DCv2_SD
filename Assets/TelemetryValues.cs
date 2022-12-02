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