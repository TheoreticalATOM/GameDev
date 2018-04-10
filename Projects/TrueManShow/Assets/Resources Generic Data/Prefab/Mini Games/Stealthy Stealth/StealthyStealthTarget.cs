using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StealthyStealthTarget : MonoBehaviour
{
	public float LevelEndDelayInSeconds;
    public StealthyStealth Game;
    public AudioSource GunShot;
    private Animator mAnimator;

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        mAnimator.SetBool("kill", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StealthyStealthPlayer player = other.GetComponent<StealthyStealthPlayer>();
        if (player)
            player.LockPlayer(true);

        mAnimator.SetBool("kill", true);
        GunShot.Play();
		StopAllCoroutines();
        StartCoroutine(DelayRoutine());
    }

	IEnumerator DelayRoutine()
	{
		yield return new WaitForSeconds(LevelEndDelayInSeconds);
		Game.NextLevel();
	}
}
