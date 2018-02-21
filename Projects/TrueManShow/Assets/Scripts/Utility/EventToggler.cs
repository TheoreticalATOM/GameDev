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
		if(mToggle) A.Invoke();
		else B.Invoke();
	}
}
