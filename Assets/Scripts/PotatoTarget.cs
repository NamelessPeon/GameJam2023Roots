using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotatoTarget : MonoBehaviour
{
    public TargetManager targetManagerGameObject;

    void Start()
    {
        targetManagerGameObject = GameObject.FindGameObjectWithTag("TargetManager").GetComponent<TargetManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Potato")
        {
            print("something");
            targetManagerGameObject.numberOfTargets -= 1;
            Destroy(this.gameObject);
        }
    }
}
