using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SmoothRotation
{
    public float Smoothness = 30.0f;
    private float mVelocity;

    public void UpdateRotation(ref Quaternion thisRot, Quaternion targetRot)
    {
        float delta = Quaternion.Angle(thisRot, targetRot);
        if (delta > 0.0f)
        {
            float t = Mathf.SmoothDampAngle(delta, 0.0f, ref mVelocity, Time.deltaTime * Smoothness);
            t = 1.0f - t / delta;
            thisRot = Quaternion.Slerp(thisRot, targetRot, t);
        }
    }
}

public class CCCLookAt : CinematicControllerComponent
{
    public Transform LookAtTarget;
    public SmoothRotation Transition;
    public float CloseEnoughRotation;
    public UnityEvent OnLookingAt;

    private float mAngularVelocity;

    public override void Respond(CinemaCam camera, Action onCompletedCallback)
    {
        StartCoroutine(LookAtRoutine(camera, onCompletedCallback));
    }

    private IEnumerator LookAtRoutine(CinemaCam camera, Action onCompletedCallback)
    {
        Transform camTrans = camera.CameraTransform.transform;
        Quaternion camRot, targetRot;
        do
        {
            targetRot = Quaternion.LookRotation(LookAtTarget.position - camTrans.position);

            camRot = camTrans.rotation;
            Transition.UpdateRotation(ref camRot, targetRot);
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

    private void Complete(Action completedCallback)
    {
        OnLookingAt.Invoke();
        completedCallback.Invoke();
    }
}
