using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CCCLookAt : CinematicControllerComponent
{
    public Transform LookAtTarget;
    public float TransitionSpeed = 5.0f;
    public float CloseEnoughRotation;
    public UnityEvent OnLookingAt;

    private delegate IEnumerator DelLookAtRoutine(CinemaCam camera, Action onCompletedCallback);
    private DelLookAtRoutine mRoutineAction;

    private void Start()
    {
        if (LookAtTarget.gameObject.isStatic)
            mRoutineAction = LookAtRoutineStatic;
        else
            mRoutineAction = LookAtRoutine;
    }

    public override void Respond(CinemaCam camera, Action onCompletedCallback)
    {
        StartCoroutine(LookAtRoutine(camera, onCompletedCallback));
    }

    private IEnumerator LookAtRoutineStatic(CinemaCam camera, Action onCompletedCallback)
    {
        Transform camTrans = camera.CameraTransform.transform;

        Quaternion targetRot = Quaternion.LookRotation(LookAtTarget.position - camTrans.position);
        Quaternion camRot = camTrans.rotation;
        while (!IsCloseEnough(ref camRot, ref targetRot))
        {
            // slerp the rotation
            UpdateRotation(ref camRot, targetRot);
            camTrans.rotation = camRot;
            yield return null;
        }

        // Upon completion, call the callback
        Complete(onCompletedCallback);
    }

    private IEnumerator LookAtRoutine(CinemaCam camera, Action onCompletedCallback)
    {
        Transform camTrans = camera.CameraTransform.transform;
        Quaternion camRot, targetRot;
        do
        {
            targetRot = Quaternion.LookRotation(LookAtTarget.position - camTrans.position);

            camRot = camTrans.rotation;
            UpdateRotation(ref camRot, targetRot);
            camTrans.rotation = camRot;

            yield return null;
        } while (!IsCloseEnough(ref camRot, ref targetRot));
        
        // Upon completion, call the callback
        Complete(onCompletedCallback);
    }

    private bool IsCloseEnough(ref Quaternion thisRot, ref Quaternion targetRot)
    {
        return Quaternion.Angle(thisRot, targetRot) < CloseEnoughRotation;
    }

    private void UpdateRotation(ref Quaternion thisRot, Quaternion targetRot)
    {
        float step = Mathf.SmoothStep(0.0f, 1.0f, TransitionSpeed * Time.deltaTime);
        thisRot = Quaternion.Slerp(thisRot, targetRot, step);
    }

    private void Complete(Action completedCallback)
    {
        OnLookingAt.Invoke();
        completedCallback.Invoke();
    }
}
