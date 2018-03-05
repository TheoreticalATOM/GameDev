using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemPhysics : Item
{
    private bool posReachIn = false;
    private float speed = 2;

    public float smoothIn = 5f;
    public GameObject SnapPoint;

    // Sets the displacement/rotational constraint of the physical object when its picked up
    public RigidbodyConstraints RotationConstraints;

    private Vector3 NewPosition;
    // stores the original constraints for when the object is deselected (returning it to its original constraints)
    private RigidbodyConstraints mOrigConstraints;
    public Rigidbody InteractableRigidbody { get; private set; }
    public Collider Collider { get; private set; }

    private void Start()
    {
        InteractableRigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();

        // store the original constraint on start
        mOrigConstraints = InteractableRigidbody.constraints;
    }


    public override bool InteractUpdate(GameObject interactedObject, GameObject player)
    {
        //Bring the interacted object close to the camera
        if (Input.GetKeyDown("e"))
        {
            posReachIn = false;
            interactedObject.GetComponent<Rigidbody>().useGravity = true;

            // Reset it back to the original constraint
            InteractableRigidbody.constraints = mOrigConstraints;
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
            player.GetComponent<CameraRaycast>().FirstPerson.DisableControls = isRightClicking;
            if (isRightClicking)
            {
                float h = Input.GetAxis("Mouse X") * speed;
                float v = Input.GetAxis("Mouse Y") * speed;
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
    }

    public override void StopInteract(GameObject Object, GameObject camera)
    {

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
