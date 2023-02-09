using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;

    public float maxLength;
    public float potatoOffset;
    public float force;

    public GameObject potato_prefab;

    Rigidbody potato;
    Collider potatoCollider;

    public int throwsLeft;

    bool isMouseDown;
    private MasterGameController MasterController;

    public ThrowScore throwScoreObject;
    public gameDifficulty curDifficulty;

    public PotatoYouDiedLoser gameOverScreen;
    public GameObject targetManager;
    private float shotTimer = 2;
    private bool timeLastShot = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen = GameObject.FindGameObjectWithTag("DeadImage").GetComponent<PotatoYouDiedLoser>();
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
        CreatePotato();

        GameObject MCObj = GameObject.FindGameObjectWithTag("MC");
        if (MCObj)
        {
            MasterController = MCObj.GetComponent<MasterGameController>();
            curDifficulty = MasterController.nextDifficulty;
        }
        else
            curDifficulty = gameDifficulty.Medium;

        if (curDifficulty == gameDifficulty.Easy)
            throwsLeft = 10;
        else if (curDifficulty == gameDifficulty.Medium)
            throwsLeft = 7;
        else
            throwsLeft = 5;
        throwScoreObject.UpdateScore(throwsLeft);
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
        if (timeLastShot)
            shotTimer -= Time.deltaTime;
        if (isMouseDown && throwsLeft > 0)
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

        if (shotTimer <= 0 && throwsLeft == 0 && targetManager.GetComponent<TargetManager>().numberOfTargets > 0)
        {
            if (MasterController)
            {
                Destroy(GameObject.FindGameObjectWithTag("MenuMusic"));
                Destroy(MasterController.gameObject);
            }
            SceneManager.LoadScene("MainMenu");
        }
    }

    void Shoot()
    {
        potato.isKinematic = false;
        Vector3 potatoForce = (currentPosition - center.position) * force * -1;
        potato.velocity = potatoForce;
        potato = null;
        potatoCollider = null;
        throwsLeft -= 1;
        throwScoreObject.UpdateScore(throwsLeft);
        Invoke("CreatePotato", 1);
        if (throwsLeft == 0)
            timeLastShot = true;
    }
    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        if (throwsLeft > 0)
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
