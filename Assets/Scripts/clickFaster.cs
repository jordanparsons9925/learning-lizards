using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickFaster : MonoBehaviour
{
    bool disabled;
    Image buttonSelf;
    Color normalColor;
    Color disabledColor;

    public Sprite Sprite20x;
    public Sprite Sprite10x;
    public Sprite Sprite2x;
    public Sprite Sprite1x;

    // Start is called before the first frame update
    void Start()
    {
        buttonSelf = GetComponent<Image>();
        disabled = false;

        normalColor.r = 0;
        normalColor.g = 255;
        normalColor.b = 203;
        normalColor.a = 255;

        disabledColor.r = 255;
        disabledColor.g = 255;
        disabledColor.b = 255;
        disabledColor.a = 255;

        buttonSelf.color = memoryScript.speedColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseSpeed() {
        switch (memoryScript.currentSpeed) {
            case 20.0f:
                memoryScript.currentSpeed = 40.0f;
                GameObject.FindGameObjectWithTag("speedDisplay").GetComponent<Image>().sprite = 
                    Sprite20x;
                break;
            case 4.0f:
                memoryScript.currentSpeed = 20.0f;
                GameObject.FindGameObjectWithTag("speedDisplay").GetComponent<Image>().sprite = 
                    Sprite10x;
                break;
            case 1.0f:
                memoryScript.currentSpeed = 4.0f;
                GameObject.FindGameObjectWithTag("speedDisplay").GetComponent<Image>().sprite = 
                    Sprite2x;
                break;
            case 0.25f:
                memoryScript.currentSpeed = 1.0f;
                GameObject slowButton = GameObject.FindGameObjectWithTag("slowButton");
                slowButton.GetComponent<Image>().color = 
                    normalColor;
                memoryScript.slowColor = normalColor;
                GameObject.FindGameObjectWithTag("speedDisplay").GetComponent<Image>().sprite = 
                    Sprite1x;
                break;
        }
        if (!memoryScript.paused && memoryScript.currentSpeed < 40.0f)
            Time.timeScale = memoryScript.currentSpeed;
        if (memoryScript.currentSpeed == 40.0f) {
            buttonSelf.color = disabledColor;
            memoryScript.speedColor = disabledColor;
        } else {
            buttonSelf.color = normalColor;
            memoryScript.speedColor = normalColor;
        }
    }
}