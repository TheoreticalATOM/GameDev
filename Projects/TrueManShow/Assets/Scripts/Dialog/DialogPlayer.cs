using UnityEngine;
using UnityEngine.Events;

public class DialogPlayer : MonoBehaviour 
{
	public DialogNodeBase Node;
	public UnityEvent OnCompleted;

	public void Play()
	{
		Node.Play(() => OnCompleted.Invoke());
	}
}
