using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static int static_score = 0;
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
        static_score += points;
        UpdateScoreText(); // Update the displayed score
    }
    private void UpdateScoreText()
    {
        scoreText.SetText("Score: " + score); // Update the score display
    }
    void Update() {
        if (score == 10) {
            Destroy(gameObject);
            SceneManager.LoadScene(4);
        }
    }
    public int getScore() {
        return score;
    }
    public static int getstaticScore() {
        return static_score;
    }
    public static void reset() {
        static_score = 0;
    }
}
