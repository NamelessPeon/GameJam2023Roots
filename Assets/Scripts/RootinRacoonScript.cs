using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RootinRacoonScript : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;
    public float rotateSpeed = 1.0f;
    public float stamina = 100.0f;

    public InputAction movement;
    public InputAction boost;
    public InputAction ping;

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

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = movement.ReadValue<Vector2>();
        rb.transform.Rotate(moveDirection);
        //rb.transform.Rotate(0, 0, rotateSpeed);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
