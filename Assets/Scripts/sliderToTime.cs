using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderToTime : MonoBehaviour
{
    Slider timeSlider;
    // Start is called before the first frame update
    void Start()
    {
        timeSlider = GameObject.Find("timeChanger").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 50)
            timeSlider.value = Time.timeScale;
    }

    public void resetTime() {
        Time.timeScale = 1.0f;
    }
    public void pauseTime() {
        Time.timeScale = 0.0f;
    }
    public void hyperTime() {
        Time.timeScale = 20.0f;
    }
}
