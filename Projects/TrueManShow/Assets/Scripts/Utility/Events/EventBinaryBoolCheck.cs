using UnityEngine;
using UnityEngine.Events;

public class EventBinaryBoolCheck : MonoBehaviour
{
	public bool A;
	public bool B;

	public UnityEvent OnBothAreTrue;
	public UnityEvent OnOneIsFalse;
	

	public void SetA(bool state)
	{
		A = state;
		Validate();
	}

	public void SetB(bool state)
	{
		B = state;
		Validate();
	}

	private void Validate()
	{
		Debug.Log(A + " : " + B);
		if(A && B) OnBothAreTrue.Invoke();
		else OnOneIsFalse.Invoke();
	}
}
