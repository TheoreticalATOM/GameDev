using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioClipPlayer))]
public class OnCollisionSoundPlay : MonoBehaviour
{
    public AudioClip[] Clips;
    private AudioClipPlayer mSource;

    private void Awake()
    {
        mSource = GetComponent<AudioClipPlayer>();
    }


    private void OnCollisionEnter(Collision other)
    {
        AudioClip clip = Clips[Random.Range(0, Clips.Length)];
        mSource.PlayClip(clip);
    }
}
