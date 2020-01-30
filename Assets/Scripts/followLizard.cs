using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followLizard : MonoBehaviour
{
    public Transform lizardBody;
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
        trackerX = transform.position.x;
        trackerY = transform.position.y;
        trackerZ = lizardBody.position.z;
        transform.position = new Vector3(trackerX, trackerY, trackerZ);
        if (trackerZ > memoryScript.currentDistance) {
            memoryScript.currentDistance = trackerZ;
        }
    }
}
