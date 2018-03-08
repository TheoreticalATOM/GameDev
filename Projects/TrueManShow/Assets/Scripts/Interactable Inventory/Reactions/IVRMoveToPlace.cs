using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class IVRMoveToPlace : IVReaction
{
    public Transform Target;
    [MinValue(0)] public float Speed;
    public float CloseEnoughDistance = 0.2f;
    public float CloseEnoughRotation = 0.2f;
    public bool HideAfterReaction;

    public override void OnReact(ItemPhysicsInteract item, System.Action onCompletedCallback)
    {
        item.Disable();
        StartCoroutine(MoveToPlace(item, onCompletedCallback));
    }

    //UpdateCount();
    
    private IEnumerator MoveToPlace(ItemPhysicsInteract item, System.Action onCompletedCallback)
    {
        Transform itemTrans = item.transform;

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

        item.gameObject.SetActive(!HideAfterReaction);
    }
}
