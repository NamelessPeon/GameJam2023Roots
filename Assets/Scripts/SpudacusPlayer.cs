using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpudacusPlayer : MonoBehaviour
{
    public float shieldRotSpeed = 100f;
    private GameObject Shield;
    private float RotateDirection = 0;
    public enum shieldType {Potato, Carrot};
    public shieldType curShield = shieldType.Potato;

    private GameObject PotatoShield;
    private GameObject CarrotShield;

    public InputAction SheildRotate;
    public InputAction SheildSwap;

    bool ShieldChange = false;
    // Start is called before the first frame update
    void Start()
    {
        Shield = transform.GetChild(0).gameObject;
        PotatoShield = Shield.transform.GetChild(0).gameObject;
        CarrotShield = Shield.transform.GetChild(1).gameObject;
        
    }

    private void OnEnable()
    {
        SheildRotate.Enable();
        SheildSwap.Enable();
    }

    private void OnDisable()
    {
        SheildRotate.Disable();
        SheildSwap.Disable();
    }
    // Update is called once per frame
    void Update()
    {

        RotateDirection = SheildRotate.ReadValue<float>();


        if (SheildSwap.ReadValue<float>() == 0 && ShieldChange == true)
            ShieldChange = false;

        if(SheildSwap.ReadValue<float>() != 0 && ShieldChange == false)
        {
            if (curShield == shieldType.Potato)
            {
                curShield = shieldType.Carrot;
                CarrotShield.SetActive(true);
                PotatoShield.SetActive(false);
            }
            else if (curShield == shieldType.Carrot)
            {
                curShield = shieldType.Potato;
                CarrotShield.SetActive(false);
                PotatoShield.SetActive(true);
            }

            ShieldChange = true;
        }

    }

    private void FixedUpdate()
    {
        Shield.transform.Rotate(new Vector3(0, 0, shieldRotSpeed * RotateDirection) * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Potato_Projectile(Clone)" && curShield == shieldType.Potato)
            Destroy(other.gameObject);
        if (other.gameObject.name == "Carrot_Projectile(Clone)" && curShield == shieldType.Carrot)
            Destroy(other.gameObject);
    }
}
