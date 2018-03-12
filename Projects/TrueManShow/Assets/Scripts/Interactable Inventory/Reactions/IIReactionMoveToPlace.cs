using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IIR Move To Place ", menuName = "Inventory Interaction/Reaction - Move To Place", order = 0)]
public class IIReactionMoveToPlace : IIReaction
{
    public float TransitionSpeed;
	public float CloseEnoughDistance;
	public float CloseEnoughRotation;

    protected override IEnumerator OnReactionRoutine(Transform target, ItemPhysicsInteract item, Action completedCallback)
    {
        item.Disable();
        Transform itemTrans = item.transform;
		Vector3 targetPos = target.position;
		Quaternion targetRot = target.rotation;

        const int MAX_BOOLEAN_COUNT = 2;
        int booleanCount = 0;
        do
        {
			booleanCount = 0;
            float dSpeed = Time.deltaTime * TransitionSpeed;

            // move into position
            Vector3 pos = itemTrans.position;
            pos = Vector3.Lerp(pos, targetPos, dSpeed);
            if (Vector3.Distance(pos,targetPos) < CloseEnoughDistance * CloseEnoughDistance)
                booleanCount++;
				
            // Rotate into right direction
            Quaternion rot = itemTrans.rotation;
            rot = Quaternion.Slerp(rot, targetRot, dSpeed);
            if (Quaternion.Angle(rot, targetRot) < CloseEnoughRotation)
                booleanCount++;

            itemTrans.position = pos;
            itemTrans.rotation = rot;

            yield return null;
        } while (booleanCount < MAX_BOOLEAN_COUNT);

        item.Collider.enabled = true;
		completedCallback();
    }
}

/*Transform itemTrans = item.transform;

        const int MAX_BOOLEAN_COUNT = 2;
        int booleanCount = 0;
        do
        {
            float dSpeed = Time.deltaTime * Speed;

            // move into position
            Vector3 pos = itemTrans.position;
            Vector3 otherPos = Target.position;

            pos = Vector3.Lerp(pos, otherPos, dSpeed);
            if ((pos - otherPos).sqrMagnitude < CloseEnoughDistance * CloseEnoughDistance)
                booleanCount++;

            // Rotate into right direction
            Quaternion rot = itemTrans.rotation;
            Quaternion otherRot = Target.rotation;
            rot = Quaternion.Slerp(rot, otherRot, dSpeed);
            if (Quaternion.Angle(rot, otherRot) < CloseEnoughRotation)
                booleanCount++;


            itemTrans.position = pos;
            itemTrans.rotation = rot;

            yield return null;
        } while (booleanCount >= MAX_BOOLEAN_COUNT);
        onCompletedCallback();

        item.gameObject.SetActive(!HideAfterReaction); */
