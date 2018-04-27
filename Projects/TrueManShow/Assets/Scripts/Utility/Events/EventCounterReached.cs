using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class EventCounterReached : SerializedMonoBehaviour
{
	[MinValue(1)] public int TargetCounts;
	public UnityEvent OnTargetMet;
	public UnityEvent OnTargetDecremented;
	
	public int CurrentCount { get; private set; }

	public void ResetCounter()
	{
		CurrentCount = 0;
	}

	public void Decrement()
	{
		CurrentCount = Math.Max(CurrentCount - 1, 0);
		OnTargetDecremented.Invoke();
	}
	public void Increment()
	{
		CurrentCount = Mathf.Min(CurrentCount + 1, TargetCounts);
		
		// if current count has reached target count, then fire off event
		if(CurrentCount == TargetCounts)
			OnTargetMet.Invoke();
	}
}
