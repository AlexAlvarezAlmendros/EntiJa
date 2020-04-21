using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioClip[] musicList;
    private AudioClip currentClip;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(int numA)
    {
        currentClip = musicList[numA];

        audioSource.clip = currentClip;
        audioSource.Play(0);
    }

    public void PauseMusic(int numA)
    {
        audioSource.Pause();
    }
}
