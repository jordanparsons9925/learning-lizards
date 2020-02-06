using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sightFollow : MonoBehaviour
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
        trackerX = lizardTracker.position.x;
        trackerY = lizardTracker.position.y;
        trackerZ = lizardTracker.position.z + 1.97f;
        transform.position = new Vector3(trackerX, trackerY, trackerZ);
    }
}
