using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float start;
    // Update is called once per frame
    void Start() {
        start = Time.time;
        transform.position += transform.forward * 2;
    }
    void Update()
    {
        transform.position += transform.forward * AIController.speed() * Time.deltaTime;
        if (Time.time - start >= 1) {
            Destroy(gameObject);
            AIController.lowerCount();
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "GameController") {
            Destroy(gameObject);
            PlayerHealth health = FindObjectOfType<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
            }
            AIController.lowerCount();
            
        }
    }
}
