using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoystickIndicator : MonoBehaviour
{
    
    [SerializeField]
    private RectTransform _dot;
    
    [SerializeField]
    private RectTransform _field;

    [SerializeField]
    private bool _isLeftJoystick;

    private float boundX;
    private float boundY;
    
    void Start()
    {
        boundX = (_field.rect.width - _dot.rect.width) / 2f;
        boundY = (_field.rect.height - _dot.rect.height) / 2f;
    }

    void Update()
    {
        float x;
        float y;
        if (_isLeftJoystick)
        {
            x = Input.GetAxis("LX");
            y = Input.GetAxis("LY");
        }
        else
        {
            x = Input.GetAxis("RX");
            y = Input.GetAxis("RY");
        }
        
        _dot.anchoredPosition = new Vector2(x * 80, y * -80);
    }

}