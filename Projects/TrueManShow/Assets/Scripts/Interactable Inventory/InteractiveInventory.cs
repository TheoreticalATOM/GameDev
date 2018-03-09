using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractiveInventory : SerializedMonoBehaviour 
{
	public IIVerifier Verifier;

	public bool InsertItem(ItemPhysicsInteract item)
	{
		int failCount = 0;
		if(Verifier.Verify(item, ref failCount))
		{
			InventoryResponse response = item.Response.GetResponse(this);
			if(response) response.Respond(failCount < 1);
			return true;
		}
		return false;
	}

}
