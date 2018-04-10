using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Interact : MonoBehaviour {
    private bool Interacting = false;
    private Vector3 OrigPos;
    private Quaternion OrigRot;

    private GameObject interactedObject;
    private GameObject player;
    private bool posReachIn = false;
    private bool posReachOut = true;
    private GameObject interactedObjectOut;

    private float speed = 2;
    public float smoothIn = 5;
    public float smoothOut = 10;

    public GameObject SnapPoint;

    public Image throwBar;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Interacting)
        {
            //Check if we are pressing the "E" key to stop interacting with an object
            if (Input.GetKeyDown("e") && interactedObject.tag != "ThrowableObject" && (posReachIn || interactedObject.tag == "InteractableObjectPhys"))
            {
                switch (interactedObject.tag)
                {
                    case "InteractableObject":
                        Interacting = false;
                        player.GetComponent<FirstPersonController>().enabled = true;
                        posReachIn = false;
                        posReachOut = false;
                        interactedObjectOut = interactedObject;
                        break;

                    case "InteractableObjectPhys":
                        print("OLLLA");
                        Interacting = false;
                        posReachIn = false;
                        posReachOut = true;
                        interactedObject.GetComponent<Rigidbody>().useGravity = true;
                        player.GetComponent<CameraRaycast>().Interacting = false;
                        break;
                }
            }
            else if (Input.GetKey(KeyCode.Mouse0) && interactedObject.tag == "ThrowableObject")
            {

                throwBar.GetComponent<HandleBar>().UpdateBar();
                if (!posReachIn || interactedObject.tag == "InteractableObjectPhys")
                {
                    posReachIn = BringObject(interactedObject, SnapPoint.transform.position);
                }

            }
            else
            {
                if (throwBar.GetComponent<HandleBar>().thrust != 0)
                {
                    interactedObject.GetComponent<Rigidbody>().useGravity = true;
                    interactedObject.GetComponent<Rigidbody>().AddForce(player.transform.forward * throwBar.GetComponent<HandleBar>().thrust);

                    throwBar.GetComponent<HandleBar>().ResetThrust();
                    Interacting = false;
                    player.GetComponent<FirstPersonController>().enabled = true;
                    player.GetComponent<CameraRaycast>().Interacting = false;
                    posReachIn = false;
                    posReachOut = true;
                }
                else
                {
                    //Bring the interacted object close to the camera
                    if (!posReachIn || interactedObject.tag == "InteractableObjectPhys")
                    {
                        posReachIn = BringObject(interactedObject, SnapPoint.transform.position);
                        print("BringObject");
                    }

                    //Rotate the interacted object according to the mouse movement.
                    float h = Input.GetAxis("Mouse X") * speed;
                    float v = Input.GetAxis("Mouse Y") * speed;
                    interactedObject.transform.Rotate(v, h, 0);
                }
            }
        }
        else
        {
            //Check if we need to return an object back to it's original position
            if (!posReachOut)
            {
                posReachOut = ReturnObject(interactedObjectOut, OrigPos, OrigRot);
                if (posReachOut && player != null && player.GetComponent<CameraRaycast>().Interacting)
                {
                    player.GetComponent<CameraRaycast>().Interacting = false;
                    print("JP!!!!!!");
                }
            }
        }
    }

    public void StartInteraction(GameObject Object,GameObject camera)
    {
        Interacting = true;
        player = camera;
        player.GetComponent<CameraRaycast>().Interacting = true;
        interactedObject = Object;
        interactedObject.transform.GetComponent<cakeslice.Outline>().enabled = false;
        switch (interactedObject.tag)
        {
            case "InteractableObject":
                OrigPos = interactedObject.transform.position;
                OrigRot = interactedObject.transform.rotation;
                player.GetComponent<FirstPersonController>().enabled = false;
                break;
            case "InteractableObjectPhys":
                OrigPos = interactedObject.transform.position;
                OrigRot = interactedObject.transform.rotation;
                interactedObject.GetComponent<Rigidbody>().useGravity = false;
                break;
            case "ThrowableObject":
                OrigPos = interactedObject.transform.position;
                OrigRot = interactedObject.transform.rotation;
                interactedObject.GetComponent<Rigidbody>().useGravity = false;
                break;


        }
    }

    //This function will bring the GameObject to the specified Vector3 location.
    bool BringObject(GameObject Object, Vector3 TargetPosition)
    {

        Object.transform.position = Vector3.Lerp(Object.transform.position, TargetPosition, Time.deltaTime * smoothIn);
        if (Object.transform.position == TargetPosition && Object.tag == "InteractableObject")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //This function will return the GameObject to the specified Vector3 location with a specified Quaternion rotation.
    bool ReturnObject(GameObject Object, Vector3 TargetPosition, Quaternion TargetRotation)
    {
        if (Object.transform.position != TargetPosition)
        {
            Object.transform.position = Vector3.Lerp(Object.transform.position, TargetPosition, Time.deltaTime * smoothOut);
        }
        if (Object.transform.rotation != TargetRotation)
        {
            //Object.transform.rotation = Quaternion.Lerp(Object.transform.rotation, TargetRotation, Time.deltaTime * smoothOut);
            Object.transform.rotation = TargetRotation;
        }
        if ((Object.transform.position == TargetPosition) && (Object.transform.rotation == TargetRotation))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
