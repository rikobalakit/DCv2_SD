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
            var eulerAngles = TelemetryValues.I.Orientatation.eulerAngles;
            
            _robotTransform.localRotation = Quaternion.Euler(-eulerAngles.x, -eulerAngles.y, eulerAngles.z);
        }
    }
}
