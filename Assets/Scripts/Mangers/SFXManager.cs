using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    public AudioClip[] sfxList;
    private AudioClip currentClip;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSourceloop;

    void Start()
    {

    }

    public void PlayAudio(int numA)
    {
        currentClip = sfxList[numA];

        if (audioSource1.isPlaying)
        {
            if (audioSource2.isPlaying)
            {
                audioSource3.clip = currentClip;
                audioSource3.Play(0);
            }
            else {
                audioSource2.clip = currentClip;
                audioSource2.Play(0);
            }
        }
        else {
            audioSource1.clip = currentClip;
            audioSource1.Play(0);
        }
    }

    public void PlayInLoop(int numA)
    {
        currentClip = sfxList[numA];
        audioSourceloop.clip = currentClip;
        audioSourceloop.Play(0);
    }

    public void PauseLoop()
    {
        audioSourceloop.Pause();
    }
}
