using System;
using UnityEngine;

namespace GSA
{
    [Serializable]
    public class ControlState
    {
        public Vector2 LeftJoystick;
        public Vector2 RightJoystick;
        //These are buttonStates1
        public bool DPadUp;
        public bool DPadDown;
        public bool DPadLeft;
        public bool DPadRight;
        public bool FaceA;
        public bool FaceB;
        public bool FaceX;
        public bool FaceY;
        //these are ButtonStates2
        public bool L1Button;
        public bool L3Button; //Left Joystick Button
        public bool L4Button; //Left upper paddle
        public bool L5Button; //Left lower paddle
        public bool R1Button;
        public bool R3Button; //Right Joystick Button
        public bool R4Button; //Right upper paddle
        public bool R5Button; //Right lower paddle
        
        public float LeftTrigger;
        public float RightTrigger;

        public long Timestamp;

        public bool StartButton;
        public bool SelectButton;
    }
}
