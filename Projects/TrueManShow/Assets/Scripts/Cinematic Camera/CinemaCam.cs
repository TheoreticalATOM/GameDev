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

    public UnityEvent OnCameraLocked;
    public UnityEvent OnCameraUnlocked;
    public UnityEvent OnMovementLocked;
    public UnityEvent OnMovementUnlocked;

    private Queue<UnityEvent> mKeyEvents;
    private Stack<System.Action> mAnimationCompleteEvents;

    // _______________________________________________________
    // @ Locking
    public void LockCamera()
    {
        OnCameraLocked.Invoke();
    }
    public void UnlockCamera()
    {
        OnCameraUnlocked.Invoke();
    }
    public void LockMovement()
    {
        OnMovementLocked.Invoke();
    }
    public void UnlockMovement()
    {
        OnMovementUnlocked.Invoke();
    }
    // _______________________________________________________
    // @ Events
    public void RegisterAnimationCompletion()
    {
        mAnimationCompleteEvents.PopAll(popped => popped.Invoke());
        mKeyEvents.Clear(); // when it is completed, remove all the logged key events
    }

    public void RegisterKeyEvent()
    {
        // do only one item at a time, allowing for calling multiple events iteratively
        mKeyEvents.Dequeue().Invoke();
    }

    // + Adding Event Triggers
    public void AddKeyEventTrigger(UnityEvent e)
    {
        mKeyEvents.Enqueue(e);
    }
    public void AddKeyEventTriggers(UnityEvent[] e)
    {
        mKeyEvents.Enqueue(e);
    }

    public void AddAnimationCompleteTrigger(System.Action e)
    {
        mAnimationCompleteEvents.Push(e);
    }

    private void Awake()
    {
        mKeyEvents = new Queue<UnityEvent>();
        mAnimationCompleteEvents = new Stack<System.Action>();
    }
}
