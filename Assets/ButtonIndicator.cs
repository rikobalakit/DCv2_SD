using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicator : MonoBehaviour
{

    private enum ButtonType
    {
        Button,
        Trigger,
        DPadLeft,
        DPadRight,
        DPadUp,
        DPadDown
    }
    
    [SerializeField]
    private Image _backgroundPanel;

    [SerializeField]
    private string _buttonName;

    [SerializeField]
    private ButtonType _buttonType;

    private Color _colorActive = new Color(1f, 1f, 1f, 0.8f);
    private Color _colorDown = new Color(1f, 1f, 1f, 0.1f);
    private Color _colorUp = new Color(1f, 1f, 1f, 1f);
    private Color _colorInactive = new Color(1f, 1f, 1f, 0.25f);
    private Color _colorOff = new Color(1f, 1f, 1f, 0.05f);
        
    void Update()
    {
        if (_buttonType == ButtonType.Button)
        {
            if (String.IsNullOrEmpty(_buttonName))
            {
                _backgroundPanel.color = _colorOff;
                return;
            }
            
            if (Input.GetButtonDown(_buttonName))
            {
                _backgroundPanel.color = _colorDown;
            }
            else if (Input.GetButtonUp(_buttonName))
            {
                _backgroundPanel.color = _colorUp;
            }
            else if (Input.GetButton(_buttonName))
            {
                _backgroundPanel.color = _colorActive;
            }
            else
            {
                _backgroundPanel.color = _colorInactive;
            }
        }
        else if (_buttonType == ButtonType.Trigger)
        {
            if (String.IsNullOrEmpty(_buttonName))
            {
                _backgroundPanel.color = _colorOff;
                return;
            }
            
            float triggerValue = Input.GetAxis(_buttonName);
            {
                _backgroundPanel.color = Color.Lerp(_colorInactive, _colorActive, triggerValue);
            }
        }
        else if (_buttonType == ButtonType.DPadUp)
        {
            float dpadY = Input.GetAxis("DPAD_Y");

            if (dpadY < 0)
            {
                _backgroundPanel.color = _colorActive;
            }
            else
            {
                _backgroundPanel.color = _colorInactive;
            }
        }
        else if (_buttonType == ButtonType.DPadDown)
        {
            float dpadY = Input.GetAxis("DPAD_Y");
            
            if (dpadY > 0)
            {
                _backgroundPanel.color = _colorActive;
            }
            else
            {
                _backgroundPanel.color = _colorInactive;
            }
        }
        else if (_buttonType == ButtonType.DPadLeft)
        {
            float dpadX = Input.GetAxis("DPAD_X");
            
            if (dpadX < 0)
            {
                _backgroundPanel.color = _colorActive;
            }
            else
            {
                _backgroundPanel.color = _colorInactive;
            }
        }
        else if (_buttonType == ButtonType.DPadRight)
        {
            float dpadX = Input.GetAxis("DPAD_X");
            
            if (dpadX > 0)
            {
                _backgroundPanel.color = _colorActive;
            }
            else
            {
                _backgroundPanel.color = _colorInactive;
            }
        }

    }
}
