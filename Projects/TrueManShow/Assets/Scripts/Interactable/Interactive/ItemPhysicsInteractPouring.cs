using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PouringTrigger))]
public class ItemPhysicsInteractPouring : ItemPhysicsInteract
{
    private PouringTrigger mPouring;
    private InteractiveInventory mFoundInventory;
    private Collider mFoundCollider;
    private bool mSuccesfulState;

    private LinkedList<InteractiveInventory> mUsedInventories;

    public void LogCurrentInventoryAsUsed()
    {
        mUsedInventories.AddLast(mFoundInventory);
    }

    protected override void Awake()
    {
        base.Awake();
        mPouring = GetComponent<PouringTrigger>();
        mUsedInventories = new LinkedList<InteractiveInventory>();
    }

    protected override bool OnInteract(GameObject player)
    {
        Collider col = mPouring.CollisionCheck();
        if (col && col != mFoundCollider && !mFoundInventory)
        {
            mFoundCollider = col;

            InteractiveInventory inv = col.GetComponent<InteractiveInventory>();
            if (inv && inv.VerifyItem(this, out mSuccesfulState))
                mFoundInventory = inv;
        }
        else if (mFoundInventory && !mUsedInventories.Contains(mFoundInventory))
            mFoundInventory.InsertWithoutVerification(this, mSuccesfulState);

        return true;
    }
}