using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsMenu : MonoBehaviour
{
    public void OnBackButton() {
        SceneManager.LoadScene(0);
    }
}
