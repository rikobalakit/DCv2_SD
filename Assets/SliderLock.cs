using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class SliderLock : MonoBehaviour
{
    private bool _locked = true;

    [SerializeField] private bool _searchForSliders = true;
    [SerializeField] private GameObject _slidersParent;
    
    [SerializeField]
    private List<Slider> _sliders = new List<Slider>();
    
    void Start()
    {
        if (_searchForSliders)
        {
            _sliders = _slidersParent.GetComponentsInChildren<Slider>().ToList();
        }
        
        SetLock(true);
    }

    public void ToggleLock()
    {
        SetLock(!_locked);
    }

    private void SetLock(bool newLocked)
    {
        _locked = newLocked;
        
        foreach (var slider in _sliders)
        {
            if (slider != null)
            {
                slider.interactable = !_locked;
            }
        }
    }
}