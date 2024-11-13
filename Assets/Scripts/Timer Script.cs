using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript: MonoBehaviour {

<<<<<<< Updated upstream
    private int textTime = 75;
    private int actualTime = 75;
    public TMP_Text timer; 
=======
    public float targetTime = 180f;
    public TMP_Text timer;
    public 
>>>>>>> Stashed changes

    void Start() {
        UpdateTimer();
    }

    void Update(){
<<<<<<< Updated upstream
        textTime = actualTime - (int) Time.time;
=======

        targetTime -= 0.01f;
>>>>>>> Stashed changes
        UpdateTimer();

        if (actualTime <= 0)
        {
            timerEnded();
        }

    }
    void UpdateTimer() {
<<<<<<< Updated upstream
        string seconds;
        if (textTime % 60 < 10)
            seconds = "0" + textTime % 60;
        else
            seconds = textTime % 60 + "";
        timer.SetText("Time Left: " + textTime / 60 + ":" + seconds);
=======
        targetTime = Mathf.Round(targetTime * 100.0f)* 0.01f;
        timer.SetText("Time Left: " + targetTime);
>>>>>>> Stashed changes
    }

    void timerEnded()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.dead();
    }


}