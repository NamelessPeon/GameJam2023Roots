using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpudacusPlayer : MonoBehaviour
{
    private float rotateDir = 0f;
    public float shieldRotSpeed = 100f;
    private GameObject Shield;
    public enum shieldType {Potato, Carrot};
    public shieldType curShield = shieldType.Potato;
    private Renderer shieldRend;
    [SerializeField] Material potatoShield_MAT;
    [SerializeField] Material carrotShield_MAT;
    
    // Start is called before the first frame update
    void Start()
    {
        Shield = transform.GetChild(0).gameObject;
        shieldRend = Shield.transform.GetChild(0).GetComponent<Renderer>();
        shieldRend.material = potatoShield_MAT;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Shield.transform.rotation.z);
        Debug.Log(curShield);
        if (Input.GetKey(KeyCode.D) && Shield.transform.rotation.z > -0.5)
            Shield.transform.Rotate(new Vector3(0, 0, shieldRotSpeed * -1) * Time.deltaTime);
        else if (Input.GetKey(KeyCode.A) && Shield.transform.rotation.z < 0.5)
            Shield.transform.Rotate(new Vector3(0, 0, shieldRotSpeed * 1) * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (curShield == shieldType.Potato)
            {
                curShield = shieldType.Carrot;
                shieldRend.material = carrotShield_MAT;
            }
            else if (curShield == shieldType.Carrot)
            {
                curShield = shieldType.Potato;
                shieldRend.material = potatoShield_MAT;
            }
        }
    }

//    public void rotateShield(InputAction.CallbackContext context)
//    {
//        Debug.Log("Hello");
//        rotateDir = context.ReadValue<float>();
//    }
}
