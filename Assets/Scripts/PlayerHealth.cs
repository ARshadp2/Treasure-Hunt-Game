using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public Slider healthBar;
    private float original;

    void Start() {
        original = health;
        
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health/original;
        Debug.Log(health);
        if (health <= 0)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            player.dead();
            Debug.Log("Player has died!");
            // Implement player death logic, like restarting the game or showing a game over screen.
        }
    }
}
