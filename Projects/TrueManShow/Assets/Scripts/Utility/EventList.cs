using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventList : MonoBehaviour
{
    public UnityEvent[] Events;
    private int mIndex;

	public void RaiseEvent()
	{
		if(mIndex < Events.Length)
			Events[mIndex++].Invoke();
	}	
}
