using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickSlower : MonoBehaviour
{
    bool disabled;
    Image buttonSelf;
    Color normalColor;
    Color disabledColor;

    public Sprite Sprite10x;
    public Sprite Sprite2x;
    public Sprite Sprite1x;
    public Sprite SpriteHalfx;

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

        buttonSelf.color = memoryScript.slowColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseSpeed() {
        switch (memoryScript.currentSpeed) {
            case 1.0f:
                memoryScript.currentSpeed = 0.25f;
                GameObject.FindGameObjectWithTag("speedDisplay").GetComponent<Image>().sprite = 
                    SpriteHalfx;
                break;
            case 4.0f:
                memoryScript.currentSpeed = 1.0f;
                GameObject.FindGameObjectWithTag("speedDisplay").GetComponent<Image>().sprite = 
                    Sprite1x;
                break;
            case 20.0f:
                memoryScript.currentSpeed = 4.0f;
                GameObject.FindGameObjectWithTag("speedDisplay").GetComponent<Image>().sprite = 
                    Sprite2x;
                break;
            case 40.0f:
                memoryScript.currentSpeed = 20.0f;
                GameObject speedButton = GameObject.FindGameObjectWithTag("speedButton");
                speedButton.GetComponent<Image>().color = 
                    normalColor;
                memoryScript.speedColor = normalColor;
                GameObject.FindGameObjectWithTag("speedDisplay").GetComponent<Image>().sprite = 
                    Sprite10x;
                break;
        }
        if (!memoryScript.paused && memoryScript.currentSpeed > 0.25f)
            Time.timeScale = memoryScript.currentSpeed;
        if (memoryScript.currentSpeed == 0.25f) {
            buttonSelf.color = disabledColor;
            memoryScript.slowColor = disabledColor;
        } else {
            buttonSelf.color = normalColor;
            memoryScript.slowColor = normalColor;
        }
    }
}
