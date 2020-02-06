using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class wooperLearner : MonoBehaviour
{
    IDictionary<string, int[]> wooperBrain = new Dictionary<string, int[]>();
    private float footTime = 0.0f;
    private float partTime = 0.0f;
    private float actionTime = 0.0f;
    private string lastBehaviour;
    private string wooperBehaviour;
    private int[] newBehaviour;
    private int[] currentBehaviour;
    private int[] currentAction;
    private 

    void decideAction(int actionInt) {
        switch (actionInt) {
            //Walk forward
            case 0:
                break;
            //Walk backward
            case 1:
                break;
            //Jump forward
            case 2:
                break;
            //Jump backward
            case 3:
                break;
            //Turn right
            case 4:
                break;
            //Turn left
            case 5:
                break;
        }
    }

    public void changeBehaviour(string collectedBehaviour) {
        wooperBehaviour = collectedBehaviour;
    }

    // Start is called before the first frame update
    void Start()
    {
        newBehaviour = new int[4];
        newBehaviour[0] = 0;
        newBehaviour[1] = 0;
        newBehaviour[2] = 0;
        newBehaviour[3] = 0;
        wooperBehaviour = "Nothing";
        lastBehaviour = "Nothing";
    }

    // Update is called once per frame
    void Update()
    {
        footTime += Time.deltaTime;
        if (wooperBehaviour != "Ground" && wooperBehaviour != "Lizard" && wooperBehaviour != "Sight" && !wooperBrain.Keys.Contains(wooperBehaviour)) {
            wooperBrain.Add(wooperBehaviour, newBehaviour);
            Debug.Log(wooperBehaviour);
        }
        if (footTime >= 0.5f) {
        }
        if (footTime >= 1.0f) {
        }
        if (actionTime >= 2.0f) {
        }
    }
}
