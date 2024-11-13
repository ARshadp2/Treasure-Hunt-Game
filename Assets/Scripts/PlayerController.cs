using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float max_vel = 10;
    private float max_accel = 10;
    private float max_vel_rot = 10;

    public AudioClip jump1;
    public AudioClip jump2;
    public AudioClip walk;
    public AudioSource audio;

    public float jumpHeight = 10.0f;
    public float gravity = -9.81f;
    public GameObject projectilePrefab;
    
    private bool standing_check = false;
    private float standing_gap = .1f;
    private float saved_time_stand = 0;

    private CharacterController controller;

    private Vector3 velocity;
    private Vector3 acceleration;
    private float walk_gap = .5f;
    private float saved_time_walk = 0;
    
    private float launch_gap = .5f;
    private float saved_time_launch = 0;

    private float velocity_rot;
    private float acceleration_rot;
    private float current_rotation;
    
    private Vector3 vel_y;
    private float jumps = 2;
    private bool usemouse = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.M)) {
            usemouse = true;
            Debug.Log("mouse");
        }
        else if (Input.GetKey(KeyCode.N))
            usemouse = false;
        // Running
        if (Input.GetKey(KeyCode.LeftShift)) {
            max_vel = 20;
            max_accel = 20;
            walk_gap = .25f;
            jumpHeight = 20f;
        }
        else {
            max_vel = 10;
            max_accel = 10;
            walk_gap = .5f;
            jumpHeight = 10f;
        }
        // Up and Down Movement
        float input_horizontal = 0;
        float input_vertical = 0;
        float input_rot;
        
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
            input_vertical = 0;
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            input_vertical = 1;
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            input_vertical = -1;
        else
            input_vertical = 0;

        // Left and Right Movement
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
            input_horizontal = 0;
        else if (Input.GetKey(KeyCode.A))
            input_horizontal = -1;
        else if (Input.GetKey(KeyCode.D))
            input_horizontal = 1;
        else
            input_horizontal = 0;

        // Right and Left Movement (Rotation)
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
            input_rot = 0;
        else if (Input.GetKey(KeyCode.RightArrow))
            input_rot = 1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            input_rot = -1;
        else
            input_rot = 0;
        if (usemouse)
            input_rot = Input.GetAxis("Mouse X");
        
        // Normal Movement Converter
        current_rotation = transform.rotation.eulerAngles.y;
        Vector3 movement = new Vector3(0, 0, 0);
        movement.x = input_horizontal;
        movement.z = input_vertical;
        movement = movement.normalized;
        acceleration = movement * max_accel;
        velocity += acceleration * Time.deltaTime;
        Vector3 desired_velocity = new Vector3(movement.x, 0, movement.z) * max_vel;
        velocity = Vector3.MoveTowards(velocity, desired_velocity, 25 * Time.deltaTime);
        controller.Move(Quaternion.Euler(0, current_rotation, 0) * velocity * Time.deltaTime);
        if (velocity != new Vector3(0,0,0) && vel_y.y <= .01 && vel_y.y >= -.01 && jumps == 2 && Time.time - saved_time_walk > walk_gap) {
            saved_time_walk = Time.time;
            audio.PlayOneShot(walk, .7f);
        }

        // Rotational Movement Converter 
        velocity_rot = input_rot * max_vel_rot;
        float rotate = 10 * velocity_rot * Time.deltaTime;
        transform.Rotate(0, rotate, 0);
        if (jumps > 0 && Input.GetButtonDown("Jump"))
        {
            standing_check = false;
            Debug.Log("jump");
            if (jumps == 2)
                audio.PlayOneShot(jump1, .7f);
            else
                audio.PlayOneShot(jump2, .7f);
            jumps -= 1;
            vel_y.y = Mathf.Sqrt(-jumpHeight * gravity);
        }
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Z)) && Time.time - saved_time_launch >= launch_gap) {
            saved_time_launch = Time.time;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
        if (vel_y.y < -.1)
            standing_check = false;
        // Apply gravity
        if (standing_check == false && vel_y.y < .00000001 && vel_y.y > -.00000001) {
            standing_check = true;
            saved_time_stand = Time.time;
        }
        vel_y.y += gravity * Time.deltaTime;
        controller.Move(vel_y * Time.deltaTime);
        if (controller.isGrounded == true || (standing_check == true && Time.time - saved_time_walk > standing_gap)) {
            vel_y.y = 0;
            jumps = 2;
        }
    }
    public void dead() {
        Destroy(gameObject);
        SceneManager.LoadScene(3);
    }
}
/*
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(10,100)] float max_vel = 10;
    [SerializeField, Range(10,100)] float max_accel = 10;

    [SerializeField, Range(10,100)] float max_vel_rot = 10;
    [SerializeField, Range(10,100)] float max_accel_rot = 10;

    public float jumpHeight = 10.0f;
    public float gravity = -9.81f;


    private CharacterController controller;

    private float velocity;
    private float acceleration;

    private float velocity_rot;
    private float acceleration_rot;
    
    private Vector3 vel_y;
    private float jumps = 1;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        // Up and Down Movement
        float input = 0;
        float input_rot = 0;
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
            input = 0;
        else if (Input.GetKey(KeyCode.UpArrow))
            input = 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            input = -1;
        else
            input = 0;

        // Right and Left Movement (Rotation)
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
            input_rot = 0;
        else if (Input.GetKey(KeyCode.RightArrow))
            input_rot = 1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            input_rot = -1;
        else
            input_rot = 0;
        
        // Normal Movement Converter
        acceleration = input * max_accel;
        velocity += acceleration * Time.deltaTime;
        float desired_velocity = input * max_vel;
        velocity = Mathf.MoveTowards(velocity, desired_velocity, max_vel * Time.deltaTime);
        controller.Move(transform.forward * velocity * Time.deltaTime);

        // Rotational Movement Converter 
        acceleration_rot = input_rot * max_accel_rot;
        velocity_rot += acceleration_rot * Time.deltaTime;
        float desired_velocity_rot = input_rot * max_vel_rot;
        velocity_rot = Mathf.MoveTowards(velocity_rot, desired_velocity_rot, max_vel_rot * Time.deltaTime);
        float rotation = 10 * velocity_rot * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        if (jumps > 0 && Input.GetButtonDown("Jump"))
        {
            jumps -= 1;
            vel_y.y = Mathf.Sqrt(-jumpHeight * gravity);
        }

        // Apply gravity
        if (controller.isGrounded)
            jumps = 2;
        vel_y.y += gravity * Time.deltaTime;
        float before = transform.position.y;
        controller.Move(vel_y * Time.deltaTime);
        if (transform.position.y == before)
            vel_y.y = 0;
        if (controller.isGrounded)
            jumps = 2;
    }
}
*/