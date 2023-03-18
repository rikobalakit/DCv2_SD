using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outputorientation : MonoBehaviour
{

    void Update()
    {
        Debug.LogError($"{gameObject.name}, {transform.rotation.eulerAngles.x},{transform.rotation.eulerAngles.y},{transform.rotation.eulerAngles.z}");
    }
}
