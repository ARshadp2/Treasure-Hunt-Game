using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }
    public void RestartButton() {
        SceneManager.LoadScene("Start Menu");
    }

    public void ExitButton() {
        SceneManager.LoadScene("Instructions Menu");
    }
}
