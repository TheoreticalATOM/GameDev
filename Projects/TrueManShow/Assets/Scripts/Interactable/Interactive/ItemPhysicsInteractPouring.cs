using UnityEngine;

[RequireComponent(typeof(PouringTrigger))]
public class ItemPhysicsInteractPouring : ItemPhysicsInteract
{
    private PouringTrigger mPouring;
    private InteractiveInventory mFoundInventory;
    private bool mVerified;
    private bool mSuccesfulState;
    
    protected override void Awake()
    {
        base.Awake();
        mPouring = GetComponent<PouringTrigger>();
    }

    protected override bool OnInteract(GameObject player)
    {
        Collider col = mPouring.CollisionCheck();
        if(col && !mFoundInventory)
        {
            mFoundInventory = col.GetComponent<InteractiveInventory>();
            mVerified = mFoundInventory.VerifyItem(this, out mSuccesfulState);

        } else if(mVerified && mFoundInventory)
        {
            mFoundInventory.InsertWithoutVerification(this, mSuccesfulState);
        }

        return true;
    }
}