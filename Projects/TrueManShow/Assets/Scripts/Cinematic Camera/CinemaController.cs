using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

using SDE;

public class CinemaController : MonoBehaviour
{
    [Header("Placement")]
    public CinemaCam Cinema;
    public Transform AnimationPlayPosition;
    public CinemaDetail AnimationDetail;


    [Header("Events")]
    public UnityEvent OnAnimationComplete;
    public UnityEvent OnAnimationStarted;
    public UnityEvent OnAnimationKeyEvent;

    private void Awake()
    {
        if (!Cinema)
        {
            Cinema = Camera.main.GetComponent<CinemaCam>();
            Assert.IsNotNull(Cinema, name + " : is missing a CinemaCam!");
        }
    }

    public void TransitionToTarget(System.Action ArrivedCallback)
    {
        Cinema.Transition(AnimationPlayPosition, AnimationDetail, ArrivedCallback);
    }
    
    public void PlayAnimation(System.Action playedAnimationCallback)
    {
        OnAnimationStarted.Invoke();
        Cinema.PlayAnimation(AnimationDetail, () =>
        {
            OnAnimationComplete.Invoke();
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
