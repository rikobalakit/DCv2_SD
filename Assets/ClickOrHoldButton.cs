using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickOrHoldButton : MonoBehaviour
{
    private float _timeClickedDown;

    [SerializeField]
    private RectTransform _loadingBarRect;

    [SerializeField] private RectTransform _buttonRect;

    private const float _timeToHoldToQuit = 1f;

    private bool _isClicked = false;

    [SerializeField] public UnityEvent OnShortPressFinished = new UnityEvent();
    [SerializeField] public UnityEvent OnLongPressFinished = new UnityEvent();

    
    public void OnClickUp()
    {
        _isClicked = false;
        var elapsedTime = Time.time - _timeClickedDown;


        if (elapsedTime < _timeToHoldToQuit)
        {
            if (OnShortPressFinished != null)
            {
                OnShortPressFinished.Invoke();
            }
        }
        else
        {
            /*
            if (OnLongPressFinished != null)
            {
                OnLongPressFinished.Invoke();
            }
            */
        }
    }
    
    
    public void OnClickDown()
    {
        _isClicked = true;

        _timeClickedDown = Time.time;
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
                if (OnLongPressFinished != null)
                {
                    OnLongPressFinished.Invoke();
                    
                    _isClicked = false;
                }
            }
            
            _loadingBarRect.anchoredPosition = new Vector2(-(_buttonRect.sizeDelta.x/2f)*inverseElapsedTimeFraction, 0f);
            _loadingBarRect.sizeDelta = new Vector2(-(_buttonRect.sizeDelta.x)*inverseElapsedTimeFraction, 0f);
        }
        else
        {
            var loadingBarRectRect = _loadingBarRect.rect;
            _loadingBarRect.anchoredPosition = new Vector2(-(_buttonRect.sizeDelta.x/2f), 0f);
            _loadingBarRect.sizeDelta = new Vector2(-(_buttonRect.sizeDelta.x), 0f);
        }

        
    }
}
