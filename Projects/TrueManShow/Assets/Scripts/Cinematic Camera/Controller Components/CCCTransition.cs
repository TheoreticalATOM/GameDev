using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCCTransition : CinematicControllerComponent
{
	public Transform TransitionTo;
    public CinemaDetail TransitionDetails;

    public override void Respond(CinemaCam camera, Action onCompletedCallback)
    {
		StartCoroutine(TransitionRoutine(camera, onCompletedCallback));
    }

	private IEnumerator TransitionRoutine(CinemaCam camera, Action onCompletedCallback)
	{
		Vector3 targetPos = TransitionTo.position;
		Quaternion targetRot = TransitionTo.rotation;

		// Transition Details
        MovementRestriction restriction = TransitionDetails.Restriction;
        Quaternion targetCamRot = Quaternion.Euler(TransitionDetails.StartRotation);

		// Store values to reduce copy
		Transform lerpingTrans = camera.LerpingTransform; 
		Transform camTrans = camera.CameraTransform;
		float speed = TransitionDetails.Speed;
		float closeEnoughPos = camera.CloseEnoughDistance;
		float closeEnoughRot = camera.RotatedEnough;

        Vector3 distance = Vector3.zero;
        // used for incrementally check if all the close enough values are valid
        const int CLOSE_ENOUGH_MAX = 3;
        int closeEnoughCounter = 0;
        do
        {
            closeEnoughCounter = 0;

            Vector3 pos = lerpingTrans.position;
            Quaternion rot = lerpingTrans.rotation;
            Quaternion camRot = camTrans.localRotation;

            float dSpeed = speed * Time.deltaTime;

            // Movement
            TryLerpValue(ref pos.x, ref distance.x, targetPos.x, dSpeed, restriction, MovementRestriction.PX);
            TryLerpValue(ref pos.y, ref distance.y, targetPos.y, dSpeed, restriction, MovementRestriction.PY);
            TryLerpValue(ref pos.z, ref distance.z, targetPos.z, dSpeed, restriction, MovementRestriction.PZ);
            ValidateBoolCounter(ref closeEnoughCounter, distance.sqrMagnitude < (closeEnoughPos * closeEnoughPos));

            // Rotation
            rot = Quaternion.Slerp(rot, targetRot, dSpeed);
            ValidateBoolCounter(ref closeEnoughCounter, Quaternion.Angle(rot, targetRot) < closeEnoughRot);

            // Camera Rotation
            camRot = Quaternion.Slerp(camRot, targetCamRot, dSpeed);
            ValidateBoolCounter(ref closeEnoughCounter, Quaternion.Angle(camRot, targetCamRot) < closeEnoughRot);

            // Set the values
            lerpingTrans.rotation = rot;
            lerpingTrans.position = pos;
            camTrans.localRotation = camRot;

            yield return null;
        } while (closeEnoughCounter < CLOSE_ENOUGH_MAX);
		
		// Upon completion, call the callback
		onCompletedCallback.Invoke();
	}

	private static void TryLerpValue(ref float value, ref float distance, float target, float speed, MovementRestriction restriction, MovementRestriction targetRestriction)
    {
        if ((restriction & targetRestriction) != targetRestriction)
        {
            value = Mathf.Lerp(value, target, speed);
            distance = value - target;
        }
    }

	private static void ValidateBoolCounter(ref int counter, bool result)
    {
        counter = (result) ? ++counter : --counter;
    }
}
