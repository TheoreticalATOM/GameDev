using UnityEngine;

public class ItemPhysicsInteractRaycast : ItemPhysicsInteract
{
    public LayerMask ItemInventoryMask;
    public float dist;
    private RaycastHit hit;

    protected override bool OnInteract(GameObject player)
    {
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, dist, ItemInventoryMask))
            return !TryAddToInventory(hit.collider);
        return true;
    }
}