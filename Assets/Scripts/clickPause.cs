using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickPause : MonoBehaviour
{
    bool paused = false;
    Image buttonSelf;

    public Sprite pauseSprite;
    public Sprite playSprite;
    // Start is called before the first frame update
    void Start()
    {
        buttonSelf = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void togglePause() {
        paused = !paused;
        if (paused) {
            Time.timeScale = 0.0f;
            buttonSelf.sprite = playSprite;
        }
            
        else {
            Time.timeScale = 1.0f;
            buttonSelf.sprite = pauseSprite;
        }
    }
}
