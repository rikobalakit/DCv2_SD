using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager I;
    private bool _dPadUpPressed = false;
    private bool _dPadDownPressed = false;
    private bool _dPadLeftPressed = false;
    private bool _dPadRightPressed = false;

    public bool DPadUpPressed
    {
        get { return _dPadUpPressed; }
    }

    public bool DPadDownPressed
    {
        get { return _dPadDownPressed; }
    }

    public bool DPadLeftPressed
    {
        get { return _dPadLeftPressed; }
    }

    public bool DPadDRightPressed
    {
        get { return _dPadRightPressed; }
    }

    private void Start()
    {
        if (I != null)
        {
            Debug.LogError("cannot initialize another input manager");
        }
    }

    private void Update()
    {
        float dpadY = Input.GetAxis("DPAD_Y");
        float dpadX = Input.GetAxis("DPAD_X");
        _dPadUpPressed = dpadY < 0f;
        _dPadDownPressed = dpadY > 0f;
        _dPadLeftPressed = dpadX < 0f;
        _dPadRightPressed = dpadX > 0f;
    }

}