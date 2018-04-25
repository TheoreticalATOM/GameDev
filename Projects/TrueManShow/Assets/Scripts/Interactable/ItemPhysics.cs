using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioClipPlayer))]
public class ItemPhysics : Item
{
    public AudioClip[] PickupSounds;
    public bool CanBePickedUp = true;
    private bool posReachIn = false;
    private float speed = 2;

    public float smoothIn = 5f;
    public GameObject SnapPoint;

    // Sets the displacement/rotational constraint of the physical object when its picked up
    public RigidbodyConstraints RotationConstraints;

    private AudioClipPlayer mSource;
    private Vector3 NewPosition;
    // stores the original constraints for when the object is deselected (returning it to its original constraints)
    private RigidbodyConstraints mOrigConstraints;
    public Rigidbody InteractableRigidbody { get; private set; }
    public Collider Collider { get; private set; }

    public void CanPickup(bool value)
    {
        CanBePickedUp = value;
    }

    protected override void Awake()
    {
        base.Awake();

        InteractableRigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        mSource = GetComponent<AudioClipPlayer>();


        // store the original constraint on start
        mOrigConstraints = InteractableRigidbody.constraints;
    }


    public override bool InteractUpdate(GameObject interactedObject, GameObject player)
    {
        InteractableRigidbody.velocity = Vector3.zero;
        InteractableRigidbody.angularVelocity = Vector3.zero;

        //Bring the interacted object close to the camera
        if (Input.GetKeyDown("e"))
        {
            posReachIn = false;
            interactedObject.GetComponent<Rigidbody>().useGravity = true;

            // Reset it back to the original constraint
            InteractableRigidbody.constraints = mOrigConstraints;
            player.GetComponent<CameraRaycast>().FirstPerson.DisableCamera = false;
            player.GetComponent<CameraRaycast>().FirstPerson.DisableMovement = false;

            UIInteract.Main.HideAll();

            return false;
        }
        else
        {
            if (!posReachIn)
            {
                posReachIn = BringObject(interactedObject, SnapPoint.transform.position);
            }

            // Makes sure that the camera is locked when the Right click is pressed
            bool isRightClicking = Input.GetButton("Fire2");
            player.GetComponent<CameraRaycast>().FirstPerson.DisableCamera = isRightClicking;
            player.GetComponent<CameraRaycast>().FirstPerson.DisableMovement = isRightClicking;

            if (isRightClicking)
            {
                float h = 0.0f, v = 0.0f;

                if(RotationConstraints != RigidbodyConstraints.FreezeRotation)
                {
                    if(RotationConstraints != RigidbodyConstraints.FreezeRotationX)
                        h = Input.GetAxis("Mouse X") * speed;

                    if(RotationConstraints != RigidbodyConstraints.FreezeRotationY)
                        v = Input.GetAxis("Mouse Y") * speed;
                }
                interactedObject.transform.Rotate(-v, -h, 0, Space.World);
            }

            //Rotate the interacted object according to the mouse movement.
            // float h = Input.GetAxis("Mouse X") * speed;
            // float v = Input.GetAxis("Mouse Y") * speed;
            // interactedObject.transform.Rotate(v, h, 0);
            return true;
        }
    }

    protected override void OnStartInteract(GameObject InteractedObject, GameObject player)
    {
        InteractedObject.GetComponent<Rigidbody>().useGravity = false;
        InteractableRigidbody.constraints = RotationConstraints;

        InteractableRigidbody.velocity = Vector3.zero;
        InteractableRigidbody.angularVelocity = Vector3.zero;
        
        if(PickupSounds.Length > 0)
            mSource.PlayRandomClip(PickupSounds);
    }

    public override void StopInteract(GameObject Object, GameObject camera)
    {
        Debug.Log("hello");
    }

    //This function will bring the GameObject to the specified Vector3 location.
    bool BringObject(GameObject Object, Vector3 TargetPosition)
    {
        NewPosition = Vector3.Lerp(Object.transform.position, TargetPosition, Time.deltaTime * smoothIn);
        InteractableRigidbody.MovePosition(NewPosition);
        if (Object.transform.position == TargetPosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
