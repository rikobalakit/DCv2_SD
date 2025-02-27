using System.Collections;
using System.Collections.Generic;
using GSA;
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

    private Image _dotImage;
    
    void Start()
    {
        boundX = (_field.rect.width - _dot.rect.width) / 2f;
        boundY = (_field.rect.height - _dot.rect.height) / 2f;

        _dotImage = _dot.GetComponent<Image>();
    }

    void Update()
    {
        float x;
        float y;
        if (_isLeftJoystick)
        {
            x = InputManager.I.LX;
            y = InputManager.I.LY;
        }
        else
        {
            x = InputManager.I.RX;
            y = InputManager.I.RY;
        }

        if (x == 0f && y == 0f)
        {
            _dotImage.color = new Color(0.2f, 0.2f, 0.2f);
        }
        else
        {
            _dotImage.color = Color.white;
        }
        
        _dot.anchoredPosition = new Vector2(x * boundX, y * -boundY);
    }

}