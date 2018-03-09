using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IIVStaticItemEqualityWithStorage : IIVStaticItemEquality 
{
	public ItemPhysicsInteract CurrentlyStoredItem {get;private set;}

	protected override bool OnVerifiying(ItemPhysicsInteract item)
	{
		bool result = base.OnVerifiying(item);
		if(result && !CurrentlyStoredItem)
		{
			CurrentlyStoredItem = item;

			item.RegisterVerificationChange(ClearStorage);
			return true;
		}
		return false;
	}

	public void ClearStorage()
	{
		CurrentlyStoredItem = null;
	}
}
