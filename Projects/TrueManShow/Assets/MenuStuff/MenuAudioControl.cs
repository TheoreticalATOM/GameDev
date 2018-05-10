using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioControl : MonoBehaviour {

    public AudioSource Sourcy;
    public AudioSource SourcyView;
    public AudioClip[] Viewers;
    private AudioClip OneToPlay;
    private bool coroutineStart = false;
    private bool audioPlaying = false;

	void Start ()
    {
        Sourcy.Play();
        
	}
	

	void Update ()
    {
        print(Sourcy.volume);
        if (!coroutineStart)
        {
            Sourcy.volume = 0.4f;
            StartCoroutine(PlayViewCom());
        }
        else
        {
            if (audioPlaying)
            {
                if (!SourcyView.isPlaying)
                {
                    coroutineStart = false;
                    audioPlaying = false;
                }
                    
            }
        }
	}

    IEnumerator PlayViewCom()
    {
        print("ok");
        coroutineStart = true;
        yield return new WaitForSeconds(40);

        Sourcy.volume = 0.1f;

        OneToPlay =  Viewers[Random.Range(0, Viewers.Length)];
        SourcyView.clip = OneToPlay;
        SourcyView.Play();
        audioPlaying = true;

    }
}
