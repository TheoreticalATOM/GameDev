using UnityEngine;
using UnityEngine.Events;

public class EventOnStart : MonoBehaviour {

    public UnityEvent OnStartEvent;

	// Use this for initialization
	void Start ()
    {
        OnStartEvent.Invoke();
	}
}
