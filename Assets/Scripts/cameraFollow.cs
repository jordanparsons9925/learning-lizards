﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform lizardTracker;
    float trackerX;
    float trackerY;
    float trackerZ;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        trackerX = lizardTracker.position.x + 11.0200672f;
        trackerY = lizardTracker.position.y + 3.473f;
        trackerZ = lizardTracker.position.z - 21.521f;
        transform.position = new Vector3(trackerX, trackerY, trackerZ);
    }
}