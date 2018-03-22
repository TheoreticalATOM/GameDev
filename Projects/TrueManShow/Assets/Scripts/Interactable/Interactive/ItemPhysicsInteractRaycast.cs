using System.Collections;
using UnityEngine;

public class ItemPhysicsInteractRaycast : ItemPhysicsInteract
{
    public LayerMask ItemInventoryMask;
    public float dist;
    private RaycastHit hit;
    private Collider CurrentHit;
    private Coroutine mRoutine;

    protected override bool OnInteract(GameObject player)
    {
        bool collided = Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, dist, ItemInventoryMask);
        if (collided && CurrentHit != hit.collider)
        {
            CurrentHit = hit.collider;
            Debug.Log(CurrentHit.name);
            return !TryAddToInventory(hit.collider);
        }
        else if(!collided)
            CurrentHit = null;

        //CurrentHit = null;
        return true;
    }
}