using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<MonoBehaviour>
{
    public AudioClip[] sfxList;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
