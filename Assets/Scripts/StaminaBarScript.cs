using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Questions? Call Robert Howard/NamelessPeon on discord

public class StaminaBarScript : MonoBehaviour
{
    private Image staminaBar;
    public float maxStamina = 100f;
    public float curStamina;
    //Need reference to Raccon Player Script
    //EX: PlayerController_Script player;

    private void Start()
    {
        staminaBar = GetComponent<Image>();
        // player = FindObjectOfType<PlayerController_Script>();
    }

    private void Update()
    {
        // curStamina = player.stamina
        staminaBar.fillAmount = curStamina / maxStamina;
    }
}
