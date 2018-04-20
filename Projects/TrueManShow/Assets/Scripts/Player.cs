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
        Lock(true);
    }

    public void LockCamera(bool value)
    {
        FirstPerson.DisableCamera = value;
        CameraRaycaster.enabled = !value;
        CameraRaycaster.ShowCrosshair(!value);        
        
        if (!value)
            FirstPerson.ReInitializeMouseLook();

    }
    public void LockMovement(bool value)
    {
        FirstPerson.DisableMovement = value;
        FirstPerson.ResetMovement();
    }
    public void Lock(bool value)
    {
        LockCamera(value);
        LockMovement(value);
    }

    public override void ResetObject()
    {
        Camera.transform.localRotation = mOrigLocalCamRot;
        transform.rotation = mOrigRot;
        transform.position = mOrigPos;
        Lock(false);
    }
}
