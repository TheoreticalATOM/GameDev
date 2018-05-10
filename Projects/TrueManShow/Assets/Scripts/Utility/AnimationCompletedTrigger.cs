using UnityEngine;
using UnityEngine.Events;

public class AnimationCompletedTrigger : MonoBehaviour
{
    public UnityEvent OnAnimationComplete;
    public UnityEvent[] OnKeyEventsTriggered;

    private int mKeyIndex = 0;
    
    public void RegisterCompletion()
    {
        OnAnimationComplete.Invoke();
    }

    public void RegisterKeyEvent()
    {
        if (mKeyIndex >= OnKeyEventsTriggered.Length)
            return;
        
        OnKeyEventsTriggered[mKeyIndex++].Invoke();
    }

    public void ResetKeyEvents()
    {
        mKeyIndex = 0;
    }
}
