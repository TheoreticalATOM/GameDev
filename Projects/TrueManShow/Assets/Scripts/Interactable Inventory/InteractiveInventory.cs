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
			item.Response.Respond(failCount < 1);
			return true;
		}
		return false;
	}

}
