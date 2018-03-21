using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycast : MonoBehaviour {
	public Camera maincamera;
	private float RaycastDist;
	public float MaxRaycastDist;
	public float MinRaycastDist;
	[Range(0.0f,90.0f)]
	public float AngleMaxReach = 45f;
	public GameObject SnapPoint;
	public Texture2D crosshairImage;
    public FirstPersonController FirstPerson;

    private GameObject interactedObject;
    private Item InteractableItem;

    RaycastHit hit;
    public bool Interacting = false;
    
    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
        //Check if we are curently already interacting with an object
        if (!Interacting)
        {
            //Throws a ray and checks if we hit any object in the world
			RaycastDist = CalcRayDist(MinRaycastDist,MaxRaycastDist,AngleMaxReach);
            if (Physics.Raycast(ray, out hit, RaycastDist))
            {

                //Checks what tag the object has
                if (hit.collider.GetComponent<Item>())
                {

                    hit.transform.GetComponent<cakeslice.Outline>().eraseRenderer = false;
                    if (interactedObject != null)
                        if (interactedObject != hit.collider.gameObject)
                            interactedObject.GetComponent<cakeslice.Outline>().eraseRenderer = true;

                    interactedObject = hit.collider.gameObject;

                    //Check if we are pressing the "E" key to interact 
                    //and if the previous object as returned to the desired location

                    if (Input.GetKeyDown("e"))
                    {
                        InteractableItem = interactedObject.GetComponent<Item>();
                        InteractableItem.StartInteract(hit.collider.gameObject, this.gameObject);
                        hit.transform.GetComponent<cakeslice.Outline>().eraseRenderer = true;
                        Interacting = true;

                    }
                }
                else if (interactedObject != null)
                {

                    interactedObject.transform.GetComponent<cakeslice.Outline>().eraseRenderer = true;
                }
            }
            else if (interactedObject != null)
            {
                interactedObject.transform.GetComponent<cakeslice.Outline>().eraseRenderer = true;
            }
        }
        else
        {
            Interacting = InteractableItem.InteractUpdate(interactedObject,this.gameObject);
        }
    }

	private float CalcRayDist(float MinDist, float MaxDist, float MaxAngle){
		Vector3 CenterofView = new Vector3(SnapPoint.transform.position.x,transform.position.y,SnapPoint.transform.position.z);
		Vector3 targetDir = CenterofView - transform.position;
		float Angle = Vector3.Angle (targetDir,transform.forward);
		if (Angle > MaxAngle) {
			return MaxDist;
		} else {
			return MinDist + ((MaxDist - MinDist) * (Angle / MaxAngle));
		}
	}
}
	
