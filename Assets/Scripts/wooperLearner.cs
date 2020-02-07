using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class wooperLearner : MonoBehaviour
{
    private IDictionary<string, int[,]> wooperBrain;
    private Rigidbody wooperBody;
    private string lastBehaviour;
    private string wooperBehaviour;
    private int[,] currentAction;
    private float actionTime;
    private float walkingSpeed;
    private int actionCounter;

    private void renderAction(int actionType, int direction) {
        switch (actionType) {
            case 0:
                wooperBody.velocity = transform.forward * walkingSpeed;
                break;
            case 1:
                wooperBody.velocity += transform.forward * walkingSpeed * (0 - 1);
                break;
            case 2:
                break;
        }
    }

    public void changeBehaviour(string collectedBehaviour) {
        wooperBehaviour = collectedBehaviour;
    }
    private int[,] makeNewBehaviour() {
        int[,] newBehaviour = new int[4, 2];
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
    private int[,] makeNewBehaviourB() {
        int[,] newBehaviour = new int[4, 2];
        newBehaviour[0, 0] = 1;
        newBehaviour[0, 1] = 0;
        newBehaviour[1, 0] = 1;
        newBehaviour[1, 1] = 0;
        newBehaviour[2, 0] = 1;
        newBehaviour[2, 1] = 0;
        newBehaviour[3, 0] = 1;
        newBehaviour[3, 1] = 0;
        return newBehaviour;
    }

    // Start is called before the first frame update
    void Start()
    {
        walkingSpeed = 10.0f;
        wooperBody = GetComponent<Rigidbody>();
        wooperBrain = new Dictionary<string, int[,]>();
        wooperBrain.Add("Nothing", makeNewBehaviour());
        wooperBehaviour = "Nothing";
        lastBehaviour = "Nothing";
        actionTime = 0.0f;
        actionCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        actionTime += Time.deltaTime;
        Debug.Log(wooperBehaviour);
        if  (!wooperBrain.Keys.Contains(wooperBehaviour)) {
            int[,] newBehaviour = new int[4, 2];
            int[,] cleanBehaviour = makeNewBehaviourB();
            for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 2; y++) {
                    newBehaviour[x, y] = cleanBehaviour[x, y];
                    Debug.Log(newBehaviour[x, y]);
                }
            }
            wooperBrain.Add(wooperBehaviour, newBehaviour);
        }
        if (actionTime >= 0.5f) {
            if (actionCounter > 3) {
                actionCounter = 0;
            }
            currentAction = wooperBrain[wooperBehaviour];
            renderAction(currentAction[actionCounter, 0], currentAction[actionCounter++, 1]);
            actionTime -= 0.5f;
        }
    }
}
