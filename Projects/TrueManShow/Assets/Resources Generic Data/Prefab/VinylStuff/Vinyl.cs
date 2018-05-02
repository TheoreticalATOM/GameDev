using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vinyl : MonoBehaviour
{
    private AudioSource musicSource;
    public AudioClip musicClip;

    private void Awake()
    {
        if (!musicSource)
            musicSource = GetComponent<AudioSource>();
    }

    public void OnPlay()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public virtual void OnPaused()
    {
        musicSource.Pause();
    }

    public void OnEnded()
    {
        musicSource.Stop();
    }
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
