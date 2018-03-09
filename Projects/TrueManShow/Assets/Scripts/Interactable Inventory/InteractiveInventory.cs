using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractiveInventory : SerializedMonoBehaviour 
{
	public IIVerifier Verifier;
	public Transform ReactionTarget;

	public bool InsertItem(ItemPhysicsInteract item)
	{
		int failCount = 0;
		if(Verifier.Verify(item, ref failCount))
		{
			ResponseDetails details = item.Response.GetResponseDetails(this);
			InventoryResponse response = details.Response;
			IIReaction reaction = details.Reaction;

			bool hasFailed = failCount < 1;

			if(reaction)
				reaction.React(item, ReactionTarget, this, () => response.Respond(hasFailed));
			else
				response.Respond(hasFailed);

			return true;
		}
		return false;
	}

}
