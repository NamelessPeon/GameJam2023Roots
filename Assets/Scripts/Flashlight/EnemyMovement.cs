using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public GameObject player;
    public int lane = 0;
    public float speed = 200;
    public bool canMove = false;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            
            Vector3 moveVector = -transform.position;
            moveVector.Normalize();

            moveVector *= speed * Time.deltaTime;

            //Debug.Log(moveVector);
            rb.velocity = moveVector;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void StopMoving()
    {
        Debug.Log("Enemy STOP.");
        canMove = false;
    }
}
