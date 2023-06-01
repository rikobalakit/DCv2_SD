using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class Quit : MonoBehaviour
{

    private float _timeClickedDown;

    [SerializeField]
    private RectTransform _loadingBarRect;

    private const float _timeToHoldToQuit = 1f;

    private bool _isClicked = false;
    
    public void OnClickUp()
    {
        _isClicked = false;
        var elapsedTime = Time.time - _timeClickedDown;
        Debug.LogError($"Click up after {elapsedTime}");

        if (elapsedTime < _timeToHoldToQuit)
        {
            ReloadScene();
        }
        else
        {
            QuitApp();
        }
    }
    
    
    public void OnClickDown()
    {
        _isClicked = true;
        Debug.LogError("Click down");
        _timeClickedDown = Time.time;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitApp()
    {

        Debug.LogError("Quitting application");
        Application.Quit();

#if UNITY_EDITOR

        Debug.LogError("Quitting application IN EDITOR");
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }

    private void Update()
    {
        if (_isClicked)
        {
            var loadingBarRectRect = _loadingBarRect.rect;
            
            var elapsedTime = Time.time - _timeClickedDown;
            var inverseElapsedTimeFraction = 1f-(elapsedTime / _timeToHoldToQuit);

            if (elapsedTime > _timeToHoldToQuit)
            {
                QuitApp();
            }
            
            _loadingBarRect.anchoredPosition = new Vector2(-125f*inverseElapsedTimeFraction, 0f);
            _loadingBarRect.sizeDelta = new Vector2(-250f*inverseElapsedTimeFraction, 0f);
        }
        else
        {
            var loadingBarRectRect = _loadingBarRect.rect;
            _loadingBarRect.anchoredPosition = new Vector2(-125f, 0f);
            _loadingBarRect.sizeDelta = new Vector2(-250f, 0f);
        }

        
    }

}