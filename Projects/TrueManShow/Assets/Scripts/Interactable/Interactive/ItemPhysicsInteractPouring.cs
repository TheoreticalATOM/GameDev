using UnityEngine;

[RequireComponent(typeof(PouringTrigger))]
public class ItemPhysicsInteractPouring : ItemPhysicsInteract
{
    private PouringTrigger mPouring;
    private bool mIsPouring;

    protected override void Awake()
    {
        base.Awake();
        mPouring = GetComponent<PouringTrigger>();
    }

    protected override bool OnInteract(GameObject player)
    {
        Collider col = mPouring.CollisionCheck();
        if(col && !mIsPouring)
        {
            InteractiveInventory inv = col.GetComponent<InteractiveInventory>();
            //mIsPouring = inv.InsertItemStreamASync(this, );
        }

        return true;
    }
}