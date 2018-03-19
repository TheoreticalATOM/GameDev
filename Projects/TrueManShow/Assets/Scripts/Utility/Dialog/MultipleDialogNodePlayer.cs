using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class MultipleDialogNodePlayer : MonoBehaviour
{
    public DialogNode[] Nodes;
	public UnityEvent OnCompleted;

    public void Play()
    {
        ASyncRecursivePlaying(0);
    }

    private void ASyncRecursivePlaying(int index)
    {
        if (index >= Nodes.Length)
		{
			OnCompleted.Invoke();
            return;
		}
        Nodes[index].Play(() => ASyncRecursivePlaying(++index));
    }

}
