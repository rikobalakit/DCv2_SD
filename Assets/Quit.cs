using UnityEngine;

public class Quit : MonoBehaviour
{

    public void QuitApp()
    {

        Debug.LogError("Quitting application");
        Application.Quit();

#if UNITY_EDITOR

        Debug.LogError("Quitting application IN EDITOR");
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }

}