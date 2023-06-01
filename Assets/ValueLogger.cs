using System.IO;
using System.Text;
using PearlSoft.Scripts.Runtime.ScreenUI.OutputElements;
using UnityEngine;

public class ValueLogger : MonoBehaviour
{

    [SerializeField]
    private MonospaceTextLogOutput _console;

    [SerializeField]
    private BluetoothConsole _bluetoothController;
    
    private bool isInitialized = false;

    private StringBuilder logLineStringBuilder = new StringBuilder();

    public string GetCSVLine(bool isInitialLine = false, bool logDrive = false)
    {
        logLineStringBuilder.Clear();

        // timestamps

        if (isInitialLine)
        {
            logLineStringBuilder.Append("TimeSinceAppStart,");
        }
        else
        {
            logLineStringBuilder.Append($"{Time.time},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("BatteryVoltage,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.BatteryVoltage},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("WeaponThrottle,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.WeaponThrottle},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("OrientationX,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.OrientationEuler.x},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("OrientationY,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.OrientationEuler.y},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("OrientationZ,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.OrientationEuler.z},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("BnoAccelerationX,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.BnoAcceleration.x},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("BnoAccelerationY,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.BnoAcceleration.y},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("BnoAccelerationZ,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.BnoAcceleration.z},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("LisAccelerationX,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.LisAcceleration.x},");
        }
        
                
        if (isInitialLine)
        {
            logLineStringBuilder.Append("LisAccelerationY,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.LisAcceleration.y},");
        }
        
                
        if (isInitialLine)
        {
            logLineStringBuilder.Append("LisAccelerationZ,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.LisAcceleration.z},");
        }

                
        if (isInitialLine)
        {
            logLineStringBuilder.Append("FusionAccelerationX,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.Acceleration.x},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("FusionAccelerationY,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.Acceleration.y},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("FusionAccelerationZ,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.Acceleration.z},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("EscTemperature,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.EscTemperature},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("EscVoltage,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.EscVoltage},");
        }

        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("EscCurrent,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.EscCurrent},");
        }

        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("EscUsedMah,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.EscUsedMah},");
        }

        if (isInitialLine)
        {
            logLineStringBuilder.Append("EscRpm,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.EscRpm},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("BnoTemp,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.BnoTemp},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("BnoCalibrationSystem,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.BnoCalibrationSystem},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("BnoCalibrationGyro,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.BnoCalibrationGyro},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("BnoCalibrationAccelerometer,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.BnoCalibrationAccelerometer},");
        }
        
        if (isInitialLine)
        {
            logLineStringBuilder.Append("BnoCalibrationMagnetometer,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.BnoCalibrationMagnetometer},");
        }

        if (isInitialLine)
        {
            logLineStringBuilder.Append("SmartHeadingOffset,");
        }
        else
        {
            logLineStringBuilder.Append($"{TelemetryValues.I.SmartHeadingOffset},");
        }
        
        return logLineStringBuilder.ToString();
    }

    void SetupLogFile()
    {
        int logNumber = 0;
        bool logExists = false;
        string dataLogFileName = $"log_app_{logNumber}.csv";

        do
        {
            dataLogFileName = $"log_app_{logNumber}.csv";

            if (File.Exists(dataLogFileName))
            {
                logExists = true;
                logNumber++;
            }
            else
            {
                logExists = false;
            }
        } while (logExists);

        TelemetryValues.I.PcLogFileName = dataLogFileName;

        using (var writer = new StreamWriter(File.Create(TelemetryValues.I.PcLogFileName)))
            ;

        _console.LogText(TelemetryValues.I.PcLogFileName);
        
        isInitialized = true;
    }

    void AppendToLogAsLine(string logToAppend)
    {
        if (File.Exists(TelemetryValues.I.PcLogFileName))
        {
            using (StreamWriter w = File.AppendText(TelemetryValues.I.PcLogFileName))
            {
                w.WriteLine(logToAppend);
                TelemetryValues.I.PcLogLinesLength++;
            }
        }
        else
        {
            Debug.LogError("Can't log to file because it doesnt exist");
        }
    }

    private void Update()
    {
        if (TelemetryValues.I == null)
        {
            return;
        }

        if (!_bluetoothController.IsConnected)
        {
            return;
        }

        if (!isInitialized)
        {
            SetupLogFile();
            AppendToLogAsLine(GetCSVLine(true));
        }

        if (isInitialized)
        {
            AppendToLogAsLine(GetCSVLine());
            
            /*
            if (TelemetryValues.I.SomethingChangedSinceLastLog)
            {
                
                TelemetryValues.I.SomethingChangedSinceLastLog = false;
            }
            */
        }
    }

}