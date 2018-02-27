using UnityEngine;

public class ItemPhysicsInteractRaycast : ItemPhysicsInteract
{
    public float dist;
    private RaycastHit hit;

    protected override bool OnInteract(GameObject player)
    {
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, dist))
            return TryAddToInventory(hit.collider);
        return true;
    }
}