using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycast : MonoBehaviour
{
    public Camera maincamera;
    public float RaycastDist;
    public GameObject CrossHair;
    public FirstPersonController FirstPerson;
    public Transform SnappingPoint;

    public float MaxRaycastDist;
    public float MinRaycastDist;
    [Range(0.0f, 90.0f)]
    public float AngleMaxReach = 45f;

    private Item interactedObject;
    private Item InteractableItem;

    RaycastHit hit;
    public bool Interacting = false;
    public LayerMask InteractiveMask;


    // void OnGUI () {
    // 	//crosshairImage.Resize ((crosshairImage.width / 2), (crosshairImage.height / 2), crosshairImage.format);
    // 	float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
    // 	float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
    // 	GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width / 5, crosshairImage.height / 5), crosshairImage);
    // }


    public void ShowCrosshair(bool value)
    {
        CrossHair.SetActive(value);
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);

        //Check if we are curently already interacting with an object
        if (!Interacting)
        {
            RaycastDist = Mathf.Round(CalcRayDist(MinRaycastDist, MaxRaycastDist, AngleMaxReach));
            Debug.DrawRay(transform.position, transform.forward * RaycastDist);
            //Throws a ray and checks if we hit any object in the world
            if (Physics.Raycast(ray, out hit, RaycastDist, InteractiveMask))
            {
                Item item = hit.collider.GetComponent<Item>();
                //Checks what tag the object has
                if (item)
                {
                    item.ItemOutline.enabled = true;
                    if (interactedObject != null)
                        if (interactedObject != item)
                            interactedObject.ItemOutline.enabled = false;

                    interactedObject = item;

                    //Check if we are pressing the "interact" input to interact 
                    //and if the previous object as returned to the desired location

                    if (Input.GetButtonDown("Interact"))
                    {
                        ItemPhysics physicsItem = item as ItemPhysics;
                        if(!physicsItem || physicsItem.CanBePickedUp)
                        {
                            InteractableItem = item;
                            InteractableItem.StartInteract(hit.collider.gameObject, this.gameObject);
                            item.ItemOutline.enabled = false;
                            Interacting = true;
                        }
                    }
                }
                else if (interactedObject != null)
                {
                    interactedObject.ItemOutline.enabled = false;
                }
            }
            else if (interactedObject != null)
            {
                interactedObject.ItemOutline.enabled = false;
            }
        }
        else
        {
            Interacting = InteractableItem.InteractUpdate(interactedObject.gameObject, this.gameObject);
        }
    }

    private float CalcRayDist(float MinDist, float MaxDist, float MaxAngle)
    {
        Vector3 CenterofView = new Vector3(SnappingPoint.position.x, transform.position.y, SnappingPoint.position.z);
        Vector3 targetDir = CenterofView - transform.position;
        float Angle = Vector3.Angle(targetDir, transform.forward);
        if (Angle > MaxAngle)
        {
            return MaxDist;
        }
        else
        {
            return MinDist + ((MaxDist - MinDist) * (Angle / MaxAngle));
        }
    }
}
