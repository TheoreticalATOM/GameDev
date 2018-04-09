using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SmoothRotation
{
    public float Smoothness = 30.0f;
    private float mVelocity;

    private float mDelta;

    public void UpdateRotation(ref Quaternion thisRot, Quaternion targetRot)
    {
        mDelta = Quaternion.Angle(thisRot, targetRot);
        if (mDelta > 0.0f)
            thisRot = Quaternion.Slerp(thisRot, targetRot, GetT());
    }

    public void UpdateAngle(ref float thisAngle, float targetAngle)
    {
        thisAngle = Mathf.SmoothDampAngle(thisAngle, targetAngle, ref mVelocity, Time.deltaTime * Smoothness);
    }

    private float GetT()
    {
        float t = Mathf.SmoothDampAngle(mDelta, 0.0f, ref mVelocity, Time.deltaTime * Smoothness);
        return 1.0f - t / mDelta;
    }
}

public class CCCLookAt : CinematicControllerComponent
{
    public Transform LookAtTarget;
    public SmoothRotation CameraTransition;
    public SmoothRotation AgentTransition;
    public float CloseEnoughRotation;
    public UnityEvent OnLookingAt;

    private readonly static Vector3[] AXES = new Vector3[] { Vector3.right, Vector3.up };
    private enum EAxis { CameraAxis = 0, AgentAxis }

    public override void Respond(CinemaCam camera, Action onCompletedCallback)
    {
        StartCoroutine(LookAtRoutine(camera, onCompletedCallback));
    }

    private IEnumerator LookAtRoutine(CinemaCam camera, Action onCompletedCallback)
    {
        Transform camTrans = camera.CameraTransform;
        Transform agentTrans = camera.LerpingTransform;

        Vector3 targetPos = LookAtTarget.position;
        Quaternion camTarget = Quaternion.LookRotation(targetPos - camTrans.position);
        Quaternion agentTarget = Quaternion.LookRotation(targetPos - agentTrans.position);

        // Quaternion cam, agent;
        int bCount = 0;
        do
        {
            bCount = 2;

            /* Agent rotation */
            Vector3 aRotV = agentTrans.rotation.eulerAngles;
            Vector3 aRotTargetV = agentTarget.eulerAngles;
            if (UpdateRotation(agentTrans, ref aRotV.y, aRotTargetV.y, AgentTransition, EAxis.AgentAxis)) bCount--;

            /* Camera Rotation */
            Vector3 cRotV = camTrans.localRotation.eulerAngles;
            Vector3 cRotTargetV = camTarget.eulerAngles;
            if (UpdateRotation(camTrans, ref cRotV.x, cRotTargetV.x, CameraTransition, EAxis.CameraAxis)) bCount--;

            // AgentTransition.UpdateAngle(ref aRotV.y, aRotTargetV.y);
            // Quaternion aRotQ = Quaternion.AngleAxis(aRotV.y, AgentAxis);
            // Quaternion aRotTargetQ = Quaternion.AngleAxis(aRotTargetV.y, AgentAxis);
            // if (Quaternion.Angle(aRotQ, aRotTargetQ) < CloseEnoughAgentRotation) bCount--;

            /* Camera rotation */
            // Vector3 cRotV = camTrans.localRotation.eulerAngles;
            // Vector3 cRotTargetV = camTarget.eulerAngles;
            // CameraTransition.UpdateAngle(ref cRotV.x, cRotTargetV.x);
            // Quaternion cRotQ = Quaternion.AngleAxis(cRotV.x, CameraAxis);
            // Quaternion cRotTargetQ = Quaternion.AngleAxis(cRotTargetV.x, CameraAxis);
            // if (Quaternion.Angle(cRotQ, cRotTargetQ) < CloseEnoughCameraRotation) bCount--;

            agentTrans.rotation = Quaternion.Euler(aRotV);
            camTrans.localRotation = Quaternion.Euler(cRotV);

            yield return null;
        } while (bCount > 0);

        // Transform camTrans = camera.CameraTransform.transform;
        // Quaternion camRot, targetRot;
        // do
        // {
        //     targetRot = Quaternion.LookRotation(LookAtTarget.position - camTrans.position);

        //     camRot = camTrans.rotation;
        //     CameraTransition.UpdateRotation(ref camRot, targetRot);
        //     camTrans.rotation = camRot;

        //     yield return null;
        // } while (!IsCloseEnough(ref camRot, ref targetRot));

        // Upon completion, call the callback
        Complete(onCompletedCallback);
    }

    private bool UpdateRotation(Transform thisTrans, ref float thisAngle, float agentAngle, SmoothRotation smoothRotation, EAxis axis)
    {
        smoothRotation.UpdateAngle(ref thisAngle, agentAngle);
        Quaternion rotQ = Quaternion.AngleAxis(thisAngle, AXES[(int)axis]);
        Quaternion rotTargetQ = Quaternion.AngleAxis(agentAngle, AXES[(int)axis]);
        return Quaternion.Angle(rotQ, rotTargetQ) < CloseEnoughRotation;
    }

    // private bool IsCloseEnough(ref Quaternion thisRot, ref Quaternion targetRot)
    // {
    //     Debug.Log(Quaternion.Angle(thisRot, targetRot));
    //     return Quaternion.Angle(thisRot, targetRot) < CloseEnoughRotation;
    // }

    private void Complete(Action completedCallback)
    {
        OnLookingAt.Invoke();
        completedCallback.Invoke();
    }
}
