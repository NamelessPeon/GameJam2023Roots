using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab;
    Rigidbody target;
    Collider targetCollider;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<5; i++)
        {
            target = Instantiate(targetPrefab).GetComponent<Rigidbody>();
            targetCollider = target.GetComponent<Collider>();
            target.position = new Vector3(Random.Range(10, 20), Random.Range(1, 6), 0);
        }
    }
}
