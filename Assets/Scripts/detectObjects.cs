﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectObjects : MonoBehaviour
{
    Rigidbody selfBody;
    public GameObject wooper;
    // Start is called before the first frame update
    void Start()
    {
        selfBody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Ground" && other.gameObject.tag != "Lizard" && other.gameObject.tag != "Sight")
            wooper.GetComponent<wooperLearner>().changeBehaviour(other.gameObject.tag);
    }
}
