using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInteract : Item
{
    public bool CanBeInteractedWith;
    public bool OneUse;
    public UnityEvent OnInteracted;
    public UnityEvent OnFailedToInteract;

    private bool mHasBeenInteractedWithOnce;

    public void CanInteract(bool state)
    {
        CanBeInteractedWith = state;
    }

    protected override void OnStartInteract(GameObject Object, GameObject player)
    {
        if (CanBeInteractedWith)
        {
            if (OneUse && mHasBeenInteractedWithOnce)
                return;
            
			mHasBeenInteractedWithOnce = true;
            OnInteracted.Invoke();
        }
        else
            OnFailedToInteract.Invoke();
    }

    public override bool InteractUpdate(GameObject Object, GameObject camera) { return false; }
    public override void StopInteract(GameObject Object, GameObject camera) { }
}
