using System;
using UnityEngine;

namespace GSA
{
    [Serializable]
    public class TelemetryState
    {
        public Quaternion OrientationQuaternion;

        public Vector3 OrientationEuler;

        public float BatteryVoltage;
        public int WeaponMotorRPM;    

        public float peakAccelX;
        public float peakAccelY;
        public float peakAccelZ;

        public float Heading;
        public float SmartArmoHealth1;
        public float SmartArmoHealth2;
        public float SmartArmoHealth3;
        public float SmartArmoHealth4;
        public ushort reserved0;
        public byte reserved1;
        public byte reserved2;
        public byte reserved3;
        public byte reserved4;

        public long timestamp;

        public static void WriteFromTelemetryMessageToState(TelemetryMessage sourceMessage, TelemetryState targetState)
        {
            // Convert orientation from compact representation to Quaternion
            targetState.OrientationQuaternion = new Quaternion(
                (sourceMessage.orientationX / 32767.5f) - 1.0f,
                (sourceMessage.orientationY / 32767.5f) - 1.0f,
                (sourceMessage.orientationZ / 32767.5f) - 1.0f,
                (sourceMessage.orientationW / 32767.5f) - 1.0f
            );
            
            targetState.OrientationEuler = targetState.OrientationQuaternion.eulerAngles;

            // Convert battery voltage from 0.1V precision (0-320 -> 0-32.0V)
            targetState.BatteryVoltage = sourceMessage.batteryVoltage / 1000.0f;

            // Convert weapon motor RPM directly
            targetState.WeaponMotorRPM = sourceMessage.weaponMotorRPM;

            // Convert peak acceleration back to float values (scaled by 5.0)
            targetState.peakAccelX = sourceMessage.peakAccelX / 5.0f;
            targetState.peakAccelY = sourceMessage.peakAccelY / 5.0f;
            targetState.peakAccelZ = sourceMessage.peakAccelZ / 5.0f;

            // Convert heading from 0-10000 scale (0-360 degrees)
            targetState.Heading = sourceMessage.heading / 100.0f;

            // Convert smart armor statuses to percentages (0-255 -> 0-100%)
            targetState.SmartArmoHealth1 = sourceMessage.smartArmor1 / 255.0f * 100.0f;
            targetState.SmartArmoHealth2 = sourceMessage.smartArmor2 / 255.0f * 100.0f;
            targetState.SmartArmoHealth3 = sourceMessage.smartArmor3 / 255.0f * 100.0f;
            targetState.SmartArmoHealth4 = sourceMessage.smartArmor4 / 255.0f * 100.0f;

            // Copy reserved values directly
            targetState.reserved0 = sourceMessage.reserved0;
            targetState.reserved1 = sourceMessage.reserved1;
            targetState.reserved2 = sourceMessage.reserved2;
            targetState.reserved3 = sourceMessage.reserved3;
            targetState.reserved4 = sourceMessage.reserved4;

            // Copy timestamp directly (convert to long for consistency if needed)
            targetState.timestamp = sourceMessage.timestamp;
        }

    }
}
