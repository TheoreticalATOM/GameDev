using UnityEngine.Events;
using SDE;

public class CCCAnimation : CinematicControllerComponent
{
    public ScriptableAnimation AnimationDetail;

    public UnityEvent OnAnimationStarted;
    public UnityEvent OnKeyEventTriggered;
    public UnityEvent OnAnimationCompleted;

    public override void Respond(CinemaCam camera, System.Action onCompletionCallback)
    {
        OnAnimationStarted.Invoke();

        camera.AddAnimationCompleteTrigger(() =>
        {
            onCompletionCallback.TryInvoke();
            OnAnimationCompleted.Invoke();
        });
        camera.AddKeyEventTrigger(OnKeyEventTriggered);
        AnimationDetail.SetValue(camera.CameraAnimator);
    }
}
