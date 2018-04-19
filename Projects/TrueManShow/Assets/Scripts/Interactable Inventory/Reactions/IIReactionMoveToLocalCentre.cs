using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IIR Move To Place Position Only", menuName = "Inventory Interaction/Reaction - Move To Place Position Only", order = 0)]
public class IIReactionMoveToLocalCentre : IIReaction {

	public float TransitionSpeed;
	public float CloseEnoughDistance;

	protected override IEnumerator OnReactionRoutine(Transform target, ItemPhysicsInteract item, System.Action completedCallback)
	{
		item.Disable();
		Transform itemTrans = item.transform;
		Vector3 targetPos = Vector3.zero;

		float distance = 0.0f;
		do
		{
			float dSpeed = Time.deltaTime * TransitionSpeed;

			// move into position
			Vector3 pos = itemTrans.localPosition;
			pos = Vector3.Lerp(pos, targetPos, dSpeed);			

			itemTrans.localPosition = pos;

			distance = Vector3.Distance(pos, targetPos);
			
			yield return null;
		} while (distance > CloseEnoughDistance * CloseEnoughDistance);

		item.Collider.enabled = true;
		completedCallback();
	}
}
