using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{
    public bool startMoving = false;
    public InputAction flashlightInput;
    public InputAction lookAround;
    public Camera cam;
    public GameObject enemy;
    public GameObject target;
    public int numLanes = 3;
    float angle;
    private List<GameObject> enemiesList = new List<GameObject>();
    private List<GameObject> targetList = new List<GameObject>();

    private float lookDirection;
    private bool pressedOnce = false;
    private bool lookInputPressed = false;
    Rigidbody rb;

    private void OnEnable()
    {
        flashlightInput.Enable();
        lookAround.Enable();
    }

    private void OnDisable()
    {
        flashlightInput.Disable();
        lookAround.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float angle = 180.0f / (float)numLanes;

        for (int i = 0; i < numLanes; i++)
        {
            // Spawn Enemy
            Vector3 pos = new Vector3(Mathf.Sin(Mathf.Deg2Rad * (angle - (angle * i))), 0.0f, Mathf.Cos(Mathf.Deg2Rad * (angle - (angle * i))));
            Vector3 targetPos = pos;
            pos *= 25.0f;
            Debug.Log(pos);
            GameObject newEnemy = Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0));
            newEnemy.GetComponent<EnemyMovement>().lane = i;
            enemiesList.Add(newEnemy);

            // Spawn Target
            targetPos *= Random.Range(5, 20);
            targetList.Add(Instantiate(target, targetPos, Quaternion.Euler(0, 0, 0)));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            // Look Direction
            float lookInput = lookAround.ReadValue<float>();
            angle = 180.0f / (float)numLanes;

            if (lookInput != 0)
            {
                
                if (!lookInputPressed)
                {
                    
                    lookDirection += lookAround.ReadValue<float>();
                    if (lookDirection < 0)
                    {
                        lookDirection = (float)numLanes - 1.0f;
                    }
                    else if (lookDirection >= (float)numLanes)
                    {
                        lookDirection = 0.0f;
                    }
                }
                lookInputPressed = true;
            }
            else
            {
                lookInputPressed = false;
            }
            

            // Rotate Player
            transform.rotation = Quaternion.Euler(0.0f, -angle + (angle * lookDirection), 0.0f); // only for 3 lanes
            //Debug.Log(transform.rotation);

            // Flashight Mechanic
            if (flashlightInput.ReadValue<float>() != 0)
            {
                if (!pressedOnce)
                {
                    Debug.Log(angle * lookDirection);
                    foreach (var enemyUnit in enemiesList)
                    {
                        EnemyMovement enemyScript = enemyUnit.GetComponent<EnemyMovement>();
                        if (enemyScript.lane == lookDirection)
                        {
                            if (enemyScript.lane == lookDirection)
                            {
                                enemyScript.StopMoving();
                            }
                        }
                    }
                    pressedOnce = true;
                }
            }
            else
            {
                pressedOnce = false;
            }

        }
        else if (flashlightInput.ReadValue<float>() != 0)
        {
            /* START */
            Debug.Log(enemiesList.Count);
            startMoving = true;
            foreach (var enemyUnit in enemiesList)
            {
                EnemyMovement enemyScript = enemyUnit.GetComponent<EnemyMovement>();
                enemyScript.canMove = true;
            }

            pressedOnce = true;
        }

    }
}
