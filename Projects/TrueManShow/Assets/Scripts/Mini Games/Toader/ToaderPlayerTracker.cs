using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ToaderAutoMover))]
public class ToaderPlayerTracker : MonoBehaviour
{
    private ToaderPlayer mPlayer;

    private ToaderAutoMover mMover;

    private void Awake()
    {
        mMover = GetComponent<ToaderAutoMover>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        mPlayer = other.GetComponent<ToaderPlayer>();
		mPlayer.IsOnPlatform = true;

		mMover.OnMoved += MovePlayer;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
		mMover.OnMoved -= MovePlayer;
        
		mPlayer.IsOnPlatform = false;
		mPlayer = null;
    }
	
	private void MovePlayer()
	{
		mPlayer.Move(-transform.right);
	}
}
