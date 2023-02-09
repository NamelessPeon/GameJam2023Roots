using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RootinRacoonScript : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 5f;
    public float rotateSpeed = 1.0f;
    public float stamina = 100.0f;
    public float boostAmount = 5f; 
    public float rateOfStaminaLoss = 1.0f;
    public float staminaRegen = 0.01f; 

    public InputAction movement;
    public InputAction boost;
    public InputAction ping;

    private float boostButton;
    public bool play = false;

    Vector2 moveDirection= Vector2.zero;

    private void OnEnable()
    {
        movement.Enable();
        boost.Enable();
        ping.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        boost.Disable();
        ping.Disable();
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (play)
        {
            // Movement
            moveDirection = movement.ReadValue<Vector2>();

            transform.Rotate(0, 0, rotateSpeed);

            transform.Rotate(0, 0, moveDirection.x * 200 * Time.deltaTime, Space.World);

            rb.AddRelativeForce(new Vector3(0, 0, moveDirection.y * moveSpeed * Time.deltaTime));


            // Boost
            boostButton = boost.ReadValue<float>();
            //Debug.Log(boostButton);
            if (boostButton == 1)
            {
                if (stamina > 0)
                {
                    stamina -= rateOfStaminaLoss;
                    transform.Rotate(0, 0, 6 * rotateSpeed);
                    rb.AddRelativeForce(new Vector3(0, 0, boostAmount * moveSpeed * Time.deltaTime));
                }
            }
            if (stamina < 100)
            {
                stamina += staminaRegen;
            }
        }
    }

    
}
