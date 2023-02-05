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

    public InputAction movement;
    public InputAction boost;
    public InputAction ping;

    private float CurrentRot = 0;

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
    void Update()
    {
        moveDirection = movement.ReadValue<Vector2>();

        rb.transform.Rotate(0, 0, rotateSpeed);

        transform.Rotate(0, 0, moveDirection.x * 90 * Time.deltaTime, Space.World);



    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.up * 90 * moveDirection.y;

    }
}
