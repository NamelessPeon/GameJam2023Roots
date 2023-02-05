using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool startMoving = false;
    public InputAction jumpInput;
    public InputAction duckInput;
    public Camera cam;
    public float speed = 700.0f;
    public float jumpSpeed = 7.0f;
    private bool canJump = true;
    private bool canDuck = true;
    public GameObject GameController;

    private AudioSource[] sound;

    Rigidbody rb;

    private void OnEnable()
    {
        jumpInput.Enable();
        duckInput.Enable();
    }

    private void OnDisable()
    {
        jumpInput.Disable();
        duckInput.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponents<AudioSource>();
    }

    void FixedUpdate()
    {
        if (startMoving)
        {

            // Rotation b/c its funny
            //transform.Rotate(new Vector3(0, 0, 15 * Time.deltaTime));

            // Move Player
            Vector3 moveVector = new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            moveVector.y = rb.velocity.y;

            // Player Input
            if (jumpInput.ReadValue<float>() != 0 && canJump)
            {
                canJump = false;
                moveVector.y = jumpSpeed * Time.deltaTime;
            }
            if (duckInput.ReadValue<float>() != 0)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                cam.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                moveVector.y -= 7.0f * Time.deltaTime;
                moveVector.x *= 0.90f;
                sound[0].Play();
            }
            else
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                cam.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }

            // Apply Movement
            rb.velocity = moveVector;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // Hitbox Collision
        if (other.tag == "Hitbox")
        {
            canJump = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Kill Box Collision
        if (other.tag == "Kill_Box")
        {
            Debug.Log("You Died, LOL.");
            GameController.GetComponent<GameController>().GameOver();
        }

        // Vine Collision
        if (other.tag == "Vine_Trigger")
        {
            canJump = true;
            sound[1].Play();
        }

        /*/ Hitbox Collision
        if (other.tag == "Hitbox")
        {
            //Debug.Log("Side Hitbox Enter");
            //Vector3 newPos = rb.transform.position;
            //newPos.x -= 0.2f;
            //rb.MovePosition(newPos);
            canJump = true;
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit Collision.");

        // Exit Vine Collision
        if (other.tag == "Vine_Trigger")
        {
            //Debug.Log("Vine_Trigger Exit");
            //rb.useGravity = true;
            canJump = false;
        }
    }
}
