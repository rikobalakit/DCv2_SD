using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> objectsToToggle = new List<GameObject>();

    [SerializeField]
    private List<GameObject> objectsToUntoggle = new List<GameObject>();

    private bool CurrentState => objectsToToggle[0].activeSelf;

    private void Awake()
    {
        var currentState = CurrentState;

        foreach (var gameObjectToToggle in objectsToToggle)
        {
            gameObjectToToggle.SetActive(currentState);
        }
        
        foreach (var gameObjectToUntoggle in objectsToUntoggle)
        {
            gameObjectToUntoggle.SetActive(!currentState);
        }
    }

    public void Toggle()
    {
        var newState = !CurrentState;

        foreach (var gameObjectToToggle in objectsToToggle)
        {
            gameObjectToToggle.SetActive(newState);
        }

        foreach (var gameObjectToUntoggle in objectsToUntoggle)
        {
            gameObjectToUntoggle.SetActive(!newState);
        }
    }

}