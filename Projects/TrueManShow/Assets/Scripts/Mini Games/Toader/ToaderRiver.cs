using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToaderRiver : MonoBehaviour
{
    private static float LOGGED_TIME = 0.0f;

    public float InRiverCheckRate = 0.1f;
    ToaderPlayer mPlayer;

    private float mLastTime;
    private void OnTriggerEnter2D(Collider2D other)
    {
        UpdateDelay();
        mPlayer = other.GetComponent<ToaderPlayer>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        /* add a small delay to the check, to allow the next river object 
        to attempt to take effect first */
        if (Time.time - LOGGED_TIME > InRiverCheckRate)
            mPlayer.Kill();

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        mPlayer = null;
    }

    public static void UpdateDelay()
    {
        LOGGED_TIME = Time.time;
    }
}
