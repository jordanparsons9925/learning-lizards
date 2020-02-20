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
        transform.position = memoryScript.freeCamPosition;
        transform.rotation = memoryScript.freeCamRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!memoryScript.freeCam) {
            transform.LookAt(farTracker);

            if (cameraShot == 0) {
                transform.position = new Vector3(-83.4f, 28.7f, 39.1f);
                //Debug.Log(transform.rotation.y);
                if (transform.rotation.y < 0.45f)
                    cameraShot++;
            } else if (cameraShot == 1) {
                transform.position = new Vector3(-70, 20, 287.4f);
            }
        } else {
            memoryScript.freeCamPosition = transform.position;
            memoryScript.freeCamRotation = transform.rotation;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                transform.position += transform.forward;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                transform.position += -transform.right;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                transform.position += -transform.forward;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                transform.position += transform.right;
            }
            if (Input.GetMouseButton(1))
                transform.localRotation *= Quaternion.AngleAxis (Input.GetAxis("Mouse X"), Vector3.up) * Quaternion.AngleAxis (Input.GetAxis("Mouse Y"), -Vector3.right) * Quaternion.AngleAxis (0, Vector3.forward);
        }
    }
}