using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public AudioClip soundExplosion;
    AudioSource myAudio;
    public static soundManager instance;

    void Awake()
    {
        if (soundManager.instance == null)
        {
            soundManager.instance = this;
        }
    }

    void Start()
    {
        myAudio = this.gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound()
    {
        myAudio.PlayOneShot(soundExplosion);
    }

    void Update()
    {
        
    }
}
