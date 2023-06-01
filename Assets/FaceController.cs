using System;
using System.Collections;
using System.Collections.Generic;
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
