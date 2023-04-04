using System.IO;
using System.Text;
using UnityEngine;

public class ValueLogger : MonoBehaviour
{

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

            ;
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