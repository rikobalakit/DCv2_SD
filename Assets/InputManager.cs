using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{

    public static InputManager I;
    private bool _dPadUpPressed = false;
    private bool _dPadDownPressed = false;
    private bool _dPadLeftPressed = false;
    private bool _dPadRightPressed = false;

    private bool _l1;
    private bool _r1;
    private bool _l3;
    private bool _l4;
    private bool _r3;
    private bool _r4;
    
    private float _l2;
    private float _r2;
    private float _lx;
    private float _rx;
    private float _ly;
    private float _ry;

    public const float DEADZONE_JOYSTICK_RADIUS = 0.1f;
    public const float DEADZONE_TRIGGER_THRESHOLD = 0.1f;

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
        get { return _r2; }
    }
    
    public bool L1
    {
        get { return _l3; }
    }
    
    public bool R1
    {
        get { return _r1; }
    }
    
    public bool L3
    {
        get { return _l3; }
    }
    
    public bool R3
    {
        get { return _r3; }
    }
    
    public bool L4
    {
        get { return _l4; }
    }
    
    public bool R4
    {
        get { return _r4; }
    }
    
    public float LX
    {
        get { return _lx; }
    }
    
    public float RX
    {
        get { return _rx; }
    }
    
    public float LY
    {
        get { return _ly; }
    }
    
    public float RY
    {
        get { return _ry; }
    }

    public short Heading
    {
        get
        {
            var headingDirectionRaw = (Mathf.Rad2Deg * Mathf.Atan(RY/RX)) + 90f;
            
            if (headingDirectionRaw > 180f)
            {
                headingDirectionRaw -= 360f;
            }

            if (RX < 0f)
            {
                headingDirectionRaw -= 180f;
            }

            return (short)headingDirectionRaw;
        }
    }

    public Int16 ButtonBytes
    {
        get
        {
            Int16 startBytes = 0b0000000000000000;
            // order: (left to right bytes), A,B,X,Y,DU,DD,DL,DR,L1,R1, L-Joy engaged, R-joy engaged, unused, unused, unused, unused

            if (Input.GetButton("A"))
            {
                startBytes = (Int16)(0b1000000000000000 | startBytes);
            }
            if (Input.GetButton("B"))
            {
                startBytes = (Int16)(0b0100000000000000 | startBytes);
            }
            if (Input.GetButton("X"))
            {
                startBytes = (Int16)(0b0010000000000000 | startBytes);
            }
            if (Input.GetButton("Y"))
            {
                startBytes = (Int16)(0b0001000000000000 | startBytes);
            }
            if (_dPadUpPressed)
            {
                startBytes = (Int16)(0b0000100000000000 | startBytes);
            }
            if (_dPadDownPressed)
            {
                startBytes = (Int16)(0b0000010000000000 | startBytes);
            }
            if (_dPadLeftPressed)
            {
                startBytes = (Int16)(0b0000001000000000 | startBytes);
            }
            if (_dPadRightPressed)
            {
                startBytes = (Int16)(0b0000000100000000 | startBytes);
            }
            if (Input.GetButton("L1"))
            {
                startBytes = (Int16)(0b0000000010000000 | startBytes);
            }
            if (Input.GetButton("R1"))
            {
                startBytes = (Int16)(0b0000000001000000 | startBytes);
            }
            if ((new Vector2(LX, LY)).magnitude > 0.1f) // dead zone
            {
                startBytes = (Int16)(0b0000000000100000 | startBytes);
            }
            if ((new Vector2(RX, RY)).magnitude > 0.1f) // dead zone
            {
                startBytes = (Int16)(0b0000000000010000 | startBytes);
            }
            
            
            return startBytes;
        }
    }

    private void Start()
    {
        if (I != null)
        {
            Debug.LogError("cannot initialize another input manager");
        }

        I = this;
    }

    private float GetDeadzonedAxisJoystick(float input)
    {
        if (MathF.Abs(input) < DEADZONE_JOYSTICK_RADIUS)
        {
            return 0f;
        }

        return input;
    }
    
    private float GetDeadzonedAxisTrigger(float input)
    {
        if (MathF.Abs(input) < DEADZONE_TRIGGER_THRESHOLD)
        {
            return 0f;
        }

        return input;
    }
    
    
    private void Update()
    {
        float dpadY = Input.GetAxis("DPAD_Y");
        float dpadX = Input.GetAxis("DPAD_X");
        _dPadUpPressed = dpadY < 0f;
        _dPadDownPressed = dpadY > 0f;
        _dPadLeftPressed = dpadX < 0f;
        _dPadRightPressed = dpadX > 0f;

        _l1 = Input.GetButton("L1");
        _r1 = Input.GetButton("R1");
        
        _l2 = GetDeadzonedAxisTrigger(Input.GetAxis("L2"));
        _r2 = GetDeadzonedAxisTrigger(Input.GetAxis("R2"));
        
        _lx = GetDeadzonedAxisJoystick(Input.GetAxis("LX"));
        _rx = GetDeadzonedAxisJoystick(Input.GetAxis("RX"));
        _ly = GetDeadzonedAxisJoystick(Input.GetAxis("LY"));
        _ry = GetDeadzonedAxisJoystick(Input.GetAxis("RY"));

        _l3 = Input.GetKey("1");
        _l4 = Input.GetKey("2");
        _r3 = Input.GetKey("3");
        _r4 = Input.GetKey("4");

        if (Input.GetButton("M") && Input.GetButton("S"))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

}