using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APGameController : MonoBehaviour
{
    private AudioSource[] music;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponents<AudioSource>();
        music[1].PlayDelayed(music[0].clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
