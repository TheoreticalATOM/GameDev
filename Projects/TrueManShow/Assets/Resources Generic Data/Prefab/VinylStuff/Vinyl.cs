using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vinyl : MonoBehaviour
{
    private AudioSource musicSource;
    public AudioClip musicClip;
    public Rotator Rotator;
    
    
    private void Awake()
    {
        if (!musicSource)
            musicSource = GetComponent<AudioSource>();
    }

    public void OnPlay()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
        
        Rotator.SetRotate(true);
    }

    public virtual void OnPaused()
    {
        Rotator.SetRotate(false);
        musicSource.Pause();
    }

    public void OnEnded()
    {
        Rotator.SetRotate(false);
        musicSource.Stop();
    }
}
