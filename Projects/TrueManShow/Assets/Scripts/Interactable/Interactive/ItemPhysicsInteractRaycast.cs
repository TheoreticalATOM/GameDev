using System.Collections;
using UnityEngine;

public class ItemPhysicsInteractRaycast : ItemPhysicsInteract
{
    public LayerMask ItemInventoryMask;
    public float dist;
    private RaycastHit hit;

    private bool mAddedToInventory;

    protected override void OnStartInteract(GameObject InteractedObject, GameObject player)
    {
        base.OnStartInteract(InteractedObject, player);
        
        if(mAddedToInventory)
        {
            StopAllCoroutines();
            StartCoroutine(DelayRemoveFromInventory());
        }
    }

    protected override bool OnInteract(GameObject player)
    {
        bool collided = Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, dist, ItemInventoryMask);
        if (collided && !mAddedToInventory)
        {
            mAddedToInventory = TryAddToInventory(hit.collider);
            if(mAddedToInventory)
                UIInteract.Main.HideAll();

            return !mAddedToInventory;
        }
        return true;
    }

    IEnumerator DelayRemoveFromInventory()
    {
        yield return new WaitForSeconds(2.0f);
        mAddedToInventory = false;
    }
}