using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PanPhysics : Item
{
    private bool posReachIn = false;
    private float speed = 2;

    public float smoothIn = 5f;
    public float MaxDistOut = 2f;
    public float MaxRot = 10f;
    public GameObject SnapPoint;
    public GameObject Player;

    private Vector3 NewPosition;
    private Vector3 StartingPos;
    public Quaternion StartingRot;
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
            posReachIn = BringObject(interactedObject, SnapPoint.transform.position);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartingPos = Player.transform.position; 
                Player.GetComponent<FirstPersonController>().enabled = false;
                posReachIn = true;
            }
                
            if (Input.GetButton("Fire1"))
            {
                    var rotationVector = transform.rotation.eulerAngles;
                    rotationVector.z = MaxRot * (Vector3.Distance(SnapPoint.transform.position, this.transform.position) / MaxDistOut);
                    this.transform.rotation = Quaternion.Euler(rotationVector);
                    //Rotate the interacted object according to the mouse movement.
                    float h = Input.GetAxis("Mouse X") * 0.1f;
                    float v = Input.GetAxis("Mouse Y") * 0.1f;
                    //NewPosition.Set(Player.transform.forward.x + v, Player.transform.forward.y + h, Player.transform.forward.z);
                    NewPosition.Set(interactedObject.transform.position.x + Player.transform.forward.x * v, interactedObject.transform.position.y + h, interactedObject.transform.position.z + Player.transform.forward.z * v);
                    if (MaxDistOut >= Vector3.Distance(SnapPoint.transform.position, NewPosition))
                    {
                        bool pos = BringObject(interactedObject, NewPosition);
                        //NewPosition.Set(interactedObject.transform.position.x + v, interactedObject.transform.position.y + h, interactedObject.transform.position.z);
                        //InteractableRigidbody.MovePosition(NewPosition);
                    }
            }
            else if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                Player.GetComponent<FirstPersonController>().enabled = true;
                posReachIn = false;
                this.transform.rotation = StartingRot;
            }
            return true;
        }
    }

    protected override void OnStartInteract(GameObject InteractedObject, GameObject player)
    {
        InteractedObject.GetComponent<Rigidbody>().useGravity = false;
    }

    public override void StopInteract(GameObject Object, GameObject camera)
    {
        throw new System.NotImplementedException();
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
