using GSA;
using System;
using UnityEngine;

public class ControlMessage
{
    public byte leftJoystickX;
    public byte leftJoystickY;
    public byte rightJoystickX;
    public byte rightJoystickY;
    // In order, buttonStates1 is DPad Up, DPad Down, DPad Left, DPad Right, Face A, Face B, Face X, Face Y
    public byte buttonStates1;
    // In order, buttonStates2 is L1, L3, L4, L5, R1, R3, R4, R5
    public byte buttonStates2;
    public byte leftTriggerAnalog;
    public byte rightTriggerAnalog;
    public uint timestamp; // 32 bits

    /// <summary>
    /// Converts a ControlState to a ControlMessage.
    /// </summary>
    public static ControlMessage GetControlMessageFromState(ControlState sourceState)
    {
        var message = new ControlMessage
        {
            leftJoystickX = ConvertJoystickToByte(sourceState.LeftJoystick.x),
            leftJoystickY = ConvertJoystickToByte(sourceState.LeftJoystick.y),
            rightJoystickX = ConvertJoystickToByte(sourceState.RightJoystick.x),
            rightJoystickY = ConvertJoystickToByte(sourceState.RightJoystick.y),
            buttonStates1 = EncodeButtonStates1(sourceState),
            buttonStates2 = EncodeButtonStates2(sourceState),
            leftTriggerAnalog = ConvertTriggerToByte(sourceState.LeftTrigger),
            rightTriggerAnalog = ConvertTriggerToByte(sourceState.RightTrigger),
            timestamp = (uint)(sourceState.Timestamp & 0xFFFFFFFF)
        };

        return message;
    }

    private static byte ConvertJoystickToByte(float value)
    {
        value = Mathf.Clamp(value, -1.0f, 1.0f);
        return (byte)((value + 1.0f) * 127.5f); // Map -1.0 -> 0, 1.0 -> 255
    }
    
    private static byte ConvertTriggerToByte(float value)
    {
        value = Mathf.Clamp(value, 0.0f, 1.0f);
        return (byte)(value * 255.0f); // Map 0.0 -> 0, 1.0 -> 255
    }

    private static byte EncodeButtonStates1(ControlState state)
    {
        return (byte)(
            (state.DPadUp ? 1 << 0 : 0) |
            (state.DPadDown ? 1 << 1 : 0) |
            (state.DPadLeft ? 1 << 2 : 0) |
            (state.DPadRight ? 1 << 3 : 0) |
            (state.FaceA ? 1 << 4 : 0) |
            (state.FaceB ? 1 << 5 : 0) |
            (state.FaceX ? 1 << 6 : 0) |
            (state.FaceY ? 1 << 7 : 0)
        );
    }

    private static byte EncodeButtonStates2(ControlState state)
    {
        return (byte)(
            (state.L1Button ? 1 << 0 : 0) |
            (state.L3Button ? 1 << 1 : 0) |
            (state.L4Button ? 1 << 2 : 0) |
            (state.L5Button ? 1 << 3 : 0) |
            (state.R1Button ? 1 << 4 : 0) |
            (state.R3Button ? 1 << 5 : 0) |
            (state.R4Button ? 1 << 6 : 0) |
            (state.R5Button ? 1 << 7 : 0)
        );
    }

    /// <summary>
    /// Serialize this C# object into a 12-byte array (little-endian).
    /// </summary>
    public byte[] ToBytes()
    {
        byte[] data = new byte[12];
        data[0] = leftJoystickX;
        data[1] = leftJoystickY;
        data[2] = rightJoystickX;
        data[3] = rightJoystickY;
        data[4] = buttonStates1;
        data[5] = buttonStates2;
        data[6] = leftTriggerAnalog;
        data[7] = rightTriggerAnalog;

        // timestamp -> 4 bytes, little-endian
        data[8]  = (byte)(timestamp & 0xFF);
        data[9]  = (byte)((timestamp >> 8) & 0xFF);
        data[10] = (byte)((timestamp >> 16) & 0xFF);
        data[11] = (byte)((timestamp >> 24) & 0xFF);

        return data;
    }
}
