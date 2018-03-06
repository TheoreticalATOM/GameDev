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
    [TabGroup("Unknown")] public string RequiredTag;
    [TabGroup("Unknown")] public UnityEvent OnAddedWrongItem;

    private int mCount = 0;

    public void ExternalInsertItemNonReaction(GameObject referencedItem)
    {
        if (ExternalItems.Contains(referencedItem))
        {
            UpdateCount();
            return;
        }
    }

    public bool InsertAnyItemFailureInvoke(ItemPhysicsInteract insertedItem)
    {
        // if the game object does not have the required tag, then invoke the wrong item added
        if (RequiredTag != string.Empty && !insertedItem.gameObject.CompareTag(RequiredTag))
            OnAddedWrongItem.Invoke();

        InventoryItem foundItem = RequiredItems.Find((item) => item.RequiredItem == insertedItem);
        if (foundItem != null)
        {
            ReactToFoundItem(foundItem);
        }
        return true;
    }

    public bool InsertItem(ItemPhysicsInteract insertedItem)
    {
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

        if (insertedItem.IsUniversial && insertedItem.CompareTag(RequiredTag))
        {
            DefaultReaction.OnReact(insertedItem, () =>
            {
                OnAddedWrongItem.Invoke();
            });
            return true;
        }


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