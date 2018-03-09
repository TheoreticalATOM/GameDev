using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public abstract class ItemPhysicsInteract : ItemPhysics
{
    public bool IsUniversial;
    public bool IsEligible;
    [Required()] public ResponseCollection Response;

    private Stack<System.Action> mRegisteredVerifications = new Stack<System.Action>();

    public void RegisterVerificationChange(System.Action change)
    {
        mRegisteredVerifications.Push(change);
    }

    // ____________________________________________ Item Methods:
    protected override void OnStartInteract(GameObject InteractedObject, GameObject player)
    {
        base.OnStartInteract(InteractedObject, player);

        // if there are any registered verifications, then go through them all
        while (mRegisteredVerifications.Count > 0)
            mRegisteredVerifications.Pop()();
    }

    public override bool InteractUpdate(GameObject interactedObject, GameObject player)
    {
        if (!base.InteractUpdate(interactedObject, player))
            return false;

        return OnInteract(player);
    }

    protected abstract bool OnInteract(GameObject player);


    // _____________________________________________ Item Behaviour
    public void Disable()
    {
        InteractableRigidbody.isKinematic = true;
        //Collider.enabled = false;
    }

    public void SetEligibility(bool state)
    {
        IsEligible = state;
    }


    protected bool TryAddToInventory(Collider inventoryObject)
    {
        if (!IsEligible)
            return false;

        InteractiveInventory inv = inventoryObject.GetComponent<InteractiveInventory>();
        if(!inv) return false;

        return inv.InsertItem(this);
    }
}
