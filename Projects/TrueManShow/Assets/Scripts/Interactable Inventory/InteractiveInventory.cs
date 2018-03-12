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
        if (!item.IsEligible)
            return false;

        int failCount;
        if (VerifyItem(item, out failCount))
        {
            ResponseDetails details = item.Response.GetResponseDetails(this);
            InventoryResponse response = details.Response;
            IIReaction reaction = details.Reaction;

            bool hasFailed = failCount < 1;

            if (reaction)
                reaction.React(item, ReactionTarget, this, () => response.Respond(hasFailed));
            else
                response.Respond(hasFailed);

            return true;
        }
        return false;
    }

    public bool InsertItemStreamASync(ItemPhysicsInteract item, System.Func<bool> endStreamAction)
    {
        if (!item.IsEligible)
            return false;

        int failCount;
        if (VerifyItem(item, out failCount))
        {
            ResponseDetails details = item.Response.GetResponseDetails(this);
            InventoryResponse response = details.Response;
            IIReaction reaction = details.Reaction;

            bool hasFailed = failCount < 1;

            if (reaction)
                reaction.React(item, ReactionTarget, this, () => StartCoroutine(ItemInsertionStream(response, hasFailed, endStreamAction)));
            else
                StartCoroutine(ItemInsertionStream(response, hasFailed, endStreamAction));
        }

        return false;
    }

    private bool VerifyItem(ItemPhysicsInteract item, out int failCount)
    {
        failCount = 0;
        return Verifier.Verify(item, ref failCount);
    }

    private IEnumerator ItemInsertionStream(InventoryResponse response, bool reactionSuccess, System.Func<bool> endStreamAction)
    {
		while(!endStreamAction())
		{
			response.Respond(reactionSuccess);
			yield return null;
		}
    }
}
