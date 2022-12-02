using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotOrientationDisplay : MonoBehaviour
{
    [SerializeField]
    private Transform _robotOffsetRotationTransform;

    [SerializeField]
    private Transform _robotTransform;
    
    void Update()
    {
        if (TelemetryValues.I != null && _robotTransform != null)
        {
            var eulerAngles = TelemetryValues.I.Orientatation.eulerAngles;
            
            _robotTransform.localRotation = Quaternion.Euler(-eulerAngles.x, -eulerAngles.y, eulerAngles.z);
        }
        
        if (Input.GetKey("1") && Input.GetKey("3"))
        {
			var currentSpin = _robotTransform.localRotation.eulerAngles.y;
            _robotOffsetRotationTransform.localRotation = Quaternion.Euler(0f, -currentSpin, 0f);
        }
    }
}
