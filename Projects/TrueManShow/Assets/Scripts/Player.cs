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

    private void Start()
    {
        LockCamera();
        LockMovement();
    }

    public void LockCamera()
    {
        FirstPerson.DisableCamera = true;
    }
    public void LockMovement()
    {
        FirstPerson.DisableMovement = true;
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
        FirstPerson.DisableCamera = false;
    }
    public void UnlockMovement()
    {
        FirstPerson.DisableMovement = false;
    }
}
