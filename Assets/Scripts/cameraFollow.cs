using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform farTracker;

    private int cameraShot;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(farTracker);

        if (cameraShot == 0) {
            transform.position = new Vector3(-83.4f, 28.7f, 39.1f);
            Debug.Log(transform.rotation.y);
            if (transform.rotation.y < 0.45f)
                cameraShot++;
        } else if (cameraShot == 1) {
            transform.position = new Vector3(-70, 20, 287.4f);
        }
    }
}