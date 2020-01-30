using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class memoryScript
{
    public static int numChunks;
    public static int numBlocks;
    public static int numNeurons;
    public static int lizardChild;
    public static int[, ,] smartestLizardBrain;

    public static int Child 
    {
        get 
        {
            return lizardChild;
        }
        set 
        {
            lizardChild = value;
        }
    }

    public static int[, ,] Brain
    {
        get 
        {
            return smartestLizardBrain;
        }
        set 
        {
            smartestLizardBrain = value;
        }
    }

    static memoryScript() {
        numChunks = lizardLearner.numChunks;
        numBlocks = lizardLearner.numBlocks;
        numNeurons = lizardLearner.numNeurons;

        smartestLizardBrain = new int[numChunks, numBlocks, numNeurons];
        
        lizardChild = 0;

        for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
            for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                for (int brainNeuron = 0; brainNeuron < numNeurons; brainNeuron++) {
                    smartestLizardBrain[brainChunk, brainBlock, brainNeuron] = 0;
                }
            }
        }
    }
}
