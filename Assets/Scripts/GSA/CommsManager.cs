using GSA;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CommsManager : BaseManager
{
    [Header("Serial Port Settings")]
    public string portName = "COM3"; // Replace with your ESP32 port name
    public int baudRate = 115200;
    
    private SerialPort serialPort;
    private bool isHandshakeComplete = false;
    private bool isSpacePressed = false;
    
    [SerializeField] [Range(10, 200)]
    private float _controlSendRateHz = 50f; 
    private DateTime _lastControlSendTime;
    
    private bool _buttonPressed = false;
    
    private void Start()
    {
        serialPort = new SerialPort(portName, baudRate)
        {
            ReadTimeout = 500,
            WriteTimeout = 500
        };

        try
        {
            serialPort.Open();
            Debug.Log("Serial Port Opened");
            Handshake();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to open serial port: {ex.Message}");
        }
    }

    private void Handshake()
    {
        Debug.Log("Starting Handshake...");
        serialPort.WriteLine("HELLO_ESP32");

        while (!isHandshakeComplete)
        {
            try
            {
                string response = serialPort.ReadLine().Trim();
                if (response == "HELLO_UNITY")
                {
                    isHandshakeComplete = true;
                    Debug.Log("Handshake Complete!");
                }
            }
            catch (TimeoutException)
            {
                Debug.LogWarning("Handshake Timeout. Retrying...");
                serialPort.WriteLine("HELLO_ESP32");
            }
        }
    }

    private void Update()
    {
        if (!isHandshakeComplete || serialPort == null || !serialPort.IsOpen)
            return;

        // Send control data (if you want to do it every frame)
        if ((DateTime.Now - _lastControlSendTime).TotalMilliseconds >= 1000f / _controlSendRateHz)
        {
            _gameManager.CurrentControlState.Timestamp = NewGameManager.GetTimeSinceAppStartMilliseconds();
            SerialProtocol.SendControlMessage(serialPort, ControlMessage.GetControlMessageFromState(_gameManager.CurrentControlState));
            Debug.Log("Sending control message");
        }
        
        // Now see if we got a Telemetry packet (or anything)
        TryReadAllAvailablePackets();
    }

    private void TryReadAllAvailablePackets()
    {
        // This is a naive loop that tries reading as long as data is available
        while (serialPort.BytesToRead >= 3) // we have at least a header
        {
            if (SerialProtocol.TryReadPacket(serialPort, out SerialProtocol.MsgType type, out byte[] payload))
            {
                switch (type)
                {
                    case SerialProtocol.MsgType.Telemetry:
                        TelemetryMessage tm = TelemetryMessage.FromBytes(payload);
                        HandleTelemetry(tm, payload);
                        break;
                }
            }
            else
            {
                // Not enough data or error reading => break or continue
                break;
            }
        }
    }
    
    private void HandleTelemetry(TelemetryMessage telem, byte[] rawMessage)
    {
        // Log the raw serial message as a hex string
        _gameManager.SetTelemetryStateFromMessage(telem);

        var debugData = false;
        if (debugData)
        {
            string telemetryLog = 
                $"Telemetry Data: " +
                $"Orientation(w={telem.orientationW}, x={telem.orientationX}, y={telem.orientationY}, z={telem.orientationZ}) | " +
                $"Battery Voltage={telem.batteryVoltage} | " +
                $"Weapon Motor RPM={telem.weaponMotorRPM} | " +
                $"Peak Acceleration(x={telem.peakAccelX}, y={telem.peakAccelY}, z={telem.peakAccelZ}) | " +
                $"Heading={telem.heading} | " +
                $"Smart Armor(smartArmor1={telem.smartArmor1}, smartArmor2={telem.smartArmor2}, " +
                $"smartArmor3={telem.smartArmor3}, smartArmor4={telem.smartArmor4}) | " +
                $"Reserved(reserved1={telem.reserved1}, reserved2={telem.reserved2}, reserved3={telem.reserved3}, reserved4={telem.reserved4}) | " +
                $"Timestamp={telem.timestamp}";

            // Log the combined string
            Debug.Log(telemetryLog);
        }
        // Combine all telemetry data into a single log statement
        
    }
    
    private void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Serial Port Closed");
        }
    }
}
