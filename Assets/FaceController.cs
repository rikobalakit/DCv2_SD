using System;
using System.Collections;
using System.Collections.Generic;
using GSA;
using UnityEngine;
using UnityEngine.UI;

public class FaceController : MonoBehaviour
{

    [SerializeField]
    private RawImage _avatarSpace;

    [SerializeField]
    private Texture _startingDefaultFace;

    [SerializeField]
    private List<Face> _facesList;

    private Texture _currentDefaultFace;

    [SerializeField]
    private bool _testCycleAllFaces = false;
    
    private void Start()
    {
        _avatarSpace.texture = _startingDefaultFace;

        if (_testCycleAllFaces)
        {
            StartCoroutine(CycleAllFaces());
        }
        
    }

    private void UpdateFaceFromStatus()
    {
        if (VOOnBigHit.I != null && VOOnBigHit.I.RecentBigHit)
        {
            SetFaceByIdentifier("Surprised");
            return;
        }
        
        if (BatteryWarningDisplay.I != null)
        {
            switch (BatteryWarningDisplay.I.CurrentBatteryStatus)
            {
                case BatteryWarningDisplay.BatteryStatus.USB:
                    break;
                case BatteryWarningDisplay.BatteryStatus.Dead:
                    SetFaceByIdentifier("Tired");
                    return;
                case BatteryWarningDisplay.BatteryStatus.Critical:
                    SetFaceByIdentifier("Sad");
                    return;
                case BatteryWarningDisplay.BatteryStatus.Low:
                    break;
                case BatteryWarningDisplay.BatteryStatus.High:
                    break;
                case BatteryWarningDisplay.BatteryStatus.Full:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (InputManager.I != null)
        {
            if (InputManager.I.R2 > 0.75f)
            {
                SetFaceByIdentifier("Fighting2");
                return;
            }
            if (InputManager.I.R2 > 0.5f)
            {
                SetFaceByIdentifier("Fighting");
                return;
            }
            if (InputManager.I.R2 > 0.25f)
            {
                SetFaceByIdentifier("Angry");
                return;
            }
        }

        if (BatteryWarningDisplay.I != null &&
            BatteryWarningDisplay.I.CurrentBatteryStatus == BatteryWarningDisplay.BatteryStatus.Low)
        {
            SetFaceByIdentifier("Neutral");
            return;
        }
        //Default face
        SetFaceByIdentifier("Smug");
    }
    private void Update()
    {
        if (_testCycleAllFaces)
        {
            return;
        }

        UpdateFaceFromStatus();
        
    }
    
    private IEnumerator CycleAllFaces()
    {
        var currentIndex = 0;
        while (true)
        {
            var currentIdentifier = _facesList[currentIndex].FaceName;
            SetFaceByIdentifier(currentIdentifier);
            
            currentIndex++;

            if (currentIndex >= _facesList.Count)
            {
                currentIndex = 0;
            }

            var bpm = 109f;
            
            yield return new WaitForSeconds(60/bpm);
        }
    }

    private void SetFaceByIdentifier(String identifier)
    {
        _avatarSpace.texture = GetFaceTexture(identifier);
    }

    private Texture GetFaceTexture(String identifier)
    {
        foreach (var face in _facesList)
        {
            if (face.FaceName == identifier)
            {
                return face.FaceTexture;
            }
        }
        
        Debug.LogError($"Couldn't find face {identifier}, returning current default");
        return _currentDefaultFace;
    }
    
    [Serializable]
    public class Face
    {

        public string FaceName;
        public Texture FaceTexture;


    }

}
