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
    private int limbLimit = 90;
    private int forceLimit = 200;
    private int childrenPerGen;
    private int mutationLevel = 40;
    private int mutationFraction = 4;
    private int mutationCounter;
    private bool chunkMutated;
    static public int numChunks = (int) lifeSpan * 4; //The half-second patterns
    static public int numBlocks = 8; //The parts of the body moved per chunk
    static public int numNeurons = 2; //The angle and strength of movement
    
    //data for lizard brains
    private int[, ,] lizardBrain;
    private int currentChunk;
    private int groundCounter;
    public float newTimeDistance;
    public float lizardScore;

    public void setTime (float newTime) {
        Time.timeScale = newTime;
    }

    void renderNextActionA () {
        //assigns the neurons to the associated motor angle and force
        thigh1Motor.targetVelocity = lizardBrain[currentChunk, 0, 0];
        thigh1Motor.force = lizardBrain[currentChunk, 0, 1];
        thigh4Motor.targetVelocity= lizardBrain[currentChunk, 3, 0];
        thigh4Motor.force = lizardBrain[currentChunk, 3, 1];
        ankle1Motor.targetVelocity = lizardBrain[currentChunk, 4, 0];
        ankle1Motor.force = lizardBrain[currentChunk, 4, 1];
        ankle4Motor.targetVelocity = lizardBrain[currentChunk, 7, 0];
        ankle4Motor.force = lizardBrain[currentChunk, 7, 1];

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
        thigh2Motor.targetVelocity = lizardBrain[currentChunk, 1, 0];
        thigh2Motor.force = lizardBrain[currentChunk, 1, 1];
        thigh3Motor.targetVelocity = lizardBrain[currentChunk, 2, 0];
        thigh3Motor.force = lizardBrain[currentChunk, 2, 1];
        ankle2Motor.targetVelocity = lizardBrain[currentChunk, 5, 0];
        ankle2Motor.force = lizardBrain[currentChunk, 5, 1];
        ankle3Motor.targetVelocity = lizardBrain[currentChunk, 6, 0];
        ankle3Motor.force = lizardBrain[currentChunk++, 6, 1];

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
        if  (memoryScript.footTraffic > memoryScript.mostTraffic) {
            for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
                for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                    for (int brainNeuron = 0; brainNeuron < numNeurons; brainNeuron++) {
                        memoryScript.FootyBrain[brainChunk, brainBlock, brainNeuron] = lizardBrain[brainChunk,brainBlock, brainNeuron];
                    }
                }
            }
            memoryScript.mostTraffic = memoryScript.footTraffic;
            memoryScript.footyUsedChunks = (int) (numChunks  * (lifeTime / lifeSpan));
        }
        else if (lizardScore > memoryScript.highestScore) {
            for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
                for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                    for (int brainNeuron = 0; brainNeuron < numNeurons; brainNeuron++) {
                        memoryScript.BestBrain[brainChunk, brainBlock, brainNeuron] = lizardBrain[brainChunk,brainBlock, brainNeuron];
                    }
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
        mutationCounter = 0;
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
        
        lizardBrain = new int[numChunks, numBlocks, numNeurons];
        for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
            for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                for (int brainNeuron = 0; brainNeuron < numNeurons; brainNeuron++) {
                    lizardBrain[brainChunk, brainBlock, brainNeuron] = memoryScript.ParentBrain[brainChunk, brainBlock, brainNeuron];
                }
            }
        }

        currentChunk = 0;
        
        for (int brainChunk = 0; brainChunk < memoryScript.parentUsedChunks; brainChunk++) {
            if (mutationCounter < memoryScript.parentUsedChunks / mutationFraction) {
                chunkMutated = false;
                for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                    if (!chunkMutated) {
                        int doMutation = Random.Range(0, 2);
                        if (doMutation == 1) {
                                //generate a random amount of mutation
                                int changeAmount = Random.Range((-1)*mutationLevel, mutationLevel);
                                //check if angle is too high when increasing
                                if (changeAmount > 0 && lizardBrain[brainChunk, brainBlock, 0] < limbLimit)
                                    lizardBrain[brainChunk, brainBlock, 0] += changeAmount;
                                //checks if angle is too low when decreasing
                                else if (changeAmount < 0 && lizardBrain[brainChunk,brainBlock, 0] > (-1)*limbLimit)
                                    lizardBrain[brainChunk, brainBlock, 0] += changeAmount;

                                if (lizardBrain[brainChunk, brainBlock, 0] > limbLimit)
                                    lizardBrain[brainChunk, brainBlock, 0] = limbLimit;
                                else if (lizardBrain[brainChunk, brainBlock, 0] < (-1)*limbLimit)
                                    lizardBrain[brainChunk, brainBlock, 0] = -1*limbLimit;
                                
                                //generate a random amount of mutation
                                changeAmount = Random.Range((-1)*mutationLevel, mutationLevel);
                                //check if force is too high when increasing
                                if (changeAmount > 0 && lizardBrain[brainChunk, brainBlock, 1] < forceLimit)
                                    lizardBrain[brainChunk, brainBlock, 1] += changeAmount;
                                //checks if force is too low when decreasing
                                else if (changeAmount < 0 && lizardBrain[brainChunk,brainBlock, 1] > 0)
                                    lizardBrain[brainChunk, brainBlock, 1] += changeAmount;

                                if (lizardBrain[brainChunk, brainBlock, 1] > forceLimit)
                                    lizardBrain[brainChunk, brainBlock, 1] = forceLimit;
                                else if (lizardBrain[brainChunk, brainBlock, 1] < 0)
                                    lizardBrain[brainChunk, brainBlock, 1] = 0;
                            chunkMutated = true;
                            mutationCounter++;
                        }
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
        if (newTimeDistance != memoryScript.currentDistance) {
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
        if (changeTime >= 0.5f) {
            renderNextActionA();
        }
        if (changeTime >= 1.0f) {
            renderNextActionB();
            changeTime -= 1.0f;
        }
    }

    void OnCollisionStay(Collision other) {
        if (other.gameObject.name == "ground" & groundCounter++ >= 30)
            nextChild();
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.name == "ground")
            groundCounter = 0;
    }
}
