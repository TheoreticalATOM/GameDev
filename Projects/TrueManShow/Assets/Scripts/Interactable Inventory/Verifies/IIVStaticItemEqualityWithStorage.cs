using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IIVStaticItemEqualityWithStorage : IIVStaticItemEquality 
{
	public ItemPhysicsInteract CurrentlyStoredItem {get;private set;}

    public UnityEvent OnRemoved;

    public UnityEvent OnItemAdded;

    protected override bool OnVerifiying(ItemPhysicsInteract item)
	{
		bool result = base.OnVerifiying(item);
		if(result && !CurrentlyStoredItem)
		{
			CurrentlyStoredItem = item;

            OnItemAdded.Invoke();


            item.RegisterVerificationChange(ClearStorage);
			return true;
		}
		return false;
	}

	public void ClearStorage()
	{
		CurrentlyStoredItem = null;
        OnRemoved.Invoke();
	}
}
