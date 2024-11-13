using UnityEngine;

public class Treasure : MonoBehaviour
{
    private AudioSource audio;
    void Start() {
        audio = FindObjectOfType<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            audio.Play();
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
