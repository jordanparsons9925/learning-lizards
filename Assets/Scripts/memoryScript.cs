using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class memoryScript
{
    public static int numChunks;
    public static int numBlocks;
    public static int numNeurons;
    public static int childrenPerGen = 10;
    private static int lizardChild = 1;
    private static int lizardGeneration;
    private static int[, ,] currentParent;
    private static int[, ,] smartestLizard;
    private static int[, ,] footyLizard;
    public static float currentDistance;
    public static int footTraffic;
    public static float highestScore;
    public static int mostTraffic;
    public static int parentUsedChunks;
    public static int smartestUsedChunks;
    public static int footyUsedChunks;

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

    public static int[, ,] ParentBrain
    {
        get 
        {
            return currentParent;
        }
        set 
        {
            currentParent = value;
        }
    }

    public static int[, ,] BestBrain
    {
        get 
        {
            return smartestLizard;
        }
        set 
        {
            smartestLizard = value;
        }
    }
    public static int[, ,] FootyBrain
    {
        get 
        {
            return footyLizard;
        }
        set 
        {
            footyLizard = value;
        }
    }

    public static int Generation
    {
        get
        {
            return lizardGeneration;
        }
        set
        {
            lizardGeneration = value;
        }
    }

    static memoryScript() {
        numChunks = lizardLearner.numChunks;
        numBlocks = lizardLearner.numBlocks;
        numNeurons = lizardLearner.numNeurons;

        currentParent = new int[numChunks, numBlocks, numNeurons];
        smartestLizard = new int[numChunks, numBlocks, numNeurons];
        footyLizard = new int[numChunks, numBlocks, numNeurons];
        lizardGeneration = 1;
        highestScore = 0;
        mostTraffic = 0;
        parentUsedChunks = 4;
        smartestUsedChunks = 4;
        footyUsedChunks = 4;

        for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
            for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                for (int brainNeuron = 0; brainNeuron < numNeurons; brainNeuron++) {
                    currentParent[brainChunk, brainBlock, brainNeuron] = 0;
                }
            }
        }
    }

    public static void nextGeneration() {
        for (int brainChunk = 0; brainChunk < numChunks; brainChunk++) {
            // this is where the two champions breed.
            int whichBrain = Random.Range(0, 2);
            if (whichBrain == 0) {
                for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                    for (int brainNeuron = 0; brainNeuron < numNeurons; brainNeuron++) {
                        currentParent[brainChunk, brainBlock, brainNeuron] = smartestLizard[brainChunk, brainBlock, brainNeuron];
                    }
                }
            } else {
                for (int brainBlock = 0; brainBlock < numBlocks; brainBlock++) {
                    for (int brainNeuron = 0; brainNeuron < numNeurons; brainNeuron++) {
                        currentParent[brainChunk, brainBlock, brainNeuron] = footyLizard[brainChunk, brainBlock, brainNeuron];
                    }
                }
            }
        }
        parentUsedChunks = (smartestUsedChunks + footyUsedChunks) / 2;
        highestScore = 0;
        mostTraffic = 0;
    }
}
