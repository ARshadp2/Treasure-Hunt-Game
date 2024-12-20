using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float start;
    // Update is called once per frame
    void Start() {
        start = Time.time;
        transform.position += transform.forward * 3;
    }

    void Update()
    {
        transform.position += transform.forward * 30 * Time.deltaTime;
        if (Time.time - start >= 1) {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "AI") {
            Destroy(gameObject);
            TempHealth health = other.GetComponent<TempHealth>();
            if (health != null)
                health.hit();
        }
    }
}
