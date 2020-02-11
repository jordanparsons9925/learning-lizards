using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class memoryScript
{
    public class Brain {
        public Brain() {
            IDictionary<string, int[,]> parentA = new Dictionary<string, int[,]>();
            IDictionary<string, int[,]> parentB = new Dictionary<string, int[,]>();
            IDictionary<string, int[,]> childA = new Dictionary<string, int[,]>();
            IDictionary<string, int[,]> childB = new Dictionary<string, int[,]>();
            float scoreA = 0.0f;
            float scoreB = 0.0f;
        }

        public void queueParents() {
            parentA.Clear();
            foreach (string currentBehaviour in childA.Keys) {
                int[,] currentArray = childA[currentBehaviour];
                parentA.Add(currentBehaviour, new int[4,2]);
                for (int x = 0; x <= 4; x++) {
                    for (int y = 0; y <=2; y++) {
                        parentA[currentBehaviour][x,y] = currentArray[x,y];
                    }
                }
            }

            parentB.Clear();
            foreach (string currentBehaviour in childB.Keys) {
                int[,] currentArray = childB[currentBehaviour];
                parentB.Add(currentBehaviour, new int[4,2]);
                for (int x = 0; x <= 4; x++) {
                    for (int y = 0; y <=2; y++) {
                        parentB[currentBehaviour][x,y] = currentArray[x,y];
                    }
                }
            }
            scoreA = 0.0f;
            scoreB = 0.0f;
        }
    }

    Brain family1 = new Brain();
    Brain family2 = new Brain();
    Brain family3 = new Brain();
    Brain family4 = new Brain();
    Brain family5 = new Brain();
    Brain family6 = new Brain();
    Brain family7 = new Brain();
    Brain family8 = new Brain();
    Brain family9 = new Brain();
    Brain family10 = new Brain();

    static public int generation;
    static public int family;

    static memoryScript() {
        generation = 1;
        family = 1;
    }

    static void nextFamily() {
        if (family <= 10) {
            family++;
        }
        nextGeneration();
    }

    static void nextGeneration() {
        generation++;
        family = 0;
        family1.queueParents();
        family2.queueParents();
        family3.queueParents();
        family4.queueParents();
        family5.queueParents();
        family6.queueParents();
        family7.queueParents();
        family8.queueParents();
        family9.queueParents();
        family10.queueParents();
    }
}
