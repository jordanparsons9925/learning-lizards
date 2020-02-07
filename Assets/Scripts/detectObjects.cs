using System.Collections;
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f)) {

        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Ground" && other.gameObject.tag != "Lizard" && other.gameObject.tag != "Sight") {
            Debug.Log("Collision Entered");
            wooper.GetComponent<wooperLearner>().changeBehaviour(other.gameObject.tag);
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Ground" && other.gameObject.tag != "Lizard" && other.gameObject.tag != "Sight") {
            Debug.Log("Left Collision");
            wooper.GetComponent<wooperLearner>().changeBehaviour("Nothing");
        }
    }
}
