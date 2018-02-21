using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemThrowable: Item
{
    private bool Interacting = false;
    private bool posReachIn = false;
    private float speed = 2;

    public float smoothIn = 5f;
    public GameObject SnapPoint;
    public HandleBar throwBar;


    public override bool InteractUpdate(GameObject interactedObject, GameObject player)
    {
        if (Interacting)
        {
            //Bring the interacted object close to the camera
            if (Input.GetKey(KeyCode.Mouse0))
            {
                throwBar.UpdateBar();
                player.GetComponent<CameraRaycast>().FirstPerson.enabled = false;
                if (!posReachIn)
                {
                    posReachIn = BringObject(interactedObject, SnapPoint.transform.position);
                }
                return true;
            }
            else
            {
                if (throwBar.thrust != 0)
                {
                    interactedObject.GetComponent<Rigidbody>().useGravity = true;
                    interactedObject.GetComponent<Rigidbody>().AddForce(player.transform.forward * throwBar.GetComponent<HandleBar>().thrust);

                    throwBar.ResetThrust();
                    Interacting = false;
                    player.GetComponent<CameraRaycast>().FirstPerson.enabled = true;
                    player.GetComponent<CameraRaycast>().Interacting = false;
                    posReachIn = false;
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
        }
        return false;
    }

    public override void StartInteract(GameObject interactedObject, GameObject player)
    {
        Interacting = true;
        interactedObject.GetComponent<Rigidbody>().useGravity = false;
    }

    public override void StopInteract(GameObject Object, GameObject camera)
    {
        throw new System.NotImplementedException();
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
}