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

    private float _l2;
    private float _r2;
    private float _lx;
    private float _rx;
    private float _ly;
    private float _ry;

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

    public bool DPadRightPressed
    {
        get { return _dPadRightPressed; }
    }

    public float L2
    {
        get { return _l2; }
    }
    
    public float R2
    {
        get { return _l2; }
    }
    
    public float LX
    {
        get { return _lx; }
    }
    
    public float RX
    {
        get { return _lx; }
    }
    
    public float LY
    {
        get { return _ly; }
    }
    
    public float RY
    {
        get { return _ly; }
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
        
        _l2 = Input.GetAxis("L2");
        _r2 = Input.GetAxis("R2");
        _lx = Input.GetAxis("LX");
        _rx = Input.GetAxis("RX");
        _ly = Input.GetAxis("LY");
        _ry = Input.GetAxis("RY");
    }

}