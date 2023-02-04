using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    private float lifespan = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, Speed) * Time.deltaTime);
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SpudacusPlayer")
        {
            Destroy(this.gameObject);
        }
    }
}
