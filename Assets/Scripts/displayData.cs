using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayData : MonoBehaviour
{
    Text childDisplay;
    // Start is called before the first frame update
    void Start()
    {
        childDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        childDisplay.text = "Generation: " + memoryScript.generation + "\nFamily: " + (memoryScript.family + 1);
    }
}
