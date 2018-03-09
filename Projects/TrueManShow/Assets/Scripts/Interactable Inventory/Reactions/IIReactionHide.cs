using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IIR Hide ", menuName = "Inventory Interaction/Reaction - Hide", order = 0)]
public class IIReactionHide : IIReaction 
{
	protected override IEnumerator OnReactionRoutine(Transform target, ItemPhysicsInteract item, System.Action completedCallback)
	{
		item.gameObject.SetActive(false);
		completedCallback();
		yield return null;
	}
}
