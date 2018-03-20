using UnityEngine.Events;
using SDE;

public class CCCAnimation : CinematicControllerComponent
{
    public ScriptableAnimation AnimationDetail;

    public UnityEvent OnAnimationStarted;
    public UnityEvent[] OnKeyEventsTriggered;
    public UnityEvent OnAnimationCompleted;

    public override void Respond(CinemaCam camera, System.Action onCompletionCallback)
    {
        OnAnimationStarted.Invoke();

        camera.AddAnimationCompleteTrigger(() =>
        {
            onCompletionCallback.TryInvoke();
            OnAnimationCompleted.Invoke();
        });

        if(OnKeyEventsTriggered.Length > 0)
            camera.AddKeyEventTriggers(OnKeyEventsTriggered);
        AnimationDetail.SetValue(camera.CameraAnimator);
    }
}
