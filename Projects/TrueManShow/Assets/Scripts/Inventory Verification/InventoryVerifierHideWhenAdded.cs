using UnityEngine;

public class InventoryVerifierHideWhenAdded : InventoryVerifier
{
    protected override void ReactToFoundItem(InventoryItem item)
    {
        base.ReactToFoundItem(item);
        item.RequiredItem.gameObject.SetActive(false);
    }
}