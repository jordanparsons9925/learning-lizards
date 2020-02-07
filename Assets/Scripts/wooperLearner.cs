using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class wooperLearner : MonoBehaviour
{
    private IDictionary<string, int[,]> wooperBrain;
    private string lastBehaviour;
    private string wooperBehaviour;
    private int[,] newBehaviour;
    private int[,] currentAction;
    private float actionTime;
    private int actionCounter;

    private void renderAction(int actionType, int direction) {
        switch (actionType) {
            case 0:
                transform.position += Vector3.forward * Time.deltaTime;
                break;
            case 1:
                transform.position += Vector3.back * Time.deltaTime;
                break;
            case 2:
                break;
        }
    }

    public void changeBehaviour(string collectedBehaviour) {
        wooperBehaviour = collectedBehaviour;
    }
    private int[,] makeNewBehaviour() {
        newBehaviour = new int[4, 2];
        newBehaviour[0, 0] = 0;
        newBehaviour[0, 1] = 0;
        newBehaviour[1, 0] = 0;
        newBehaviour[1, 1] = 0;
        newBehaviour[2, 0] = 0;
        newBehaviour[2, 1] = 0;
        newBehaviour[3, 0] = 0;
        newBehaviour[3, 1] = 0;
        return newBehaviour;
    }

    // Start is called before the first frame update
    void Start()
    {
        wooperBrain = new Dictionary<string, int[,]>();
        wooperBehaviour = "Nothing";
        lastBehaviour = "Nothing";
        actionTime = 0.0f;
        actionCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        actionTime += Time.deltaTime;
        if  (!wooperBrain.Keys.Contains(wooperBehaviour)) {
            wooperBrain.Add(wooperBehaviour, makeNewBehaviour());
        }
        Debug.Log(wooperBehaviour);
        if (actionTime >= 0.5f) {
            if (wooperBehaviour != lastBehaviour || actionCounter > 3) {
                actionCounter = 0;
            }
            currentAction = wooperBrain[wooperBehaviour];
            renderAction(currentAction[actionCounter, 0], currentAction[actionCounter++, 1]);
        }
    }
}
