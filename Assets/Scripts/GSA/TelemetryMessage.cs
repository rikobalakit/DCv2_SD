using System;
using System.Collections.Generic;

public class TelemetryMessage
{
    public ushort orientationW;
    public ushort orientationX;
    public ushort orientationY;
    public ushort orientationZ;

    public ushort batteryVoltage;
    public int weaponMotorRPM;    

    public short peakAccelX;
    public short peakAccelY;
    public short peakAccelZ;

    public ushort heading;
    public byte smartArmor1;
    public byte smartArmor2;
    public byte smartArmor3;
    public byte smartArmor4;
    public ushort reserved0;
    public byte reserved1;
    public byte reserved2;
    public byte reserved3;
    public byte reserved4;

    public uint timestamp;

    // 32 bytes total

    public static TelemetryMessage FromBytes(byte[] data, int offset = 0)
    {
        TelemetryMessage msg = new TelemetryMessage();

        // Orientation (16-bit components)
        msg.orientationW = (ushort)(data[offset + 0] | (data[offset + 1] << 8));
        msg.orientationX = (ushort)(data[offset + 2] | (data[offset + 3] << 8));
        msg.orientationY = (ushort)(data[offset + 4] | (data[offset + 5] << 8));
        msg.orientationZ = (ushort)(data[offset + 6] | (data[offset + 7] << 8));

        // Battery voltage (16-bit)
        msg.batteryVoltage = (ushort)(data[offset + 8] | (data[offset + 9] << 8));

        // Heading (16-bit)
        msg.heading = (ushort)(data[offset + 10] | (data[offset + 11] << 8));

        // Weapon motor RPM (32-bit signed)
        msg.weaponMotorRPM = (int)(
            data[offset + 12]
            | (data[offset + 13] << 8)
            | (data[offset + 14] << 16)
            | (data[offset + 15] << 24)
        );

        // Peak acceleration (16-bit signed for x, y, z)
        msg.peakAccelX = (short)(data[offset + 16] | (data[offset + 17] << 8));
        msg.peakAccelY = (short)(data[offset + 18] | (data[offset + 19] << 8));
        msg.peakAccelZ = (short)(data[offset + 20] | (data[offset + 21] << 8));

        // Reserved 0 (16-bit)
        msg.reserved0 = (ushort)(data[offset + 22] | (data[offset + 23] << 8));

        // Smart armor status (8-bit each)
        msg.smartArmor1 = data[offset + 24];
        msg.smartArmor2 = data[offset + 25];
        msg.smartArmor3 = data[offset + 26];
        msg.smartArmor4 = data[offset + 27];

        // Reserved bytes
        msg.reserved1 = data[offset + 28];
        msg.reserved2 = data[offset + 29];
        msg.reserved3 = data[offset + 30];
        msg.reserved4 = data[offset + 31];

        // Timestamp (32-bit)
        msg.timestamp = (uint)(
            data[offset + 32]
            | (data[offset + 33] << 8)
            | (data[offset + 34] << 16)
            | (data[offset + 35] << 24)
        );

        return msg;
    }

    public static string FormatRawMessageWithPipes(byte[] rawMessage)
    {
        var hexParts = BitConverter.ToString(rawMessage).Split('-');
        var result = new List<string>();
        result.Add("Orien:");
        for (int i = 0; i < hexParts.Length; i++)
        {
            result.Add(hexParts[i]);

            // Insert pipes at the specified boundaries
            if (i == 7) // End of orientation (w, x, y, z)
            {
                result.Add("| Batt:");
            }
            if (i == 9) // End of battery voltage
            {
                result.Add("| Head:");
            }
            if (i == 11) // End of heading
            {
                result.Add("| WRPM:");
            }
            if (i == 15) // End of weaponMotorRPM
            {
                result.Add("| Accel:");
            }
            if (i == 21) // End of peak acceleration (x, y, z)
            {
                result.Add("| Reserved0:");
            }
            if (i == 23) // End of reserved0
            {
                result.Add("| Armor:");
            }
            if (i == 27) // End of smart armor and reserved fields
            {
                result.Add("| Timestamp:");
            }
        }

        return string.Join(" ", result);
    }

}
