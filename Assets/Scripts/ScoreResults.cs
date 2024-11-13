using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreResults : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject recorded_score;
    void Update() {
        scoreText.SetText("Score: " + ScoreManager.getstaticScore());
    }
}
