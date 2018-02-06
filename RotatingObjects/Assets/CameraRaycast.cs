using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using cakeslice;


public class CameraRaycast : MonoBehaviour {
	public Camera maincamera;
	public float RaycastDist;
	public Texture2D crosshairImage;
    public float smoothIn;
    public float smoothOut;
    public GameObject SnapPoint;

    private GameObject interactedObject;
	private GameObject interactedObjectOut;

    RaycastHit hit;
    private bool Interacting = false;
    private Vector3 OrigPos;
    private Quaternion OrigRot;

    private float speed = 2;
    private Vector3 mousepos;

	private bool posReachIn = false;
	private bool posReachOut = true;
    

    // Use this for initialization
    void Start () {

	}

	void OnGUI () {
		//crosshairImage.Resize ((crosshairImage.width / 2), (crosshairImage.height / 2), crosshairImage.format);
		float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
		float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
	}

	// Update is called once per frame
	void Update () {

        Ray ray = maincamera.ScreenPointToRay (Input.mousePosition);
        
		//Check if we are curently already interacting with an object
        if (!Interacting){
			
			//Check if we need to return an object back to it's original position
			if (!posReachOut)
			{
				posReachOut = ReturnObject (interactedObjectOut, OrigPos, OrigRot);
			}

			//Throws a ray and checks if we hit any object in the world
			if (Physics.Raycast (ray, out hit, RaycastDist)) {

				//Checks what tag the object has
				if (hit.collider.tag == "InteractableObject" || hit.collider.tag == "InteractableObjectPhys") {
                    
					hit.transform.GetComponent<Outline> ().eraseRenderer = false;
					interactedObject = hit.collider.gameObject;

					//Check if we are pressing the "E" key to interact 
					//and if the previous object as returned to the desired location

					if (Input.GetKeyDown ("e") && posReachOut) {
						switch (hit.collider.tag)
						{
							case "InteractableObject":
								Interacting = true;
								OrigPos = interactedObject.transform.position;
								OrigRot = interactedObject.transform.rotation;
								GetComponent<FirstPersonController> ().enabled = false;
								interactedObject.transform.GetComponent<Outline> ().eraseRenderer = true;
								break;
							case "InteractableObjectPhys":
								Interacting = true;
								OrigPos = interactedObject.transform.position;
								OrigRot = interactedObject.transform.rotation;
								interactedObject.transform.GetComponent<Outline> ().eraseRenderer = true;
								interactedObject.GetComponent<Rigidbody> ().useGravity = false;
								break;

						}

					}
				}else if (interactedObject != null) {

					interactedObject.transform.GetComponent<Outline>().eraseRenderer = true;
				}
			} else if (interactedObject != null) {
				
				interactedObject.transform.GetComponent<Outline>().eraseRenderer = true;
			}
        }else{
			
			//Check if we are pressing the "E" key to stop interacting with an object
            if (Input.GetKeyDown("e"))
            {
				switch (hit.collider.tag)
				{
					case "InteractableObject":
						Interacting = false;
						GetComponent<FirstPersonController>().enabled = true;
						posReachIn = false;
						posReachOut = false;
						interactedObjectOut = interactedObject;
						break;

					case "InteractableObjectPhys":
						Interacting = false;
						posReachIn = false;
						interactedObject.GetComponent<Rigidbody> ().useGravity = true;
					break;
				}                
            }
            else
			{
				//Bring the interacted object close to the camera
				if (!posReachIn || hit.collider.tag == "InteractableObjectPhys")
                {
					posReachIn = BringObject (interactedObject,SnapPoint.transform.position);
                }

				//Rotate the interacted object according to the mouse movement.
                float h = Input.GetAxis("Mouse X") * speed;
                float v = Input.GetAxis("Mouse Y") * speed;
                interactedObject.transform.Rotate(v, h, 0);
            }
        }
    }

	//This function will bring the GameObject to the specified Vector3 location.
	bool BringObject (GameObject Object, Vector3 TargetPosition){
		
		Object.transform.position = Vector3.Lerp(Object.transform.position, TargetPosition, Time.deltaTime * smoothIn);
		if (Object.transform.position == TargetPosition) {
			return true;
		} else {
			return false;
		}
	}

	//This function will return the GameObject to the specified Vector3 location with a specified Quaternion rotation.
	bool  ReturnObject ( GameObject Object,Vector3 TargetPosition,Quaternion TargetRotation)
	{
		if (Object.transform.position != TargetPosition)
		{
			Object.transform.position = Vector3.Lerp(Object.transform.position, TargetPosition, Time.deltaTime * smoothOut);
		}
		if (Object.transform.rotation != TargetRotation)
		{
			Object.transform.rotation = Quaternion.Lerp(Object.transform.rotation, TargetRotation, Time.deltaTime * smoothOut);
		}
		if ((Object.transform.position == TargetPosition) && (Object.transform.rotation == TargetRotation)) {
			return true;
		} else {
			return false;
		}
	}

}
