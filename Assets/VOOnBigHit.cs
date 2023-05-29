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

    private int _framesHighImpact = 0;
    
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
            //Debug.LogError($"_telemetryValues.Acceleration.magnitude: {_telemetryValues.BnoAcceleration.magnitude}");
            if (_telemetryValues.BnoAcceleration.magnitude > 25f)
            {
                _framesHighImpact++;
            }
            else
            {
                _framesHighImpact = 0;
            }

            if (_framesHighImpact > 2)
            {
                PlayPainSound();
                _framesHighImpact = 0;
            }
        }
    }

}
