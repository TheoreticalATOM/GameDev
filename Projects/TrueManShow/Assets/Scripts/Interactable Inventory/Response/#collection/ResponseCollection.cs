using Sirenix.OdinInspector;

[System.Serializable]
public class ResponseDetails
{
    public IIReaction Reaction;
    public InventoryResponse Response;
}

public abstract class ResponseCollection : SerializedMonoBehaviour
{
    private void Reset()
    {
		ItemPhysicsInteract item = GetComponent<ItemPhysicsInteract>();
		if(item && !item.Response)
			item.Response = this;
    }

    public abstract ResponseDetails GetResponseDetails(InteractiveInventory inventory);
}