using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class CinemaController : MonoBehaviour
{
    [Header("Placement")]
    public CinemaCam Cinema;
    public Transform AnimationPlayPosition;
    public CinemaDetail AnimationDetail;

    [Header("Events")]
    public UnityEvent OnControllerComplete;
    public UnityEvent OnControllerStarted;
    //public UnityEvent OnAnimationKeyEvent;

    private CinematicControllerComponent[] mCinemaComponents;

    private void Awake()
    {
        if (!Cinema)
        {
            Cinema = Camera.main.GetComponent<CinemaCam>();
            Assert.IsNotNull(Cinema, name + " : is missing a CinemaCam!");
        }
        mCinemaComponents = GetComponents<CinematicControllerComponent>();
    }

    public void Execute()
    {
        OnControllerStarted.Invoke();
        RecursiveExecution(0);
    }

    public void RecursiveExecution(int index)
    {
        if(index >= mCinemaComponents.Length)
        {
            OnControllerComplete.Invoke();
            return;
        }

        mCinemaComponents[index].Respond(Cinema, () => RecursiveExecution(++index));
    }

    public void TransitionToTarget(System.Action ArrivedCallback)
    {
        Cinema.Transition(AnimationPlayPosition, AnimationDetail, ArrivedCallback);
    }
    
    public void PlayAnimation(System.Action playedAnimationCallback)
    {
        OnControllerStarted.Invoke();
        Cinema.PlayAnimation(AnimationDetail, () =>
        {
            OnControllerComplete.Invoke();
            if(playedAnimationCallback != null)
                playedAnimationCallback();   
        }, OnAnimationKeyEvent);
    }
    public void PlayAnimation()
    {
        PlayAnimation(null);
    }

    public void TransitionToTargetAndAnimate(System.Action ArrivedAndAnimatedCallback)
    {
        TransitionToTarget(() => PlayAnimation(() => TryCallCallback(ArrivedAndAnimatedCallback)));
    }
    public void AnimateAndTransitionToTarget(System.Action AnimatedAndArrivedCallback)
    {
        PlayAnimation(() => TransitionToTarget(() => TryCallCallback(AnimatedAndArrivedCallback)));
    }

    private static void TryCallCallback(System.Action action)
    {
        if (action != null)
            action();
    }
}
