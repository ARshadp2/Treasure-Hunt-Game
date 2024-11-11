using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class AIHealth : MonoBehaviour
{
    public float health = 10f;
    public Slider healthBar;

    void Start()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;

        if (health <= 0)
        {
            Destroy(gameObject);  // Destroy the AI when health reaches 0
        }
    }
}

