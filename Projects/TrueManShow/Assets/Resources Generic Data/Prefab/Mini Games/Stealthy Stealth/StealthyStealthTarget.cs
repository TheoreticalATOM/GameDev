using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StealthyStealthTarget : MonoBehaviour
{
    public DialogPlayer DialogJustBeforeKill;
	public float LevelEndDelayInSeconds;
    public StealthyStealth Game;

	public DialogPlayer DialogCompletingGame;
    private Animator mAnimator;

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
	    DialogJustBeforeKill.OnCompleted.AddListener(() =>
	    {
		    DialogCompletingGame.Play();
	    });
    }

	public void KillTarget()
	{
		mAnimator.SetBool("kill", true);
		StopAllCoroutines();
		StartCoroutine(DelayRoutine());
	}

    private void OnEnable()
    {
        mAnimator.SetBool("kill", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	    StealthyStealthPlayer player = other.GetComponent<StealthyStealthPlayer>();
	    if (player) player.LockPlayer(true);
	    DialogJustBeforeKill.Play();
    }

	private void OnDestroy()
	{
		DialogJustBeforeKill.OnCompleted.RemoveAllListeners();
	}

	IEnumerator DelayRoutine()
	{
		yield return new WaitForSeconds(LevelEndDelayInSeconds);
		Game.NextLevel();
	}
}
