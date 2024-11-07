using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField, Range(10,100)] float max_vel_rot = 10;

    public Transform target;
    private Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 0.125f;

    private float velocity_rot;
    private float rotation;
    private bool usemouse = false;

    void LateUpdate()
    {
        Cursor.visible = false;
        if (Input.GetKey(KeyCode.M))
            usemouse = true;
        if (Input.GetKey(KeyCode.N))
            usemouse = false;
        // Rotational movement that corresponds with player
        float input_rot;
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
            input_rot = 0;
        else if (Input.GetKey(KeyCode.RightArrow))
            input_rot = -1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            input_rot = 1;
        else
            input_rot = 0;
        if (usemouse && Input.GetAxis("Mouse X") != 0) {
            input_rot = -Input.GetAxis("Mouse X");
        }
        // Speed
        velocity_rot = input_rot * max_vel_rot;
        rotation += Mathf.PI * 10 / 180 * velocity_rot * Time.deltaTime;
        offset.x = 10 * Mathf.Sin(rotation);
        offset.z = -10 * Mathf.Cos(rotation);
        
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        // transform.position = target.position + offset;

        if (!Input.GetKey(KeyCode.Z)) {
            transform.LookAt(target);
        }
    }
}