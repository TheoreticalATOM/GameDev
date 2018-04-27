using System.Collections;
using System.Collections.Generic;
using SDE.Data;
using UnityEngine;

public class StealthyStealthCamera : MonoBehaviour
{
    public float MaxRotation;
    public float Speed;
    public LayerMask CollisionMask;
    public float DamageAmount;
    
    [Header("Audio")]
    public RuntimeSet AudioPoolSet;
    public AudioClip DetectionClip;

    private StealthyStealthPlayer mPlayer;
    private AudioPool mPool;
    private AudioSource mLoopedSource;
    
    private void Start()
    {
        mPool = AudioPoolSet.GetFirst<AudioPool>();
    }


    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, MaxRotation * Mathf.Sin(Time.time * Speed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        mPlayer = other.GetComponent<StealthyStealthPlayer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Vector3 thisPos = transform.position;
        Vector3 otherPos = other.transform.position;
        Vector3 towards = otherPos - thisPos;
		
		RaycastHit2D visibleCollider = Physics2D.Raycast(thisPos, towards.normalized, towards.sqrMagnitude, CollisionMask);
        if (visibleCollider.collider && visibleCollider.collider.gameObject == mPlayer.gameObject)
        {
            // if there is no loop source, then get one and loop the clip
            if (!mLoopedSource) mLoopedSource = mPool.GetLoop(DetectionClip);
            // if there is already one, but it is stopped, then play it
            else if(!mLoopedSource.isPlaying) 
                mLoopedSource.Play();
            
            mPlayer.Damage(DamageAmount * Time.deltaTime);
        }
        else if (mLoopedSource)
        {
            mLoopedSource.Stop();
            mLoopedSource = null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        mPlayer = null;
    }
}

