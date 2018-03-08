using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using SDE;

[System.Serializable]
public class InventoryItem
{
    [Header("Required Item:")]
    public ItemPhysicsInteract RequiredItem;
    public IVReaction Reaction;
    public UnityEvent CorrectItemAdded;
}

public class InventoryVerifier : SerializedMonoBehaviour
{
    [TabGroup("Known")] public InventoryItem[] RequiredItems;
    [TabGroup("Known")] public GameObject[] ExternalItems;
    [TabGroup("Known")] public UnityEvent OnGotAllRequiredItems;

    [TabGroup("Unknown")] public IVReaction DefaultReaction;
    [TabGroup("Unknown")] public IVFallbackVerification FallbackVerification;
    [TabGroup("Unknown")] public UnityEvent OnAddedWrongItem;

    private int mCount = 0;

    public void ExternalInsertItemNonReaction(GameObject referencedItem)
    {
        // if (ExternalItems.Contains(referencedItem))
        // {
        //     UpdateCount();
        //     return;
        // }
    }

    public bool InsertAnyItemFailureInvoke(ItemPhysicsInteract insertedItem)
    {
        InventoryItem foundItem = RequiredItems.Find((item) => item.RequiredItem == insertedItem);
        if (foundItem != null)
        {
            ReactToFoundItem(foundItem);
        }
        return true;
    }

    public bool InsertItem(ItemPhysicsInteract insertedItem)
    {
        /* Search for the item in the inventory.
        If found, then use its set reaction, and react positvely */
        InventoryItem foundItem = RequiredItems.Find((item) => item.RequiredItem == insertedItem);
        if (foundItem != null)
        {
            foundItem.Reaction.OnReact(insertedItem, () =>
            {
                foundItem.CorrectItemAdded.Invoke();
                UpdateCount();
            });
            return true;
        }

        /* if the item was not found, try to use the fallback verifier.
        If the item passes the fallback verification, then use the default reaction and react negatively */
        if(FallbackVerification.OnVerify(insertedItem))
        {
            DefaultReaction.OnReact(insertedItem, () => OnAddedWrongItem.Invoke());
            return true;
        }

        // if neither worked, return failure
        return false;
    }

    protected void UpdateCount()
    {
        if (++mCount >= RequiredItems.Length + ExternalItems.Length)
            OnGotAllRequiredItems.Invoke();
    }

    protected virtual void ReactToFoundItem(InventoryItem item)
    {
        item.CorrectItemAdded.Invoke();
        UpdateCount();
    }
}