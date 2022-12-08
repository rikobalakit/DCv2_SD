using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatController : MonoBehaviour
{

    public int HeartbeatTime => (int) (Time.time * 1000f);

    public static HeartbeatController I;

    private void Start()
    {
        if (I != null)
        {
            Debug.LogError("More than one HeartbeatController, this is bad");
            return;
        }

        I = this;
    }

}