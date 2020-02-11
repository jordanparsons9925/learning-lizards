using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class wooperLearner : MonoBehaviour
{
    private IDictionary<string, int[,]> wooperBrain;
    private Rigidbody wooperBody;
    private string wooperBehaviour;
    private int[,] currentAction;
    private float actionTime;
    private int walkingSpeed;
    private int actionCounter;
    int visionDistance;
    float currentRotation;
    bool hitDetected;
    bool colliding;
    bool rotating;
    bool fainted;
    RaycastHit rayHit;
    float wooperScore;
    float lastScore;
    float wooperLife;

    private void wooperDeath() {
        fainted = true;
        wooperBody.velocity = new Vector3(0, 0, 0);
        if (wooperScore > memoryScript.familyBrains[memoryScript.family].scoreA) {
            memoryScript.familyBrains[memoryScript.family].childA.Clear();
            foreach (string geneBehaviour in wooperBrain.Keys) {
                memoryScript.familyBrains[memoryScript.family].childA.Add(geneBehaviour, new int[4,2]);
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 2; y++) {
                        memoryScript.familyBrains[memoryScript.family].childA[geneBehaviour][x, y]
                            = wooperBrain[geneBehaviour][x, y];
                    }
                }
            }
            memoryScript.familyBrains[memoryScript.family].scoreA = wooperScore;
        } else if (wooperScore > memoryScript.familyBrains[memoryScript.family].scoreB) {
            memoryScript.familyBrains[memoryScript.family].childB.Clear();
            foreach (string geneBehaviour in wooperBrain.Keys) {
                memoryScript.familyBrains[memoryScript.family].childB.Add(geneBehaviour, new int[4,2]);
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 2; y++) {
                        memoryScript.familyBrains[memoryScript.family].childB[geneBehaviour][x, y]
                            = wooperBrain[geneBehaviour][x, y];
                    }
                }
            }
            memoryScript.familyBrains[memoryScript.family].scoreB = wooperScore;
        }
        memoryScript.numDead++;
        memoryScript.checkDeaths();
    }
    // Renders a specified action to the wooper
    private void renderAction(int actionType, int direction) {
        if (!fainted) {
            switch (actionType) {
                case 0:
                    wooperBody.AddTorque(0, direction, 0);
                    break;
                case 1:
                    wooperBody.AddTorque(0, direction, 0);
                    if (colliding)
                        wooperBody.velocity = transform.forward * walkingSpeed;
                    break;
                case 2:
                    wooperBody.AddTorque(0, direction, 0);
                    if (colliding)
                        wooperBody.velocity = -transform.forward * walkingSpeed;
                    break;
                case 3:
                    wooperBody.AddTorque(0, direction, 0);
                    if (colliding)
                        wooperBody.AddForce(Vector3.up * 250);
                    break;
                case 4:
                    wooperDeath();
                    break;
            }
            if (direction > 0 || direction < 0)
                rotating = true;
        }
    }
    
    // Changes the behaviour to a new one
    public void changeBehaviour(string collectedBehaviour) {
        wooperBehaviour = collectedBehaviour;
    }
    
    // Makes a new, empty behaviour
    private int[,] makeNewBehaviour() {
        int[,] newBehaviour = new int[4, 2];
        newBehaviour[0, 0] = 1;
        newBehaviour[0, 1] = 0;
        newBehaviour[1, 0] = 0;
        newBehaviour[1, 1] = 0;
        newBehaviour[2, 0] = 0;
        newBehaviour[2, 1] = 0;
        newBehaviour[3, 0] = 0;
        newBehaviour[3, 1] = 0;
        return newBehaviour;
    }
    
    // Inherits Genes from parents
    private void inheritGenes() {
        IDictionary<string, int[,]> parentA = memoryScript.familyBrains[memoryScript.family].parentA;
        if (memoryScript.familyMatch == -1) {
            do {
                memoryScript.familyMatch = Random.Range(0, 10);
            } while(memoryScript.familyBrains[memoryScript.familyMatch].parentTaken 
            || memoryScript.familyMatch == memoryScript.family);
            memoryScript.familyBrains[memoryScript.familyMatch].parentTaken = true;
        }
        IDictionary<string, int[,]> parentB = memoryScript.familyBrains[memoryScript.familyMatch].parentB;

        foreach (string geneBehaviour in parentA.Keys) {
            wooperBrain.Add(geneBehaviour, new int[4, 2]);
            if (parentB.ContainsKey(geneBehaviour)) {
                if (Random.Range(0, 2) == 1) {
                    for (int x = 0; x < 4; x++) {
                        for (int y = 0; y < 2; y++) {
                            wooperBrain[geneBehaviour][x, y] = parentA[geneBehaviour][x, y];
                        }
                    }
                } else {
                    for (int x = 0; x < 4; x++) {
                        for (int y = 0; y < 2; y++) {
                            wooperBrain[geneBehaviour][x, y] = parentB[geneBehaviour][x, y];
                        }
                    }
                }
            } else {
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 2; y++) {
                        wooperBrain[geneBehaviour][x, y] = parentA[geneBehaviour][x, y];
                    }
                }
            }
        }

        foreach (string geneBehaviour in parentB.Keys) {
            if (!wooperBrain.ContainsKey(geneBehaviour)) {
                wooperBrain.Add(geneBehaviour, new int[4, 2]);
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 2; y++) {
                        wooperBrain[geneBehaviour][x, y] = parentB[geneBehaviour][x, y];
                    }
                }
            }
        }
        mutateGenes();
    }
    
    // Mutates the behaviours of a wooper
    private void mutateGenes() {
        foreach (string geneBehaviour in wooperBrain.Keys) {
            int selectedAction = Random.Range(0, 4);
            int actionType = Random.Range(0, 5);
            int directionMutation = Random.Range(-5, 6);

            wooperBrain[geneBehaviour][selectedAction, 0] = actionType;
            wooperBrain[geneBehaviour][selectedAction, 1] += directionMutation;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentRotation = 0.0f;
        rotating = false;
        colliding = false;
        hitDetected = false;
        fainted = false;
        walkingSpeed = 7;
        visionDistance = 10;
        wooperBody = GetComponent<Rigidbody>();
        wooperBrain = new Dictionary<string, int[,]>();
        inheritGenes();
        actionTime = 0.0f;
        actionCounter = 0;
        wooperScore = 0.0f;
        lastScore = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fainted){
        rayScan();
        actionTime += Time.deltaTime;
        if (wooperScore == lastScore) {
            wooperLife += Time.deltaTime;
        } else {
            lastScore = wooperScore;
            wooperLife = 0.0f;
        }
        if  (!wooperBrain.ContainsKey(wooperBehaviour)) {
            wooperBrain.Add(wooperBehaviour, new int[4,2]);
            int[,] cleanBehaviour = makeNewBehaviour();
            for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 2; y++) {
                    wooperBrain[wooperBehaviour][x, y] = cleanBehaviour[x, y];
                }
            }

        }
        if (actionTime >= 0.5f) {
            currentRotation = transform.localEulerAngles.y;
            rotating = false;
            if (actionCounter > 3) {
                actionCounter = 0;
            }
            currentAction = wooperBrain[wooperBehaviour];
            renderAction(currentAction[actionCounter, 0], currentAction[actionCounter++, 1]);
            actionTime -= 0.5f;
        }

        if (wooperLife >= 10.0f) {
            wooperDeath();
        }
        float wooperZ = transform.position.z;
        if (wooperZ > wooperScore)
            wooperScore = wooperZ;
        }
    }
    
    // Freezes all rotation except y
    protected void LateUpdate()
    {
        if (rotating)
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        else
            transform.localEulerAngles = new Vector3(0, currentRotation, 0);
    }
    
    // Scans the environment for an object to react to
    void rayScan() {
        string detectedObject = "Nothing";
        for (float rayRotateX = (-5.0f); rayRotateX <= 5.0f; rayRotateX += 0.5f) {
            for (float rayRotateY = (-10.0f); rayRotateY <= 10.0f; rayRotateY += 0.5f) {
                Quaternion q = Quaternion.AngleAxis(rayRotateY, Vector3.up);
                q = q * Quaternion.AngleAxis(rayRotateX, Vector3.left);
                Vector3 dist = transform.forward * visionDistance;
                RaycastHit hit;
                Physics.Raycast(transform.position, q * dist, out hit, visionDistance);
                Collider hitObject = hit.collider;
                if (hitObject != null && hitObject.tag != "Ground" && hitObject.tag != "Wooper")
                    detectedObject = hitObject.tag;
            }
        }
        wooperBehaviour = detectedObject;
    }
    
    // Draws the wooper's field of view in the editor for debugging
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
    
    // Limits jumping if not on ground
    private void OnCollisionEnter(Collision other) {
        colliding = true;
        if (other.collider.tag == "Death") {
            wooperDeath();
        }
        
    }
    private void OnCollisionExit(Collision other) {
        colliding = false;
    }
}
