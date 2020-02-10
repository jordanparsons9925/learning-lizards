using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class wooperLearner : MonoBehaviour
{
    private IDictionary<string, int[,]> wooperBrain;
    private Rigidbody wooperBody;
    private string lastBehaviour;
    private string wooperBehaviour;
    private int[,] currentAction;
    private float actionTime;
    private float walkingSpeed;
    private int actionCounter;
    int visionDistance;
    bool hitDetected;
    Collider wooperCollider;
    RaycastHit rayHit;

    private void renderAction(int actionType, int direction) {
        switch (actionType) {
            case 0:
                wooperBody.velocity = transform.forward * walkingSpeed;
                break;
            case 1:
                wooperBody.velocity += transform.forward * walkingSpeed * (0 - 1);
                break;
            case 2:
                break;
        }
    }

    public void changeBehaviour(string collectedBehaviour) {
        wooperBehaviour = collectedBehaviour;
    }
    private int[,] makeNewBehaviour() {
        int[,] newBehaviour = new int[4, 2];
        newBehaviour[0, 0] = 0;
        newBehaviour[0, 1] = 0;
        newBehaviour[1, 0] = 0;
        newBehaviour[1, 1] = 0;
        newBehaviour[2, 0] = 0;
        newBehaviour[2, 1] = 0;
        newBehaviour[3, 0] = 0;
        newBehaviour[3, 1] = 0;
        return newBehaviour;
    }
    private int[,] makeNewBehaviourB() {
        int[,] newBehaviour = new int[4, 2];
        newBehaviour[0, 0] = 1;
        newBehaviour[0, 1] = 0;
        newBehaviour[1, 0] = 1;
        newBehaviour[1, 1] = 0;
        newBehaviour[2, 0] = 1;
        newBehaviour[2, 1] = 0;
        newBehaviour[3, 0] = 1;
        newBehaviour[3, 1] = 0;
        return newBehaviour;
    }

    // Start is called before the first frame update
    void Start()
    {
        wooperCollider = GetComponent<Collider>();
        hitDetected = false;
        walkingSpeed = 10.0f;
        visionDistance = 10;
        wooperBody = GetComponent<Rigidbody>();
        wooperBrain = new Dictionary<string, int[,]>();
        wooperBrain.Add("Nothing", makeNewBehaviour());
        wooperBehaviour = "Nothing";
        lastBehaviour = "Nothing";
        actionTime = 0.0f;
        actionCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rayScan();
        actionTime += Time.deltaTime;
        Debug.Log(wooperBehaviour);
        if  (!wooperBrain.Keys.Contains(wooperBehaviour)) {
            int[,] newBehaviour = new int[4, 2];
            int[,] cleanBehaviour = makeNewBehaviourB();
            for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 2; y++) {
                    newBehaviour[x, y] = cleanBehaviour[x, y];
                    Debug.Log(newBehaviour[x, y]);
                }
            }
            wooperBrain.Add(wooperBehaviour, newBehaviour);
        }
        if (actionTime >= 0.5f) {
            if (actionCounter > 3) {
                actionCounter = 0;
            }
            currentAction = wooperBrain[wooperBehaviour];
            renderAction(currentAction[actionCounter, 0], currentAction[actionCounter++, 1]);
            actionTime -= 0.5f;
        }
    }

    void rayScan() {
        string detectedObject = "Nothing";
        for (float rayRotateX = (-5.0f); rayRotateX <= 5.0f; rayRotateX += 0.5f) {
            for (float rayRotateY = (-10.0f); rayRotateY <= 10.0f; rayRotateY += 0.5f) {
                Quaternion q = Quaternion.AngleAxis(rayRotateY, Vector3.up);
                q = q * Quaternion.AngleAxis(rayRotateX, Vector3.left);
                Vector3 dist = transform.forward * visionDistance;
                Debug.DrawRay(transform.position, q * dist, Color.green);
                RaycastHit hit;
                Physics.Raycast(transform.position, q * dist, out hit, visionDistance);
                Collider hitObject = hit.collider;
                if (hitObject != null && hitObject.tag != "Ground")
                    wooperBehaviour = hitObject.tag;
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        visionDistance = 10;
        for (float rayRotateX = (-5.0f); rayRotateX <= 5.0f; rayRotateX += 10.0f) {
            for (float rayRotateY = (-10.0f); rayRotateY <= 10.0f; rayRotateY += 20.0f) {
                Quaternion q = Quaternion.AngleAxis(rayRotateY, Vector3.up);
                q = q * Quaternion.AngleAxis(rayRotateX, Vector3.left);
                Vector3 dist = transform.forward * visionDistance;
                Gizmos.DrawRay(transform.position, q * dist);
            }
        }
    }
}
