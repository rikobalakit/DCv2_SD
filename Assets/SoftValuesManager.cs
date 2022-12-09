using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftValuesManager : MonoBehaviour
{

    public float AngleToleranceNormalized = 0.5f;
    public float TurningMultiplierNormalized = 0.25f;
    public float AdditiveThrottleMultiplerNormalized = 0.75f;

    public static SoftValuesManager I;

    public void Start()
    {
        if (I != null)
        {
            Debug.LogError("Already exists a SoftValuesManager");
            return;
        }

        I = this;
    }

    public static byte NormalizedFloatToByte(float inputFloat)
    {
        return (byte)(int)(Mathf.Lerp(-128, 127, inputFloat)) ;
    }
}
