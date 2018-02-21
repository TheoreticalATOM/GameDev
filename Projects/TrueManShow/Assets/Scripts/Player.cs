using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : Resetable
{
    [Header("Controller")]
    public CharacterController Controller;
    public FirstPersonController FirstPerson;

    [Header("Camera")]
    public CameraRaycast CameraRaycaster;
    public CinemaCam CinematicCamera;
    public Camera Camera;

	
	private Quaternion mOrigLocalCamRot;
	private Quaternion mOrigRot;
	private Vector3 mOrigPos;

    private void Awake()
    {
		mOrigLocalCamRot = Camera.transform.localRotation;
		mOrigPos = transform.position;
		mOrigRot = transform.rotation;
    }

    public void LockCamera()
    {
        FirstPerson.enabled = false;
    }

    public override void ResetObject()
    {
        Camera.transform.localRotation = mOrigLocalCamRot;
		transform.rotation = mOrigRot;
		transform.position = mOrigPos;
		UnlockCamera();
    }

    public void UnlockCamera()
    {
        FirstPerson.ReInitializeMouseLook();
        FirstPerson.enabled = true;
    }
}
