using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    //hinge objects on lizard
    public HingeJoint thigh1;
    public HingeJoint thigh1Middle;
    public float thigh1Angle;
    public float thigh1Force;
    public HingeJoint thigh2;
    public HingeJoint thigh2Middle;
    public float thigh2Angle;
    public float thigh2Force;
    public HingeJoint thigh3;
    public float thigh3Angle;
    public float thigh3Force;
    public HingeJoint thigh4;
    public float thigh4Angle;
    public float thigh4Force;
    public HingeJoint ankle1;
    public float ankle1Angle;
    public float ankle1Force;
    public HingeJoint ankle2;
    public float ankle2Angle;
    public float ankle2Force;
    public HingeJoint ankle3;
    public float ankle3Angle;
    public float ankle3Force;
    public HingeJoint ankle4;
    public float ankle4Angle;
    public float ankle4Force;
    //variables for setting up the lizards
    static public float lifeSpan = 300f; //Lifespan of lizard in seconds
    private float changeTime = 0.0f;
    private float fruitTime = 0.0f;
    private float lifeTime = 0.0f;
    private int limbLimit = 120;
    private int forceLimit = 200;
    private int childrenPerGen = 10;
    private int mutationLevel = 50;
    static public int numChunks = (int) lifeSpan / 2; //The half-second patterns
    static public int numBlocks = 8; //The parts of the body moved per chunk
    static public int numNeurons = 2; //The angle and strength of movement
    
    //data for lizard brains
    private int[, ,] smartestLizardBrain = new int[numChunks, numBlocks, numNeurons];
    private int[, ,] lizardBrain;
    private int currentChunk;
    private float lizardBestTime = 0.0f;
    private int lizardBestDistance;

    //current variables
    private int lizardChild;
    private int lizardGeneration;
    private int lizardScore;

    void makeNewChild() {

    }

    void renderNextAction () {
        Debug.Log("Chunk " + currentChunk);
        thigh1Angle = lizardBrain[currentChunk, 0, 0];
        thigh1Force = lizardBrain[currentChunk, 0, 1];
        thigh2Angle = lizardBrain[currentChunk, 1, 0];
        thigh2Force = lizardBrain[currentChunk, 1, 1];
        thigh3Angle = lizardBrain[currentChunk, 2, 0];
        thigh3Force = lizardBrain[currentChunk, 2, 1];
        thigh4Angle = lizardBrain[currentChunk, 3, 0];
        thigh4Force = lizardBrain[currentChunk, 3, 1];

        ankle1Angle = lizardBrain[currentChunk, 4, 0];
        ankle1Force = lizardBrain[currentChunk, 4, 1];
        ankle2Angle = lizardBrain[currentChunk, 5, 0];
        ankle2Force = lizardBrain[currentChunk, 5, 1];
        ankle3Angle = lizardBrain[currentChunk, 6, 0];
        ankle3Force = lizardBrain[currentChunk, 6, 1];
        ankle4Angle = lizardBrain[currentChunk, 7, 0];
        ankle4Force = lizardBrain[currentChunk++, 7, 1];
    }

    void Awake()
    {
        for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
            for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                for (int brainNeuron = 0; brainNeuron < numNeurons; brainNeuron++) {
                    smartestLizardBrain[brainChunk, brainBlock, brainNeuron] = 0;
                }
            }
        }

        lizardChild = 0;
        lizardGeneration = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start!");
        thigh1Angle = thigh1.motor.targetVelocity;
        thigh1Force = thigh1.motor.force;
        thigh2Angle = thigh2.motor.targetVelocity;
        thigh2Force = thigh2.motor.force;
        thigh3Angle = thigh3.motor.targetVelocity;
        thigh3Force = thigh3.motor.force;
        thigh4Angle = thigh4.motor.targetVelocity;
        thigh4Force = thigh4.motor.force;
        
        ankle1Angle = ankle1.motor.targetVelocity;
        ankle1Force = ankle1.motor.force;
        ankle2Angle = ankle2.motor.targetVelocity;
        ankle2Force = ankle2.motor.force;
        ankle3Angle = ankle3.motor.targetVelocity;
        ankle3Force = ankle3.motor.force;
        ankle4Angle = ankle4.motor.targetVelocity;
        ankle4Force = ankle4.motor.force;
        
        lizardBrain = new int[numChunks, numBlocks, numNeurons];
        for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
            for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                for (int brainNeuron = 0; brainNeuron < numNeurons; brainNeuron++) {
                    lizardBrain[brainChunk, brainBlock, brainNeuron] = smartestLizardBrain[brainChunk, brainBlock, brainNeuron];
                }
            }
        }

        currentChunk = 0;
        lizardChild++;
        
        for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
            for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {

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
            }
        }

        renderNextAction();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        fruitTime += Time.deltaTime;
        changeTime += Time.deltaTime;

        if (lifeTime >= lifeSpan) {
            //die, next species
        }
        if (fruitTime >= 60f) {
            //die, next species
        }
        if (changeTime >= 2.0f) {
            Debug.Log("Next Action...");
            renderNextAction();
            changeTime -= 2.0f;
        }
    }
}
