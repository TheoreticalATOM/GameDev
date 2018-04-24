using UnityEngine;

public class ItemPhysicsInteractTrigger : ItemPhysicsInteract
{
    private bool mIsInteracting;

    protected override void OnStartInteract(GameObject InteractedObject, GameObject player)
    {
        base.OnStartInteract(InteractedObject, player);
        mIsInteracting = true;
    }

    protected override bool OnInteract(GameObject player)
    {
        return mIsInteracting;
    }

    public override void StopInteract(GameObject Object, GameObject camera)
    {
        UIInteract.Main.HideAll();
        mIsInteracting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        mIsInteracting = !TryAddToInventory(other);
    }
}