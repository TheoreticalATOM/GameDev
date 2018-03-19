using UnityEngine;
using UnityEngine.Events;

public class AnimationCompletedTrigger : MonoBehaviour
{
    public UnityEvent OnAnimationComplete;

    public void RegisterCompletion()
    {
        OnAnimationComplete.Invoke();
    }
}
