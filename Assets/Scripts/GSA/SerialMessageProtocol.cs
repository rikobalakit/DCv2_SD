using System;
using System.IO.Ports;
using UnityEngine;

/// <summary>
/// Minimal packet protocol for reading/writing the custom messages.
/// </summary>
public static class SerialProtocol
{
    public enum MsgType : byte
    {
        Control   = 0x01, // 12-byte payload
        Telemetry = 0x02  // 32-byte payload
    }

    /// <summary>
    /// Sends a ControlMessage as a packet: [type=0x01] [length=12] [payload=12 bytes]
    /// </summary>
    public static void SendControlMessage(SerialPort port, ControlMessage msg)
    {
        byte[] payload = msg.ToBytes(); // 12 bytes
        SendPacket(port, MsgType.Control, payload);
    }
    
    private static void SendPacket(SerialPort port, MsgType type, byte[] payload)
    {
        int length = payload.Length;  // 12 or 32
        byte[] packet = new byte[1 + 2 + length];
        packet[0] = (byte)type;
        packet[1] = (byte)(length & 0xFF);
        packet[2] = (byte)((length >> 8) & 0xFF);
        Buffer.BlockCopy(payload, 0, packet, 3, length);

        // Write the entire packet in one go
        port.Write(packet, 0, packet.Length);
    }

    /// <summary>
    /// Try to read a packet from the serial port: returns true if a full packet was read.
    /// This is a blocking example, which might block if data isn't ready.
    /// </summary>
    public static bool TryReadPacket(SerialPort port, out MsgType msgType, out byte[] payload)
    {
        msgType = 0;
        payload = null;
        
        // We expect at least 3 bytes for header
        byte[] header = new byte[3];
        int readCount = port.Read(header, 0, 3); // This may block if not enough data
        if (readCount < 3)
        {
            Debug.Log("Not enough data for header");
            return false;
        }

        msgType = (MsgType)header[0];
        int length = header[1] | (header[2] << 8);

        // Now read the payload
        payload = new byte[length];
        readCount = port.Read(payload, 0, length);
        if (readCount < length)
        {
            return false;
        }

        return true;
    }
}
