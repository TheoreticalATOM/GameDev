using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanInteract : PanPhysics
{

    private bool Interacting;
    public float dist;
    private RaycastHit hit;


    public override bool InteractUpdate(GameObject interactedObject, GameObject player)
    {
        bool Interacting = base.InteractUpdate(interactedObject, player);
        if (Interacting)
        {
            if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, dist))
            {
                InventoryVerifier inv = hit.collider.GetComponent<InventoryVerifier>();

                if (inv)
                {
                    //inv.InsertItem(this);
                    Interacting = false;
                }
            }
        }
        return Interacting;
    }

    public void Disable()
    {
        InteractableRigidbody.isKinematic = true;
        Collider.enabled = false;
    }
}
