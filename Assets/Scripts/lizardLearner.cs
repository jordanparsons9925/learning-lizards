using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lizardLearner : MonoBehaviour
{
    public Text childDisplay;
    public Rigidbody lizardBody;

    //body parts on lizard
    public HingeJoint thigh1;
    public Rigidbody thigh1Body;
    public JointMotor thigh1Motor;
    public HingeJoint thigh2;
    public Rigidbody thigh2Body;
    public JointMotor thigh2Motor;
    public HingeJoint thigh3;
    public Rigidbody thigh3Body;
    public JointMotor thigh3Motor;
    public HingeJoint thigh4;
    public Rigidbody thigh4Body;
    public JointMotor thigh4Motor;
    public HingeJoint ankle1;
    public Rigidbody ankle1Body;
    public JointMotor ankle1Motor;
    public HingeJoint ankle2;
    public Rigidbody ankle2Body;
    public JointMotor ankle2Motor;
    public HingeJoint ankle3;
    public Rigidbody ankle3Body;
    public JointMotor ankle3Motor;
    public HingeJoint ankle4;
    public Rigidbody ankle4Body;
    public JointMotor ankle4Motor;

    //variables for setting up the lizards
    static public float lifeSpan = 300f; //Lifespan of lizard in seconds
    private float changeTime = 0.0f;
    private float fruitTime = 0.0f;
    private float lifeTime = 0.0f;
    private int childrenPerGen;
    private bool chunkMutated;
    static public int numChunks = (int) lifeSpan * 2; //The second patterns
    static public int numBlocks = 8; //The parts of the body moved per chunk
    
    //data for lizard brains
    private int[,] lizardBrain;
    private int currentChunk;
    private int groundCounter;
    public float newTimeDistance;
    public float lizardScore;

    public void setTime (float newTime) {
        Time.timeScale = newTime;
    }

    void renderNextActionA () {
        //assigns the neurons to the associated motor angle and force
        thigh1Motor.targetVelocity = lizardBrain[currentChunk, 0] - ((int) thigh1.angle);
        thigh4Motor.targetVelocity = lizardBrain[currentChunk, 3] - ((int) thigh4.angle);
        ankle1Motor.targetVelocity = lizardBrain[currentChunk, 4] - ((int) ankle1.angle);
        ankle4Motor.targetVelocity = lizardBrain[currentChunk, 7] - ((int) ankle4.angle);

        thigh1.motor = thigh1Motor;
        thigh4.motor = thigh4Motor;

        ankle1.motor = ankle1Motor;
        ankle4.motor = ankle4Motor;

        //activates the motors
        lizardBody.AddForce(Vector3.zero);
        thigh1Body.AddForce(Vector3.zero);
        thigh4Body.AddForce(Vector3.zero);
        ankle1Body.AddForce(Vector3.zero);
        ankle4Body.AddForce(Vector3.zero);
    }
    void renderNextActionB () {
        //assigns the neurons to the associated motor angle and force
        thigh2Motor.targetVelocity = lizardBrain[currentChunk, 1] - ((int) thigh2.angle);
        Debug.Log((int) thigh1.angle);
        thigh3Motor.targetVelocity = lizardBrain[currentChunk, 2] - ((int) thigh3.angle);
        ankle2Motor.targetVelocity = lizardBrain[currentChunk, 5] - ((int) ankle2.angle);
        ankle3Motor.targetVelocity = lizardBrain[currentChunk, 6] - ((int) ankle3.angle);

        thigh2.motor = thigh2Motor;
        thigh3.motor = thigh3Motor;

        ankle2.motor = ankle2Motor;
        ankle3.motor = ankle3Motor;

        //activates the motors
        lizardBody.AddForce(Vector3.zero);
        thigh2Body.AddForce(Vector3.zero);
        thigh3Body.AddForce(Vector3.zero);
        ankle2Body.AddForce(Vector3.zero);
        ankle3Body.AddForce(Vector3.zero);
    }

    void nextChild() {
        if (memoryScript.Child++ >= 10) {
            memoryScript.Generation++;
            memoryScript.Child = 1;
            memoryScript.nextGeneration();
        }
        lizardScore = memoryScript.currentDistance;
        if  (memoryScript.footTraffic >= memoryScript.mostTraffic) {
            for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
                for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                    memoryScript.FootyBrain[brainChunk, brainBlock] = lizardBrain[brainChunk,brainBlock];
                }
            }
            memoryScript.mostTraffic = memoryScript.footTraffic;
            memoryScript.footyUsedChunks = (int) (numChunks  * (lifeTime / lifeSpan));
        }
        if (lizardScore >= memoryScript.highestScore) {
            for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
                for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                    memoryScript.BestBrain[brainChunk, brainBlock] = lizardBrain[brainChunk,brainBlock];
                }
            }
            memoryScript.highestScore = lizardScore;
            memoryScript.smartestUsedChunks = (int) (numChunks  * (lifeTime / lifeSpan));
        }
        SceneManager.LoadScene("inSimulation");
    }

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        groundCounter = 0;
        newTimeDistance = 0;
        memoryScript.currentDistance = 0;
        childrenPerGen = memoryScript.childrenPerGen;
        childDisplay.text = "Generation: " + memoryScript.Generation + 
            "\nChild: " + memoryScript.Child;
        lizardBody = GetComponent<Rigidbody>();
        thigh1Motor = thigh1.motor;
        thigh2Motor = thigh2.motor;
        thigh3Motor = thigh3.motor;
        thigh4Motor = thigh4.motor;

        ankle1Motor = ankle1.motor;
        ankle2Motor = ankle2.motor;
        ankle3Motor = ankle3.motor;
        ankle4Motor = ankle4.motor;
        
        lizardBrain = new int[numChunks, numBlocks];
        for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
            // this is where the two champions breed.
            int whichBrain = Random.Range(0, 2);
            if (whichBrain == 0) {
                for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                    lizardBrain[brainChunk, brainBlock] = memoryScript.BestParent[brainChunk, brainBlock];
                }
            } else {
                for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                    lizardBrain[brainChunk, brainBlock] = memoryScript.FootyParent[brainChunk, brainBlock];
                }
            }
        }

        currentChunk = 0;
        
        for (int brainChunk = 0; brainChunk < memoryScript.parentUsedChunks; brainChunk++) {
            chunkMutated = false;
            for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                if (!chunkMutated) {
                    int doMutation = Random.Range(0, 3);
                    if (doMutation == 1) {
                        int changeAmount = Random.Range(-1, 2);
                        if (changeAmount == -1) {
                            lizardBrain[brainChunk, brainBlock] = -50;
                        }
                        else if (changeAmount == 1) {
                                lizardBrain[brainChunk, brainBlock] = 50;
                        } else {
                            lizardBrain[brainChunk, brainBlock] = 0;
                        }

                        chunkMutated = true;
                    }
                }
            }
        }

        renderNextActionA();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        fruitTime += Time.deltaTime;
        changeTime += Time.deltaTime;
        if (newTimeDistance > memoryScript.currentDistance) {
            fruitTime = 0.0f;
            newTimeDistance = memoryScript.currentDistance;
        }

        if (lifeTime >= lifeSpan) {
            //die, next species
            nextChild();
        }
        if (fruitTime >= 30f) {
            //die, next species
            nextChild();
        }
        if (changeTime >= 1f) {
            renderNextActionA();
        }
        if (changeTime >= 2.0f) {
            renderNextActionB();
            changeTime -= 2.0f;
        }
    }

    /*void OnCollisionStay(Collision other) {
        if (other.gameObject.name == "ground" & groundCounter++ >= 600)
            nextChild();
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.name == "ground")
            groundCounter = 0;
    }*/
}
