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

            if ((new Vector2(RX, RY)).magnitude < 0.1f) // dead zone
            {
                return 1000;
            }

            if (Input.GetButton("Y"))
            {
                return 1001;
            }
            
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

    private void Start()
    {
        if (I != null)
        {
            Debug.LogError("cannot initialize another input manager");
        }

        I = this;
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