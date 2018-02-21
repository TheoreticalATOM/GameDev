using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.PostProcessing;


public class ItemRotatable : Item
{
    private Vector3 OrigPos;
    private Quaternion OrigRot;
    private bool Interacting = false;
    private bool posReachOut = false;
    private bool posReachIn = false;
    private float speed = 2;

    public float smoothIn = 5f;
    public float smoothOut = 10f;
    public GameObject SnapPoint;


    public override bool InteractUpdate(GameObject interactedObject, GameObject player)
    {
        if (Interacting)
        {
            //Bring the interacted object close to the camera
            if (Input.GetKeyDown("e"))
            {
                Interacting = false;
                player.GetComponent<CameraRaycast>().FirstPerson.enabled = true;
                posReachIn = false;
                posReachOut = false;
                //interactedObjectOut = interactedObject;
                player.GetComponent<PostProcessingBehaviour>().enabled = false;
                return true;
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
        else
        {
            //Check if we need to return an object back to it's original position
            if (!posReachOut)
            {
                posReachOut = ReturnObject(interactedObject, OrigPos, OrigRot);
                if (posReachOut)
                {
                    posReachOut = false;
                    return false;
                }
            }
            return true;
        }
    }

    public override void StartInteract(GameObject Object, GameObject player)
    {
        Interacting = true;
        OrigPos = Object.transform.position;
        OrigRot = Object.transform.rotation;
        player.GetComponent<CameraRaycast>().FirstPerson.enabled = false;
        player.GetComponent<PostProcessingBehaviour>().enabled = true;
        //profile.DepthofField.FocalLenght = 0;
    }

    public override void StopInteract(GameObject Object, GameObject camera)
    {
        throw new System.NotImplementedException();
    }

    //This function will bring the GameObject to the specified Vector3 location.
    bool BringObject(GameObject Object, Vector3 TargetPosition)
    {

        Object.transform.position = Vector3.Lerp(Object.transform.position, TargetPosition, Time.deltaTime * smoothIn);
        if (Object.transform.position == TargetPosition)
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
