using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        memoryScript.footTraffic = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > memoryScript.currentDistance) {
            memoryScript.currentDistance = transform.position.x;
        }
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.name == "ground")
            memoryScript.footTraffic++;
    }
}
