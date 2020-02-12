using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farTrack : MonoBehaviour
{
    private GameObject[] woopers;
    // Start is called before the first frame update
    void Start()
    {
        woopers = GameObject.FindGameObjectsWithTag("Wooper");
    }

    float getWooperZHigh() {
        float highest = -1000f;
        foreach(GameObject current in woopers) {
            if (!current.GetComponent<wooperLearner>().fainted && current.GetComponent<Transform>().position.z>highest)
                highest = current.GetComponent<Transform>().position.z;
        }
        if (highest == -1000f)
            return transform.position.z;
        else
            return highest;
    }

    float getWooperYHigh() {
        float highest = -1000f;
        foreach(GameObject current in woopers) {
            if (!current.GetComponent<wooperLearner>().fainted && current.GetComponent<Transform>().position.y>highest)
                highest = current.GetComponent<Transform>().position.y;
        }
        if (highest == -1000f)
            return transform.position.y;
        else
            return highest;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, getWooperYHigh(), getWooperZHigh());
    }
}
