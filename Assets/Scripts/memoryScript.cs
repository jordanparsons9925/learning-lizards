using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class memoryScript
{
    public class Brain {
        public Brain()
        {
            public IDictionary<string, int[,]> parentA = new Dictionary<string, int[,]>();
            public IDictionary<string, int[,]> parentB = new Dictionary<string, int[,]>();
            public IDictionary<string, int[,]> childA = new Dictionary<string, int[,]>();
            public IDictionary<string, int[,]> childB = new Dictionary<string, int[,]>();
        }

        
    }
    //Parents
    static public IDictionary<string, int[,]> parentBrain1A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain2A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain3A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain4A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain5A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain6A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain7A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain8A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain9A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain10A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain1B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain2B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain3B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain4B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain5B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain6B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain7B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain8B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain9B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> parentBrain10B = new Dictionary<string, int[,]>();

    //Children
    static public IDictionary<string, int[,]> bestChild1A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild2A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild3A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild4A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild5A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild6A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild7A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild8A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild9A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild10A = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild1B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild2B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild3B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild4B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild5B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild6B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild7B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild8B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild9B = new Dictionary<string, int[,]>();
    static public IDictionary<string, int[,]> bestChild10B = new Dictionary<string, int[,]>();

    static public int generation;
    static public int family;

    static public float generationalBestA;
    static public float generationalBestB;

    static memoryScript() {
        generation = 1;
        family = 1;
        generationalBestA = 0.0f;
        generationalBestB = 0.0f;
    }

    static void nextFamily() {
        if (family <= 10) {
            family++;
        generationalBestA = 0.0f;
        generationalBestB = 0.0f;
        }
        nextGeneration();
    }

    static void nextGeneration() {
        generation++;
        family = 0;
        generationalBestA = 0.0f;
        generationalBestB = 0.0f;
        
        parentBrain1A.Clear();
        foreach (string currentBehaviour in bestChild1A.Keys) {
            int[,] currentArray = bestChild1A[currentBehaviour];
            parentBrain1A.Add(currentBehaviour, new int[4,2]);
            for (int x = 0; x <= 4; x++) {
                for (int y = 0; y <=2; y++) {
                    parentBrain1A[currentBehaviour][x,y] = currentArray[x,y];
                }
            }
        }
    }
}
