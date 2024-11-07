using UnityEngine;

public class Treasure : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>(); // Find the GameManager
            if (scoreManager != null)
            {
                scoreManager.AddScore(1); // Add points to the score
                scoreManager.getScore();
            }

            Destroy(gameObject); // Remove the treasure
        }
    }
}
