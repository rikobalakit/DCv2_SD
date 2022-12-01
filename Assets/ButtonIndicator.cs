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
        
    void Update()
    {
        if (_buttonType == ButtonType.Button)
        {
            if (String.IsNullOrEmpty(_buttonName))
            {
                _backgroundPanel.color = new Color(1f, 1f, 1f, 0.05f);
                return;
            }
            
            if (Input.GetButtonDown(_buttonName))
            {
                _backgroundPanel.color = new Color(1f, 1f, 1f, 1f);
            }
            else if (Input.GetButtonUp(_buttonName))
            {
                _backgroundPanel.color = new Color(1f, 1f, 1f, 0.1f);
            }
            else if (Input.GetButton(_buttonName))
            {
                _backgroundPanel.color = new Color(1f, 1f, 1f, 0.8f);
            }
            else
            {
                _backgroundPanel.color = new Color(1f, 1f, 1f, 0.25f);
            }
        }
        else if (_buttonType == ButtonType.Trigger)
        {
            if (String.IsNullOrEmpty(_buttonName))
            {
                _backgroundPanel.color = new Color(1f, 1f, 1f, 0.05f);
                return;
            }
            
            
        }
        else if (_buttonType == ButtonType.DPadUp)
        {
            float dpadY = Input.GetAxis("DPAD_Y");
            Debug.LogError($"DPAD_Y: {dpadY}");
        }
        else if (_buttonType == ButtonType.DPadDown)
        {
            float dpadY = Input.GetAxis("DPAD_Y");
        }
        else if (_buttonType == ButtonType.DPadLeft)
        {
            float dpadX = Input.GetAxis("DPAD_X");
            Debug.LogError($"DPAD_X: {dpadX}");
        }
        else if (_buttonType == ButtonType.DPadRight)
        {
            float dpadX = Input.GetAxis("DPAD_X");
        }

    }
}
