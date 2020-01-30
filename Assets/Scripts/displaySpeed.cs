using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displaySpeed : MonoBehaviour
{
    Text speedDisplay;
    Text scoreDisplay;
    // Start is called before the first frame update
    void Start()
    {
        speedDisplay = GameObject.Find("speedDisplay").GetComponent<Text>();
        scoreDisplay = GameObject.Find("highScore").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float roundSpeed = (float) ((int) (Time.timeScale * 100)) / 100;
        speedDisplay.text = "Speed: " + roundSpeed;
        scoreDisplay.text = "High Score: " + memoryScript.longestDistance;
    }
}
