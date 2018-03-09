using Sirenix.OdinInspector;
public abstract class ResponseCollection : SerializedMonoBehaviour
{
	public abstract InventoryResponse GetResponse(InteractiveInventory inventory);
}