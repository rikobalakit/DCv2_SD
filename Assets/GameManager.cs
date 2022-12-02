using System.Collections;
using System.Collections.Generic;
using PearlSoft.Scripts.Runtime.ScreenUI.InputElements;
using PearlSoft.Scripts.Runtime.ScreenUI.OutputElements;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private MonospaceTextLogOutput ConsoleOutput;
    
    [SerializeField]
    private MonospaceTextLogStringInput consoleStringInput;
    // Start is called before the first frame update
    void Start()
    {
        ConsoleOutput.Initialize();
        
        ConsoleOutput.LogText("peee poooo i234 njfsd pee pooopooo");
    }

}
