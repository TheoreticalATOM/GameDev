using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

using SDE;

public class CinemaCam : SerializedMonoBehaviour
{
    [Header("Transition")]
    public float TransitionSpeed = 10.0f;
    public Transform LerpingTransform;
    public Transform CameraTransform;

    [Header("Animation and Restriction")]
    public Animator CameraAnimator;

    public UnityEvent OnLocked;
    public UnityEvent OnUnlocked;

    private Stack<UnityEvent> mKeyEvents;
    private Stack<System.Action> mAnimationCompleteEvents;
    
    // _______________________________________________________
    // @ Locking
    public void LockCamera()
    {
        OnLocked.Invoke();
    }
    public void UnlockCamera()
    {
        OnUnlocked.Invoke();
    }

    // _______________________________________________________
    // @ Events
    public void RegisterAnimationCompletion()
    {
        mAnimationCompleteEvents.PopAll(popped => popped.Invoke());
    }

    public void RegisterKeyEvent()
    {
        mKeyEvents.PopAll(popped => popped.Invoke());
    }

    // + Adding Event Triggers
    public void AddKeyEventTrigger(UnityEvent e)
    {
        mKeyEvents.Push(e);
    }

    public void AddAnimationCompleteTrigger(System.Action e)
    {
        mAnimationCompleteEvents.Push(e);
    }

    private void Awake()
    {
        mKeyEvents = new Stack<UnityEvent>();
        mAnimationCompleteEvents = new Stack<System.Action>();
    }
}
