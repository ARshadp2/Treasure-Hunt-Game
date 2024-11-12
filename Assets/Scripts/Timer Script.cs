using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript: MonoBehaviour {

    public int targetTime = 18000;
    public TMP_Text timer;
    public 

    void Start() {
        UpdateTimer();
    }

    void Update(){

        targetTime -= 1;
        UpdateTimer();

        if (targetTime <= 0)
        {
            timerEnded();

        }

    }
    void UpdateTimer() {
        timer.SetText("Time Left: " + targetTime);
    }

    void timerEnded()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.dead();
    }


}