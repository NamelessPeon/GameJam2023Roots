using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private bool startMoving = false;
    public InputAction jumpInput;
    public InputAction duckInput;
    private float speed = 700.0f;
    private float jumpSpeed = 7.0f;
    private bool canJump = true;
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
    }

    void Update()
    {
        if (startMoving)
        {
            // Move Player
            Vector3 moveVector = new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            moveVector.y = rb.velocity.y;

            // Player Input
            if (jumpInput.ReadValue<float>() != 0 && canJump)
            {
                //Debug.Log("Jump.");
                //Debug.Log(canJump);
                canJump = false;
                moveVector.y = jumpSpeed;
            }
            if (duckInput.ReadValue<float>() != 0)
            {
                Debug.Log("Duck");
            }

            // Apply Movement
            rb.velocity = moveVector;
        }
        else if (duckInput.ReadValue<float>() != 0)
        {
            Debug.Log("Start");
            startMoving = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Kill Box Collision
        if (other.tag == "Kill_Box")
        {
            Debug.Log("You Died, LOL.");
            transform.position = Vector3.up;
        }

        // Vine Collision
        if (other.tag == "Vine_Trigger")
        {
            // Set collision w/ vine variable to true?
            //Debug.Log("Vine_Trigger Enter");
            Vector3 tmpMoveVector = rb.velocity;
            tmpMoveVector.y = 0.0f;
            rb.velocity = tmpMoveVector;

            //
            //rb.useGravity = false;
            canJump = true;
        }

        // Hitbox Collision
        if (other.tag == "Hitbox")
        {
            //Debug.Log("Side Hitbox Enter");
            //Vector3 newPos = rb.transform.position;
            //newPos.x -= 0.2f;
            //rb.MovePosition(newPos);
            canJump = true;
        }
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
