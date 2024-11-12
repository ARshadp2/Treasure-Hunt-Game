using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinScreen : MonoBehaviour
{
    public void ExitButton()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
