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

        public int pairedWith;

        public bool beingModified;

        public Brain() {
            parentA = new Dictionary<string, int[,]>();
            parentA.Add("Nothing", new int[8, 2]);
            parentA["Nothing"][0, 0] = 1;
            parentA["Nothing"][0, 1] = 0;
            parentA["Nothing"][1, 0] = 1;
            parentA["Nothing"][1, 1] = 0;
            parentA["Nothing"][2, 0] = 1;
            parentA["Nothing"][2, 1] = 0;
            parentA["Nothing"][3, 0] = 1;
            parentA["Nothing"][3, 1] = 0;
            parentA["Nothing"][4, 0] = 1;
            parentA["Nothing"][4, 1] = 0;
            parentA["Nothing"][5, 0] = 1;
            parentA["Nothing"][5, 1] = 0;
            parentA["Nothing"][6, 0] = 1;
            parentA["Nothing"][6, 1] = 0;
            parentA["Nothing"][7, 0] = 1;
            parentA["Nothing"][7, 1] = 0;
            parentB = new Dictionary<string, int[,]>();
            parentB.Add("Nothing", new int[8, 2]);
            parentB["Nothing"][0, 0] = 1;
            parentB["Nothing"][0, 1] = 0;
            parentB["Nothing"][1, 0] = 1;
            parentB["Nothing"][1, 1] = 0;
            parentB["Nothing"][2, 0] = 1;
            parentB["Nothing"][2, 1] = 0;
            parentB["Nothing"][3, 0] = 1;
            parentB["Nothing"][3, 1] = 0;
            parentB["Nothing"][4, 0] = 1;
            parentB["Nothing"][4, 1] = 0;
            parentB["Nothing"][5, 0] = 1;
            parentB["Nothing"][5, 1] = 0;
            parentB["Nothing"][6, 0] = 1;
            parentB["Nothing"][6, 1] = 0;
            parentB["Nothing"][7, 0] = 1;
            parentB["Nothing"][7, 1] = 0;
            childA = new Dictionary<string, int[,]>();
            childB = new Dictionary<string, int[,]>();
            scoreA = 0.0f;
            scoreB = 0.0f;
            pairedWith = -1;
            beingModified = false;
        }

        public void queueParents() {
            parentA.Clear();
            foreach (string currentBehaviour in childA.Keys) {
                int[,] currentArray = childA[currentBehaviour];
                parentA.Add(currentBehaviour, new int[8,2]);
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 2; y++) {
                        parentA[currentBehaviour][x,y] = currentArray[x,y];
                    }
                }
            }

            parentB.Clear();
            foreach (string currentBehaviour in childB.Keys) {
                int[,] currentArray = childB[currentBehaviour];
                parentB.Add(currentBehaviour, new int[8,2]);
                for (int x = 0; x < 4; x++) {
                    for (int y = 0; y < 2; y++) {
                        parentB[currentBehaviour][x,y] = currentArray[x,y];
                    }
                }
            }
            scoreA = 0.0f;
            scoreB = 0.0f;
            pairedWith = -1;
        }
    }

    public static Brain[] familyBrains = new Brain[10];

    static public int generation;
    static public int family;
    static public int familyMatch;
    static public int numDead;

    static memoryScript() {
        Time.timeScale = 10.0f;
        generation = 1;
        family = 0;
        familyMatch = -1;
        numDead = 0;
        for (int i = 0; i < familyBrains.Length; i++) {
            familyBrains[i] = new Brain();
        }
        //UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
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
        if (generation % 10 == 10) {
            Time.timeScale = 1.0f;
        }
        if (generation % 10 != 10) {
            Time.timeScale = 10.0f;
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
