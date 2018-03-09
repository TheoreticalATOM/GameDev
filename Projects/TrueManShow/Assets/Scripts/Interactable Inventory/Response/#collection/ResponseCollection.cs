using Sirenix.OdinInspector;

[System.Serializable]
public class ResponseDetails
{
	public IIReaction Reaction;
	public InventoryResponse Response;
}

public abstract class ResponseCollection : SerializedMonoBehaviour
{
	public abstract ResponseDetails GetResponseDetails(InteractiveInventory inventory);
}