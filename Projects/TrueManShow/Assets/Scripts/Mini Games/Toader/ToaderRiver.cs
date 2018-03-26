using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToaderRiver : MonoBehaviour
{
    public float InRiverCheckRate = 0.1f;
    ToaderPlayer mPlayer;

    private float mLastTime;
    private void OnTriggerEnter2D(Collider2D other)
    {
        mPlayer = other.GetComponent<ToaderPlayer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!mPlayer.IsOnPlatform)
        {
            /* add a small delay to the check, to allow the next river object 
            to attempt to take effect first */
            if (Time.time - mLastTime > InRiverCheckRate)
                mPlayer.Kill();
        }
        else mLastTime = Time.time;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        mPlayer = null;
    }
}
