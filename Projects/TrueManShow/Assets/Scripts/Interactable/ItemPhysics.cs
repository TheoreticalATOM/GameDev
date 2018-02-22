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

    private Vector3 NewPosition;
    public Rigidbody InteractableRigidbody { get; private set; }
    public Collider Collider { get; private set; }

    private void Start()
    {
        InteractableRigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }


    public override bool InteractUpdate(GameObject interactedObject, GameObject player)
    {
        //Bring the interacted object close to the camera
        if (Input.GetKeyDown("e"))
        {
            posReachIn = false;
            interactedObject.GetComponent<Rigidbody>().useGravity = true;
            return false;
        }
        else
        {
            if (!posReachIn)
            {
                posReachIn = BringObject(interactedObject, SnapPoint.transform.position);
            }

            //Rotate the interacted object according to the mouse movement.
            float h = Input.GetAxis("Mouse X") * speed;
            float v = Input.GetAxis("Mouse Y") * speed;
            interactedObject.transform.Rotate(v, h, 0);
            return true;
        }
    }

    protected override void OnStartInteract(GameObject InteractedObject, GameObject player)
    {
        InteractedObject.GetComponent<Rigidbody>().useGravity = false;
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
