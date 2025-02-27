using System;
using PearlSoft.Scripts.Runtime.ScreenUI.InputElements;
using PearlSoft.Scripts.Runtime.ScreenUI.OutputElements;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GSA
{
    public class NewGameManager : MonoBehaviour
    {
        
        [SerializeField]
        private CommsManager _commsManager;

        [SerializeField]
        private OwoManager _owoManager;

        [SerializeField]
        private InputManager _inputManager;
        
        [SerializeField]
        private MonospaceTextLogOutput ConsoleOutput;
    
        [SerializeField]
        private MonospaceTextLogStringInput consoleStringInput;
        
        public CommsManager CommsManager => _commsManager;
        public OwoManager OwoManager => _owoManager;
        
        public ControlState CurrentControlState;
        
        public TelemetryState CurrentTelemetryState;

        private DateTime _timeLoaded;
        
        private void Awake()
        {
            var config = new FBPPConfig()
            {
                SaveFileName = "settings.txt",
                AutoSaveData = true,
                ScrambleSaveData = false,
                EncryptionSecret = "my-secret",
                SaveFilePath = ""
            };
// pass it to FBPP
            FBPP.Start(config);
        
            ConsoleOutput.Initialize();
            
            CurrentTelemetryState = new TelemetryState();
            _inputManager.Initialize(this);
            _commsManager.Initialize(this);
            _owoManager.Initialize(this);
            _timeLoaded = DateTime.Now;
        }
        
        public void SetTelemetryStateFromMessage(TelemetryMessage sourceMessage)
        {
            TelemetryState.WriteFromTelemetryMessageToState(sourceMessage, CurrentTelemetryState);
        }

        private void Update()
        {
            UpdateRestart();
        }

        private void UpdateRestart()
        {
            if (CurrentControlState.SelectButton && CurrentControlState.StartButton)
            {
                if (_timeLoaded.AddSeconds(2) < DateTime.Now)
                {
                    Debug.Log("Reloading Scene");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
        
        public static long GetTimeSinceAppStartMilliseconds()
        {
            //use Time.time for seconds
            return (long) (Time.time * 1000);
        }
    }
}
