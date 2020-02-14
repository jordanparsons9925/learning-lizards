using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sweepSweep : MonoBehaviour
{
    [Tooltip("The speed that the Sweeper moves at.")]
    public int sweeperSpeed;

    [Tooltip("The direction the sweeper animates.")]
    public bool sweepDirection;
    Rigidbody sweeper;
    float directionalTime;

    // Start is called before the first frame update
    void Start()
    {
        sweeper = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        directionalTime += Time.deltaTime;

        if (directionalTime >= 1.0f) {
            if (sweepDirection) {
                sweeper.AddForce(new Vector3(sweeperSpeed, 0, 0));
                sweeper.AddForce(new Vector3(sweeperSpeed, 0, 0));
            }
                
            else {
                sweeper.AddForce(new Vector3(-sweeperSpeed, 0, 0));
                sweeper.AddForce(new Vector3(sweeperSpeed, 0, 0));
            }
            
            directionalTime -= 1.0f;
            sweepDirection = !sweepDirection;
        }
    }
}
