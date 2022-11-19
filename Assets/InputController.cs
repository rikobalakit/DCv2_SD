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

        Debug.Log($"lx: {lx}, ly: {ly}, rx: {rx}, ry: {ry}, ");
        _dotL.anchoredPosition = new Vector2(lx * 40, ly * -40);
        _dotR.anchoredPosition = new Vector2(rx * 40, ry * -40);

    }

}