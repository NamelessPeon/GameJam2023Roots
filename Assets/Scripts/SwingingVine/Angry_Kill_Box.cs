using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angry_Kill_Box : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
        if (playerScript.startMoving)
        {
            rb.velocity = new Vector3((playerScript.speed - 5) * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}
