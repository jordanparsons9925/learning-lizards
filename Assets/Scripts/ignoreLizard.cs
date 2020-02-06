using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreLizard : MonoBehaviour
{
    Rigidbody selfBody;
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
        if (other.gameObject.tag == "Lizard") {
        Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }
    }
}
