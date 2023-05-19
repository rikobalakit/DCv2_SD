using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VOOnBigHit : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> _painSoundClips;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private bool _testModeEnabled = false;
    
    [SerializeField]
    private TelemetryValues _telemetryValues;
    
    private float _testModeLastTimeSoundPlayed = 0f;
    
    public void PlayPainSound()
    {
        if (_audioSource.isPlaying)
        {
            return;
        }
        
        var chosenClip = _painSoundClips[Random.Range(0, _painSoundClips.Count)];
        _audioSource.PlayOneShot(chosenClip);
    }

    private void Update()
    {
        if (_testModeEnabled)
        {
            if (Time.time > (_testModeLastTimeSoundPlayed + 3f))
            {
                _testModeLastTimeSoundPlayed = Time.time;
                PlayPainSound();
            }
        }
        else
        {
            if (_telemetryValues.Acceleration.magnitude > 25f)
            {
                PlayPainSound();
            }
        }
    }

}
