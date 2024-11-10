using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnStartButton() {
        SceneManager.LoadScene(1);
    }
    public void OnQuitButton() {
        Application.Quit();
    }
    public void OnHowToButton() {
        SceneManager.LoadScene(2);
    }
}
