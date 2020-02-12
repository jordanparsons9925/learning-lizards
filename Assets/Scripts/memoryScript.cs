using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class memoryScript
{
    public class Brain {
        public IDictionary<string, int[,]> parentA;
        public IDictionary<string, int[,]> parentB;
        public IDictionary<string, int[,]> childA;
        public IDictionary<string, int[,]> childB;
        public float scoreA;
        public float scoreB;

        public bool parentTaken;
        public Brain() {
            parentA = new Dictionary<string, int[,]>();
            parentB = new Dictionary<string, int[,]>();
            childA = new Dictionary<string, int[,]>();
            childB = new Dictionary<string, int[,]>();
            scoreA = 0.0f;
            scoreB = 0.0f;
            parentTaken = false;
        }

        public void queueParents() {
            parentA.Clear();
            foreach (string currentBehaviour in childA.Keys) {
                int[,] currentArray = childA[currentBehaviour];
                parentA.Add(currentBehaviour, new int[4,2]);
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 2; y++) {
                        parentA[currentBehaviour][x,y] = currentArray[x,y];
                    }
                }
            }

            parentB.Clear();
            foreach (string currentBehaviour in childB.Keys) {
                int[,] currentArray = childB[currentBehaviour];
                parentB.Add(currentBehaviour, new int[4,2]);
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 2; y++) {
                        parentB[currentBehaviour][x,y] = currentArray[x,y];
                    }
                }
            }
            scoreA = 0.0f;
            scoreB = 0.0f;
            parentTaken = false;
        }
    }

    public static Brain[] familyBrains = new Brain[10];

    static public int generation;
    static public int family;
    static public int familyMatch;
    static public int numDead;

    static memoryScript() {
        generation = 1;
        family = 0;
        familyMatch = -1;
        numDead = 0;
        for (int i = 0; i < familyBrains.Length; i++) {
            familyBrains[i] = new Brain();
        }
        UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
    }
    
    public static void checkDeaths() {
        if (numDead > 9) {
            nextFamily();
        }
    }

    static void nextFamily() {
        Debug.ClearDeveloperConsole();
        numDead = 0;
        if (family <= 8) {
            family++;
            familyMatch = -1;
        } else {
            nextGeneration();
        }
        SceneManager.LoadScene("inSimulation");
    }

    static void nextGeneration() {
        generation++;
        family = 0;
        familyMatch = -1;
        foreach (Brain family in familyBrains) {
            family.queueParents();
        }
    }
}
