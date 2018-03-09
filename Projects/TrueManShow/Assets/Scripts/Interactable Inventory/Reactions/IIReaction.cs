using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

//[CreateAssetMenu(fileName = "IIReaction", menuName = "Inventory Interaction/", order = 0)]
public abstract class IIReaction : SerializedScriptableObject
{
	public IIReaction Child;
	
	protected abstract IEnumerator OnReactionRoutine(Transform target, ItemPhysicsInteract item, System.Action completedCallback); 

	public void React(ItemPhysicsInteract item, Transform target, InteractiveInventory inventory, System.Action completedCallback)
	{
		inventory.StartCoroutine(OnReactionRoutine(target, item, () =>
		{
			if(Child)
				Child.React(item, target, inventory, completedCallback);
			else 
				completedCallback();
		}));
	}
}
