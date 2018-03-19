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
        StartCoroutine(mRoutineAction(camera, onCompletedCallback));
    }

    private Quaternion EulerToQuaternion(Vector3 sumTarget, Vector3 limiter)
    {
        Vector3 targetCam = sumTarget;
        targetCam *= limiter.x;
        targetCam *= limiter.y;
        targetCam *= limiter.z;
        return Quaternion.Euler(targetCam);
    }

    private IEnumerator LookAtRoutineStatic(CinemaCam camera, Action onCompletedCallback)
    {
        Transform camTrans = camera.CameraTransform.transform;
        Transform actorTrans = camera.LerpingTransform.transform;

        Quaternion targetRot = Quaternion.LookRotation(LookAtTarget.position - camTrans.position);

        //Quaternion targetCam = EulerToQuaternion(sumTarget, Vector3.one);
        //Quaternion targetActor = EulerToQuaternion(sumTarget, Vector3.up);

        Quaternion camRot = camTrans.rotation;
        //Quaternion actorRot = actorTrans.localRotation;

        // const int MAX_BOOL_COUNT = 1;
        // int boolCount = 0;
        // do
        // {
        //     boolCount = 0;
        //     float dSpeed = TransitionSpeed * Time.deltaTime;

        //     camRot = Quaternion.RotateTowards(camRot, actorRot, dSpeed);
        //     camTrans.rotation = camRot;
        //     if (IsCloseEnough(ref camRot, ref targetCam)) boolCount++;

        //     Debug.Log("hello");

        //     // UpdateRotation(ref actorRot, targetActor);
        //     // actorTrans.localRotation = actorRot;
        //     // if(IsCloseEnough(ref actorRot, ref targetActor)) boolCount++;

        //     yield return null;
        // } while (boolCount < MAX_BOOL_COUNT);

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
        Transform camTrans = camera.transform;
        Quaternion camRot = Quaternion.identity;
        Quaternion targetRot = Quaternion.identity;
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
