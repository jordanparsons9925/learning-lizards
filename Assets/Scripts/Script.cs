using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    static public float lifeSpan = 300f; //Lifespan of lizard in seconds
    static public int numChunks = (int) lifeSpan / 2; //The half-second patterns
    static public int numBlocks = 10; //The parts of the body moved per chunk
    static public int numNeurons = 2; //The angle and strength of movement
    private int[, ,] lizardBrain = new int[numChunks, numBlocks, numNeurons];
    private float changeTime = 0.0f;
    private float fruitTime = 0.0f;
    private float lifeTime = 0.0f;
    private int currentChunk = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
            for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                for (int brainNeuron = 0; brainNeuron < numNeurons; brainNeuron++) {
                    lizardBrain[brainChunk, brainBlock, brainNeuron] = Random.Range(-20, 20);
                }
            }
        }

        
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
            changeTime -= 2.0f;
        }
    }
}
