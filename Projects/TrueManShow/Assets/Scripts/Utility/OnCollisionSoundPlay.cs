using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioClipPlayer))]
public class OnCollisionSoundPlay : MonoBehaviour
{
    public AudioClip[] Clips;
    private AudioClipPlayer mSource;

    /* Purely to stop the audio from playing the first time it collides.
    As when it drops on to the ground the first time it will play it everywhere */
    private System.Action<Collision> mCollidedAction;

    private void Awake()
    {
        mSource = GetComponent<AudioClipPlayer>();
        mCollidedAction = (other) => { mCollidedAction = OnCollided; };
    }


    private void OnCollisionEnter(Collision other)
    {
        mCollidedAction(other);
    }

    private void OnCollided(Collision other)
    {
        AudioClip clip = Clips[Random.Range(0, Clips.Length)];
        mSource.PlayClip(clip);
    }
}
