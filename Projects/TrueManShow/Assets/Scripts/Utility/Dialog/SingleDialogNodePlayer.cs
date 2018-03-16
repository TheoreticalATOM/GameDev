using UnityEngine.Events;
using UnityEngine;

public class SingleDialogNodePlayer : MonoBehaviour
{
    public DialogNode Node;
    public UnityEvent OnDialogFinished;

    public void Play()
    {
        Node.Play(OnDialogFinished.Invoke);
    }
}
