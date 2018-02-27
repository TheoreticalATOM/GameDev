using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPhysicsInteract : ItemPhysics
{
    public override bool InteractUpdate(GameObject interactedObject, GameObject player)
    {
        if (!base.InteractUpdate(interactedObject, player))
            return false;

        return OnInteract(player);
    }

    public void Disable()
    {
        InteractableRigidbody.isKinematic = true;
        Collider.enabled = false;
    }

    protected abstract bool OnInteract(GameObject player);

    protected bool TryAddToInventory(Collider inventoryObject)
    {
        InventoryVerifier inv = inventoryObject.GetComponent<InventoryVerifier>();
        return inv && inv.InsertItem(this);
    }


    // private bool Interacting;
    // public float dist;
    // private RaycastHit hit;

    // public override bool InteractUpdate(GameObject interactedObject, GameObject player)
    // {
    //     bool Interacting = base.InteractUpdate(interactedObject, player);
    //     if (Interacting)
    //     {
    //         if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, dist))
    //         {
    //             InventoryVerifier inv = hit.collider.GetComponent<InventoryVerifier>();

    //             if (inv)
    //             {
    //                 inv.InsertItem(this);
    //                 Interacting = false;
    //             }
    //         }
    //     }
    //     return Interacting;
    // }
}
