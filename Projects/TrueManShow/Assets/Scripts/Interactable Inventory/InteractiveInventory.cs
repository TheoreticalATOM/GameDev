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

        bool neverFailed;
        if (VerifyItem(item, out neverFailed))
        {
            InsertWithoutVerification(item, neverFailed);
            return true;
        }
        return false;
    }

    public void InsertWithoutVerification(ItemPhysicsInteract item, bool success)
    {
        ResponseDetails details = item.Response.GetResponseDetails(this);
        InventoryResponse response = details.Response;
        IIReaction reaction = details.Reaction;

        if (reaction)
            reaction.React(item, ReactionTarget, this, () => response.Respond(success));
        else
            response.Respond(success);
    }


    public bool InsertItemStreamASync(ItemPhysicsInteract item, System.Func<bool> endStreamAction)
    {
        if (!item.IsEligible)
            return false;

        bool neverFailed;
        if (VerifyItem(item, out neverFailed))
        {
            ResponseDetails details = item.Response.GetResponseDetails(this);
            InventoryResponse response = details.Response;
            IIReaction reaction = details.Reaction;

            if (reaction)
                reaction.React(item, ReactionTarget, this, () => StartCoroutine(ItemInsertionStream(response, neverFailed, endStreamAction)));
            else
                StartCoroutine(ItemInsertionStream(response, neverFailed, endStreamAction));
        }

        return false;
    }


    public bool VerifyItem(ItemPhysicsInteract item, out bool neverFailed)
    {
        int failCount = 0;
        bool result = Verifier.Verify(item, ref failCount);
        neverFailed = failCount < 1;
        return result;
    }

    private IEnumerator ItemInsertionStream(InventoryResponse response, bool reactionSuccess, System.Func<bool> endStreamAction)
    {
        while (!endStreamAction())
        {
            response.Respond(reactionSuccess);
            yield return null;
        }
    }
}
