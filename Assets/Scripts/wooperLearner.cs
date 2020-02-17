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
    bool colliding;
    bool rotating;
    public bool fainted;
    RaycastHit rayHit;
    float wooperScore;
    float lastScore;
    float wooperLife;
    int familyMatch;
    bool parentTaken;

    private void wooperDeath() {
        fainted = true;
        wooperBody.velocity = new Vector3(0, 0, 0);
        do {
        } while (memoryScript.familyBrains[memoryScript.family].beingModified);
        if (wooperScore > memoryScript.familyBrains[memoryScript.family].scoreA) {
            memoryScript.familyBrains[memoryScript.family].beingModified = true;
            memoryScript.familyBrains[memoryScript.family].scoreA = wooperScore;
            memoryScript.familyBrains[memoryScript.family].childA.Clear();
            foreach (string geneBehaviour in wooperBrain.Keys) {
                memoryScript.familyBrains[memoryScript.family].childA.Add(geneBehaviour, new int[8,2]);
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 2; y++) {
                        memoryScript.familyBrains[memoryScript.family].childA[geneBehaviour][x, y]
                            = wooperBrain[geneBehaviour][x, y];
                    }
                }
            }
            memoryScript.familyBrains[memoryScript.family].beingModified = false;
        } else if (wooperScore > memoryScript.familyBrains[memoryScript.family].scoreB) {
            memoryScript.familyBrains[memoryScript.family].beingModified = true;
            memoryScript.familyBrains[memoryScript.family].scoreB = wooperScore;
            memoryScript.familyBrains[memoryScript.family].childB.Clear();
            foreach (string geneBehaviour in wooperBrain.Keys) {
                memoryScript.familyBrains[memoryScript.family].childB.Add(geneBehaviour, new int[8,2]);
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 2; y++) {
                        memoryScript.familyBrains[memoryScript.family].childB[geneBehaviour][x, y]
                            = wooperBrain[geneBehaviour][x, y];
                    }
                }
            }
            memoryScript.familyBrains[memoryScript.family].beingModified = false;
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
        int[,] newBehaviour = new int[8, 2];
        newBehaviour[0, 0] = 1;
        newBehaviour[0, 1] = 0;
        newBehaviour[1, 0] = 1;
        newBehaviour[1, 1] = 0;
        newBehaviour[2, 0] = 1;
        newBehaviour[2, 1] = 0;
        newBehaviour[3, 0] = 1;
        newBehaviour[3, 1] = 0;
        newBehaviour[4, 0] = 1;
        newBehaviour[4, 1] = 0;
        newBehaviour[5, 0] = 1;
        newBehaviour[5, 1] = 0;
        newBehaviour[6, 0] = 1;
        newBehaviour[6, 1] = 0;
        newBehaviour[7, 0] = 1;
        newBehaviour[7, 1] = 0;
        return newBehaviour;
    }
    
    // Inherits Genes from parents
    private void inheritGenes() {
        IDictionary<string, int[,]> parentA = memoryScript.familyBrains[memoryScript.family].parentA;
        if (memoryScript.familyMatch == -1) {
            do {
                familyMatch = Random.Range(0, 10);
                int pairedWith = memoryScript.familyBrains[familyMatch].pairedWith;
                int family = memoryScript.family;
            } while(memoryScript.familyBrains[familyMatch].pairedWith == memoryScript.family || familyMatch == memoryScript.family);
            
            memoryScript.familyMatch = familyMatch;
            memoryScript.familyBrains[familyMatch].pairedWith = memoryScript.family;
        }
        IDictionary<string, int[,]> parentB = memoryScript.familyBrains[memoryScript.familyMatch].parentB;

        foreach (string geneBehaviour in parentA.Keys) {
            wooperBrain.Add(geneBehaviour, new int[8, 2]);
            if (parentB.ContainsKey(geneBehaviour)) {
                if (Random.Range(0, 8) > 0) {
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
                wooperBrain.Add(geneBehaviour, new int[8, 2]);
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
            for (int i = 0; i < 1; i++) {
                int selectedAction = Random.Range(0, 8);
                if (Random.Range(0, 8) == 0)
                    wooperBrain[geneBehaviour][selectedAction, 0] = Random.Range(0, 4);
                int directionMutation = Random.Range(-5, 6);
                wooperBrain[geneBehaviour][selectedAction, 1] += directionMutation;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentRotation = 0.0f;
        rotating = false;
        colliding = false;
        fainted = false;
        walkingSpeed = 6;
        visionDistance = 7;
        wooperBody = GetComponent<Rigidbody>();
        wooperBrain = new Dictionary<string, int[,]>();
        inheritGenes();
        actionTime = 0.0f;
        actionCounter = 0;
        wooperScore = -1000f;
        lastScore = -1000f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fainted){
        rayScan();
        actionTime += Time.deltaTime;
        if (wooperScore <= lastScore + 0.5f) {
            wooperLife += Time.deltaTime;
        } else {
            wooperLife = 0.0f;
            lastScore = wooperScore;
        }
        if  (!wooperBrain.ContainsKey(wooperBehaviour)) {
            wooperBrain.Add(wooperBehaviour, new int[8,2]);
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

        if (transform.position.y <= -3.0f || transform.position.x > 12.45f || transform.position.x < -12.45f|| wooperLife >= 5.0f) {
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
        for (float rayRotateX = (-2.5f); rayRotateX <= 5.0f; rayRotateX += 0.5f) {
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
        visionDistance = 7;
        for (float rayRotateX = (-2.5f); rayRotateX <= 5.0f; rayRotateX += 5.0f) {
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
        
    }
    private void OnCollisionExit(Collision other) {
        colliding = false;
    }
}
