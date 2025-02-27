using System.Collections;
using System.Collections.Generic;
using PearlSoft.Scripts.Runtime.ScreenUI.InputElements;
using PearlSoft.Scripts.Runtime.ScreenUI.OutputElements;
using UnityEngine;

public class OldGameManager : MonoBehaviour
{

    [SerializeField]
    private MonospaceTextLogOutput ConsoleOutput;
    
    [SerializeField]
    private MonospaceTextLogStringInput consoleStringInput;
    // Start is called before the first frame update
    void Start()
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
    }

}
