using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class CountdownEvent : MonoBehaviour
{
	public float CountDownInSeconds;
    public UnityEvent OnTimeReached;
	private Coroutine mCountRoutine;

	public virtual void StartTimer()
	{
		mCountRoutine = StartCoroutine(CountDownRoutine());
	}

	public void StopTimer()
	{
		StopCoroutine(mCountRoutine);
	}

    private IEnumerator CountDownRoutine()
    {
		yield return new WaitForSeconds(CountDownInSeconds);
		OnTimeReached.Invoke();
    }
}
