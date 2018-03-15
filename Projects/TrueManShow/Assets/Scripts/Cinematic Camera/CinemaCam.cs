using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class CinemaCam : SerializedMonoBehaviour
{
    [Header("Transition")]
    public float TransitionSpeed = 10.0f;
    public float CloseEnoughDistance = 0.2f;
    public float RotatedEnough = 0.2f;
    public Transform LerpingTransform;
    public Transform CameraTransform;

    [Header("Animation and Restriction")]
    public Animator CameraAnimator;
    // public FirstPersonController FirstPerson;
    // public CharacterController Controller;

    private System.Action mAnimationFinishedCallback;
    private UnityEvent mAnimationKeyEvent;

    public void PlayTransitionAnimation(CinemaDetail details, System.Action AnimationFinishedCallback, UnityEvent onAnimationKeyEvent, Transform target = null, System.Action TransitionFinishedCallback = null)
    {
        if (Transition(target, details, () =>
        {
            TryCallCallback(TransitionFinishedCallback);
            PlayAnimation(details, AnimationFinishedCallback, onAnimationKeyEvent);
        }))
            return;

        PlayAnimation(details, AnimationFinishedCallback, onAnimationKeyEvent);
    }
    public void PlayAnimationTransition(CinemaDetail details, System.Action animationFinishedCallback, UnityEvent onAnimationKeyEvent, Transform target = null, System.Action transitionFinishedCallback = null)
    {
        PlayAnimation(details, () =>
        {
            TryCallCallback(animationFinishedCallback);
            Transition(target, details, transitionFinishedCallback);
        }, onAnimationKeyEvent);
    }

    public void PlayAnimation(CinemaDetail details, System.Action animationFinishedCallback, UnityEvent onAnimationKeyEvent)
    {
        mAnimationKeyEvent = onAnimationKeyEvent;
        mAnimationFinishedCallback = () =>
        {
            // UnlockCamera();
            CameraAnimator.applyRootMotion = true;
            animationFinishedCallback();
            // Controller.enabled = !details.LockMovementOnCompletion;
            // FirstPerson.IsWalking = false;
        };

        // LockCamera();
        CameraAnimator.applyRootMotion = false;
        details.SetValue(CameraAnimator);
    }

    public bool Transition(Transform target, CinemaDetail details, System.Action transitionFinishedCallback)
    {
        if (target)
        {
            StartCoroutine(MoveToTarget(target, details, transitionFinishedCallback));
            return true;
        }
        return false;
    }

    public void RegisterAnimationCompletion()
    {
        mAnimationFinishedCallback();
    }

    public void TriggerLoggedKeyEvent()
    {
        mAnimationKeyEvent.Invoke();
    }

    // public void LockCamera()
    // {
    //     FirstPerson.enabled = false;
    //     CameraAnimator.applyRootMotion = false;
    // }

    // public void UnlockCamera()
    // {
    //     FirstPerson.ReInitializeMouseLook();
    //     FirstPerson.enabled = true;
    //     CameraAnimator.applyRootMotion = true;
    // }

    private void Start()
    {
        //UnlockCamera();
    }

    private IEnumerator MoveToTarget(Transform target, CinemaDetail details, System.Action callback)
    {
        // Controller.enabled = false;

        // FirstPerson.enabled = false;
        CameraAnimator.applyRootMotion = true;

        Vector3 targetPos = target.position;
        Quaternion targetRot = target.rotation;
        MovementRestriction restriction = details.Restriction;

        Quaternion targetCamRot = Quaternion.Euler(details.StartRotation);

        Vector3 distance = Vector3.zero;

        // used for incrementally check if all the close enough values are valid
        const int CLOSE_ENOUGH_MAX = 3;
        int closeEnoughCounter = 0;
        do
        {
            closeEnoughCounter = 0;

            Vector3 pos = LerpingTransform.position;
            Quaternion rot = LerpingTransform.rotation;
            Quaternion camRot = CameraTransform.localRotation;

            float dSpeed = TransitionSpeed * Time.deltaTime;

            // Movement
            TryLerpValue(ref pos.x, ref distance.x, targetPos.x, dSpeed, restriction, MovementRestriction.PX);
            TryLerpValue(ref pos.y, ref distance.y, targetPos.y, dSpeed, restriction, MovementRestriction.PY);
            TryLerpValue(ref pos.z, ref distance.z, targetPos.z, dSpeed, restriction, MovementRestriction.PZ);
            ValidateBoolCounter(ref closeEnoughCounter, distance.sqrMagnitude < (CloseEnoughDistance * CloseEnoughDistance));

            // Rotation
            rot = Quaternion.Slerp(rot, targetRot, dSpeed);
            ValidateBoolCounter(ref closeEnoughCounter, Quaternion.Angle(rot, targetRot) < RotatedEnough);

            // Camera Rotation
            camRot = Quaternion.Slerp(camRot, targetCamRot, dSpeed);
            ValidateBoolCounter(ref closeEnoughCounter, Quaternion.Angle(camRot, targetCamRot) < RotatedEnough);

            // Set the values
            LerpingTransform.rotation = rot;
            LerpingTransform.position = pos;
            CameraTransform.localRotation = camRot;

            yield return null;
        } while (closeEnoughCounter < CLOSE_ENOUGH_MAX);

        //Controller.enabled = !details.LockMovementOnCompletion;
        CameraAnimator.applyRootMotion = true;
        //UnlockCamera();
        callback();
    }

    private void TryLerpValue(ref float value, ref float distance, float target, float speed, MovementRestriction restriction, MovementRestriction targetRestriction)
    {
        if ((restriction & targetRestriction) != targetRestriction)
        {
            value = Mathf.Lerp(value, target, speed);
            distance = value - target;
        }
    }


    private void ValidateBoolCounter(ref int counter, bool result)
    {
        counter = (result) ? ++counter : --counter;
    }

    private void TryCallCallback(System.Action action)
    {
        if (action != null)
            action();
    }
}
