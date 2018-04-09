using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventToggler : MonoBehaviour 
{
	public UnityEvent A;
	public UnityEvent B;

	private bool mToggle;

	public void Toggle()
	{
		mToggle = !mToggle;
		if(mToggle) ToggleA();
		else ToggleB();
	}

	protected virtual void ToggleA()
	{
		A.Invoke();
	}

	protected virtual void ToggleB()
	{
		B.Invoke();
	}
}
