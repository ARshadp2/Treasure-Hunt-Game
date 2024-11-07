using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();
    }
    public void AddScore(int points)
    {
        score += points; // Add points to the score
        UpdateScoreText(); // Update the displayed score
    }
    private void UpdateScoreText()
    {
        scoreText.SetText("Score: " + score); // Update the score display
    }
    public void getScore() {
        Debug.Log(score + "");
    }

}
