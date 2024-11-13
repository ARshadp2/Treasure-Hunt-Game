using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempHealth : MonoBehaviour
{
    public int health = 10;
    public GameObject AI;
    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    public void hit()
{
    health--;
    healthBar.value = health;

    Debug.Log("AI health: " + health);

    if (health <= 0)
    {
        Destroy(AI);  
    }
}

}
