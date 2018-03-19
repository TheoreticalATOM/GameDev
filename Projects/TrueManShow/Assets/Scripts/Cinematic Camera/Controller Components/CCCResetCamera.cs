using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CCCResetCamera : CinematicControllerComponent
{
    public float TransitionSpeed = 5.0f;
    public float CloseEnoughRotation;
    public UnityEvent OnReset;

    public override void Respond(CinemaCam camera, System.Action onCompletedCallback)
    {
        StartCoroutine(ResetRoutine(camera, onCompletedCallback));
    }

    private IEnumerator ResetRoutine(CinemaCam camera, System.Action onCompletedCallback)
    {
        Transform camTrans = camera.CameraTransform.transform;
        Quaternion resetRot = Quaternion.identity;
        Quaternion camRot = camTrans.localRotation;

        while (Quaternion.Angle(resetRot, camRot) > CloseEnoughRotation)
        {
            // slerp the rotation
            camRot = Quaternion.Slerp(camRot, resetRot, TransitionSpeed * Time.deltaTime);
            camTrans.localRotation = camRot;
            yield return null;
        }

        // Upon completion, call the callback
        onCompletedCallback.Invoke();
        OnReset.Invoke();
    }
}