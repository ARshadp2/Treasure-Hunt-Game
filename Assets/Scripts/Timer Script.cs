using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript: MonoBehaviour {
    private int textTime = 75;
    private int actualTime = 75;
    public TMP_Text timer; 

    void Start() {
        UpdateTimer();
    }

    void Update(){
        textTime = actualTime - (int) Time.time;
        UpdateTimer();

        if (textTime <= 0)
        {
            timerEnded();
        }

    }
    void UpdateTimer() {
        string seconds;
        if (textTime % 60 < 10)
            seconds = "0" + textTime % 60;
        else
            seconds = textTime % 60 + "";
        timer.SetText("Time Left: " + textTime / 60 + ":" + seconds);
    }

    void timerEnded()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.dead();
    }


}