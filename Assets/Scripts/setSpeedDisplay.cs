using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setSpeedDisplay : MonoBehaviour
{
    public Sprite Sprite20x;
    public Sprite Sprite10x;
    public Sprite Sprite2x;
    public Sprite Sprite1x;
    public Sprite SpriteHalfx;

    // Start is called before the first frame update
    void Start()
    {
        switch (memoryScript.currentSpeed) {
            case 0.25f:
                GetComponent<Image>().sprite = SpriteHalfx;
                break;
            case 1.0f:
                GetComponent<Image>().sprite = Sprite1x;
                break;
            case 4.0f:
                GetComponent<Image>().sprite = Sprite2x;
                break;
            case 20.0f:
                GetComponent<Image>().sprite = Sprite10x;
                break;
            case 40.0f:
                GetComponent<Image>().sprite = Sprite20x;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
