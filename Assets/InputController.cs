using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputController : MonoBehaviour
{

    [SerializeField]
    private TextMeshPro _rxText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float rx = Input.GetAxis("RX");
        
        Debug.Log($"rx: {rx}");
    }
}
