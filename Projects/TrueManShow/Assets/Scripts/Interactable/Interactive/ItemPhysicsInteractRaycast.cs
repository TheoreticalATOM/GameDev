using UnityEngine;

public class ItemPhysicsInteractRaycast : ItemPhysicsInteract
{
    public LayerMask ItemInventoryMask;
    public float dist;
    private RaycastHit hit;
    private Collider CurrentHit;
    protected override bool OnInteract(GameObject player)
    {

        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, dist, ItemInventoryMask) && CurrentHit != hit.collider)
        {
            CurrentHit = hit.collider;

            return !TryAddToInventory(hit.collider);
        }
        CurrentHit = null;
        return true;
    }
}