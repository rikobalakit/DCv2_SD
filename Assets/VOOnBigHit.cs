using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VOOnBigHit : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _painSoundClips;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private bool _testModeEnabled = false;

    [SerializeField] private TelemetryValues _telemetryValues;

    [SerializeField] private OWOController _owoController; // Serialized OWOController

    private float _testModeLastTimeSoundPlayed = 0f;

    private int _framesHighImpact = 0;

    private static VOOnBigHit s_instance;
    public static VOOnBigHit I => s_instance;

    private float _lastTimeBigImpact = 0;

    public bool RecentBigHit
    {
        get
        {
            return Time.time - _lastTimeBigImpact < 1f;
        }
    }

    private void Start()
    {
        if (s_instance != null)
        {
            Debug.LogError("why is there more than one of these you dumb fuck");
        }
        else
        {
            s_instance = this;
        }
    }

    public void PlayPainSound()
    {
        if (_audioSource.isPlaying)
        {
            return;
        }

        var chosenClip = _painSoundClips[Random.Range(0, _painSoundClips.Count)];
        _audioSource.PlayOneShot(chosenClip);
        _lastTimeBigImpact = Time.time;

        // Trigger OWO Ball sensation
        if (_owoController != null)
        {
            _owoController.TriggerBallSensation();
        }
        else
        {
            Debug.LogWarning("OWOController is not assigned!");
        }
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
            if (_telemetryValues.BnoAcceleration.magnitude > 15f)
            {
                _framesHighImpact++;
            }
            else
            {
                _framesHighImpact = 0;
            }

            if (_framesHighImpact > 1)
            {
                PlayPainSound();
                _framesHighImpact = 0;
            }
        }
    }
}
