using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycast : MonoBehaviour {
	public Camera maincamera;
	public float RaycastDist;
	public Texture2D crosshairImage;
    public FirstPersonController FirstPerson;
    public Transform SnappingPoint;

    private Item interactedObject;
    private Item InteractableItem;

    RaycastHit hit;
    public bool Interacting = false;
    

    // Use this for initialization
    void Start () {
	}

	// void OnGUI () {
	// 	//crosshairImage.Resize ((crosshairImage.width / 2), (crosshairImage.height / 2), crosshairImage.format);
	// 	float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
	// 	float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
	// 	GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width / 5, crosshairImage.height / 5), crosshairImage);
	// }

    // Update is called once per frame
    void Update()
    {

        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);

        //Check if we are curently already interacting with an object
        if (!Interacting)
        {
            //Throws a ray and checks if we hit any object in the world
            if (Physics.Raycast(ray, out hit, RaycastDist))
            {
                Item item = hit.collider.GetComponent<Item>();
                //Checks what tag the object has
                if (item)
                {
                    item.ItemOutline.eraseRenderer = false;
                    if (interactedObject != null)
                        if (interactedObject != item)
                            interactedObject.ItemOutline.eraseRenderer = true;

                    interactedObject = item;

                    //Check if we are pressing the "interact" input to interact 
                    //and if the previous object as returned to the desired location

                    if (Input.GetButtonDown("Interact"))
                    {
                        InteractableItem = item;
                        InteractableItem.StartInteract(hit.collider.gameObject, this.gameObject);
                        item.ItemOutline.eraseRenderer = true;
                        Interacting = true;

                    }
                }
                else if (interactedObject != null)
                {
                    interactedObject.ItemOutline.eraseRenderer = true;
                }
            }
            else if (interactedObject != null)
            {
                interactedObject.ItemOutline.eraseRenderer = true;
            }
        }
        else
        {
            Interacting = InteractableItem.InteractUpdate(interactedObject.gameObject,this.gameObject);
        }
    }
}
