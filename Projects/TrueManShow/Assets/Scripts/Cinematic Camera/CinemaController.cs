using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[System.Flags]
public enum ECameraLocking
{
    NONE = 0, CAMERA = 2, ROOT_MOTION = 4, MOVEMENT = 8
}

public class CinemaController : MonoBehaviour
{
    [Header("Placement")]
    public CinemaCam Cinema;
    //public Transform AnimationPlayPosition;
    //public CinemaDetail AnimationDetail;

    public ECameraLocking Locking;
    public ECameraLocking Unlocking;

    [Header("Events")]
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
        CameraLockState(Locking, true);
        OnControllerStarted.Invoke();
        RecursiveExecution(0);
    }

    public void RecursiveExecution(int index)
    {
        if(index >= mCinemaComponents.Length)
        {
            CameraLockState(Unlocking, false);
            return;
        }
        mCinemaComponents[index].Respond(Cinema, () => RecursiveExecution(++index));
    }

    private void CameraLockState(ECameraLocking direction, bool value)
    {
        if((direction & ECameraLocking.CAMERA) == ECameraLocking.CAMERA)
        {
            if(value) Cinema.LockCamera();
            else Cinema.UnlockCamera();
        }

        if((direction & ECameraLocking.MOVEMENT) == ECameraLocking.MOVEMENT)
        {

        }

        if((direction & ECameraLocking.ROOT_MOTION) == ECameraLocking.ROOT_MOTION)
            Cinema.CameraAnimator.applyRootMotion = !value;
    }

    // public void TransitionToTarget(System.Action ArrivedCallback)
    // {
    //     Cinema.Transition(AnimationPlayPosition, AnimationDetail, ArrivedCallback);
    // }
    
    // public void PlayAnimation(System.Action playedAnimationCallback)
    // {
    //     OnControllerStarted.Invoke();
    //     Cinema.PlayAnimation(AnimationDetail, () =>
    //     {
    //         OnControllerComplete.Invoke();
    //         if(playedAnimationCallback != null)
    //             playedAnimationCallback();   
    //     }, OnAnimationKeyEvent);
    // }
    // public void PlayAnimation()
    // {
    //     PlayAnimation(null);
    // }

    // public void TransitionToTargetAndAnimate(System.Action ArrivedAndAnimatedCallback)
    // {
    //     TransitionToTarget(() => PlayAnimation(() => TryCallCallback(ArrivedAndAnimatedCallback)));
    // }
    // public void AnimateAndTransitionToTarget(System.Action AnimatedAndArrivedCallback)
    // {
    //     PlayAnimation(() => TransitionToTarget(() => TryCallCallback(AnimatedAndArrivedCallback)));
    // }

    // private static void TryCallCallback(System.Action action)
    // {
    //     if (action != null)
    //         action();
    // }
}
