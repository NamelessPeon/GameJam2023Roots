using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;

    public float maxLength;

    public GameObject potato_prefab;

    Rigidbody potato;
    Collider potatoCollider;

    public float potatoOffset;

    public float force;

    bool isMouseDown;
    bool isMouseUp;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        CreatePotato();
    }

    void CreatePotato()
    {
        potato = Instantiate(potato_prefab).GetComponent<Rigidbody>();
        potatoCollider = potato.GetComponent<Collider>();
        potatoCollider.enabled = false;
        potato.isKinematic = true;
        potato.transform.position = center.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);

            SetStrips(currentPosition);
        }
        else
        {
            ResetStrips();
        }

        if (potatoCollider)
        {
            potatoCollider.enabled = true;
        }
    }

    void Shoot()
    {
        potato.isKinematic = false;
        Vector3 potatoForce = (currentPosition - center.position) * force * -1;
        potato.velocity = potatoForce;

        potato = null;
        potatoCollider = null;
        Invoke("CreatePotato", 1);
    }
    private void OnMouseDown()
    {
        isMouseDown = true;
        isMouseUp = false;
    }

    private void OnMouseUp()
    {
        isMouseUp = true;
        isMouseDown = false;
        Shoot();
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 postion)
    {
        lineRenderers[0].SetPosition(1, postion);
        lineRenderers[1].SetPosition(1, postion);

        if (isMouseDown)
        {
            Vector3 dir = postion - center.position;
            if (potato)
            {
                potato.transform.position = postion + dir.normalized * potatoOffset;
                potato.transform.right = -dir.normalized;
            }
        }
    }
}
