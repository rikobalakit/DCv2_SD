using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotOrientationDisplay : MonoBehaviour
{

    [SerializeField]
    private Transform _robotTransform;
    
    void Update()
    {
        if (TelemetryValues.I != null && _robotTransform != null)
        {
            _robotTransform.rotation = TelemetryValues.I.Orientatation;
        }
    }
}
