using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemThrowable : Item
{
    public UIInteractionValue DropUI = new UIInteractionValue() { Description = EDescription.Drop, Key = UIInteractionValue.EInteractKey.E };
    public UIInteractionValue ThrowUI = new UIInteractionValue() { Description = EDescription.Throw, Key = UIInteractionValue.EInteractKey.LMB };

	private bool Interacting = false;
	private bool posReachIn = false;
	private float speed = 2;

	public float smoothIn = 5f;
	public GameObject SnapPoint;
	public float Force;
	private LineRenderer lineRenderer;

    private float mDropDelay = 0.2f;
    private float mLastTime = 0.0f;

    public override bool InteractUpdate(GameObject interactedObject, GameObject player)
	{
		if (Interacting)
		{
			UpdateTrajectory (transform.position,player.transform.forward*Force, Physics.gravity);
			//Bring the interacted object close to the camera
			if (Input.GetKey(KeyCode.Mouse0))
			{
                Drop(interactedObject, player);
                interactedObject.GetComponent<Rigidbody>().AddForce(player.transform.forward * Force, ForceMode.Impulse);
				return false;
			}
            else if(Time.time - mLastTime > mDropDelay && Input.GetButton("Interact"))
            {
                Drop(interactedObject, player);
                return false;
            }

            UIInteract.Main.Display(DropUI, 0);
            UIInteract.Main.Display(ThrowUI, 1);
            if (interactedObject.transform.position != SnapPoint.transform.position)
            {
                posReachIn = BringObject(interactedObject, SnapPoint.transform.position);
            }

			//Rotate the interacted object according to the mouse movement.
			float h = Input.GetAxis("Mouse X") * speed;
			float v = Input.GetAxis("Mouse Y") * speed;
			interactedObject.transform.Rotate(v, h, 0);
			return true;
        }

        return false;
	}

    private void Drop(GameObject interactedObject, GameObject player)
    {
        UIInteract.Main.HideAll();


        interactedObject.GetComponent<Rigidbody>().useGravity = true;
        
        interactedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Interacting = false;
        player.GetComponent<CameraRaycast>().FirstPerson.enabled = true;
        player.GetComponent<CameraRaycast>().Interacting = false;
        posReachIn = false;
        lineRenderer.enabled = false;
    }

	protected override void OnStartInteract(GameObject interactedObject, GameObject player)
	{
        mLastTime = Time.time;

        Interacting = true;
		interactedObject.GetComponent<Rigidbody>().useGravity = false;
		interactedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        interactedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        interactedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.enabled = true;
	}

	public override void StopInteract(GameObject Object, GameObject camera) { }

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

	void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
	{
		int numSteps = 50; // for example
		float timeDelta = 1.0f / initialVelocity.magnitude; // for example

		lineRenderer.SetVertexCount(numSteps);

		Vector3 position = initialPosition;
		Vector3 velocity = initialVelocity;
		for (int i = 0; i < numSteps; ++i)
		{
			lineRenderer.SetPosition(i, position);

			position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
			velocity += gravity * timeDelta;
		}
	}
}