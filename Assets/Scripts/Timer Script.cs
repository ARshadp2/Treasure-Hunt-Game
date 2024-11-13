using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript: MonoBehaviour {
    private int start_time = 0;
    private int textTime = 180;
    private int actualTime = 180;
    public TMP_Text timer; 
    private bool on_scene = false;

    void Start() {
        UpdateTimer();
    }

    void Update() {
        if (on_scene == false && SceneManager.GetActiveScene().name == "Terrain") {
            ScoreManager.reset();
            start_time = (int) Time.time;
            on_scene = true;
        }
        if (on_scene == true) {
            textTime = start_time + actualTime - (int) Time.time;
            UpdateTimer();

            if (textTime <= 0)
            {
                timerEnded();
            }
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
        on_scene = false;
    }


}