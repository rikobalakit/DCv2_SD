using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputController : MonoBehaviour
{

    [SerializeField]
    private TextMeshPro _rxText;

    [SerializeField]
    private RectTransform _dotL;

    [SerializeField]
    private RectTransform _dotR;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float lx = Input.GetAxis("LX");
        float ly = Input.GetAxis("LY");
        float rx = Input.GetAxis("RX");
        float ry = Input.GetAxis("RY");
        
        _dotL.anchoredPosition = new Vector2(lx * 80, ly * -80);
        _dotR.anchoredPosition = new Vector2(rx * 80, ry * -80);

        //Debug.LogError($"L2: {Input.GetAxis("L2")}");

    }

}