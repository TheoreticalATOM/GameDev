using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryItem
{
    [Header("Required Item:")]
    public ItemPhysicsInteract RequiredItem;
    public UnityEvent CorrectItemAdded;
}

public class InventoryVerifier : SerializedMonoBehaviour
{
    [TabGroup("Verifier")] public InventoryItem[] RequiredItems;
    [TabGroup("Verifier")] public UnityEvent OnGotAllRequiredItems;
    private int mCount = 0;

    public bool InsertItem(ItemPhysicsInteract insertedItem)
    {
        foreach (InventoryItem item in RequiredItems)
        {
            if (item.RequiredItem == insertedItem)
            {
                ReactToFoundItem(item);
                return true;
            }
        }
        return false;
    }

    protected void UpdateCount()
    {
        if (++mCount >= RequiredItems.Length)
            OnGotAllRequiredItems.Invoke();
    }

    protected virtual void ReactToFoundItem(InventoryItem item)
    {
        item.CorrectItemAdded.Invoke();
        UpdateCount();
    }
}
