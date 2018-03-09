using UnityEngine;

public abstract class IIReaction : MonoBehaviour 
{
	public IIReaction Child;

	public void React(ItemPhysicsInteract item, InventoryResponse response)
	{
		OnReact(item, response, () => 
		{
			if(Child) Child.React(item, response);
		});
	}
	
	protected abstract void OnReact(ItemPhysicsInteract item, InventoryResponse response, System.Action CompletedCallback);
}
