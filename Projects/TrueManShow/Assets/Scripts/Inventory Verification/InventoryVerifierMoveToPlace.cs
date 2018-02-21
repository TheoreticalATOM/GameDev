using UnityEngine;
using Sirenix.OdinInspector;

public class InventoryVerifierMoveToPlace : InventoryVerifier
{
    [TabGroup("Move To")] public Transform Target;
    [TabGroup("Move To")] public float Speed;
    [TabGroup("Move To")] public float CloseEnoughDistance = 0.2f;
    [TabGroup("Move To")] public float CloseEnoughRotation = 0.2f;

    private InventoryItem mItem;

    private void Start()
    {
        enabled = false;
    }

    protected override void ReactToFoundItem(InventoryItem item)
    {
        item.RequiredItem.Disable();
        mItem = item;
        enabled = true;
    }

    private void Update()
    {
        float dSpeed = Time.deltaTime * Speed;
        Transform itemTrans = mItem.RequiredItem.transform;

        // move into position
        Vector3 pos = itemTrans.position;
        Vector3 otherPos = Target.position;
        pos = Vector3.Lerp(pos, otherPos, dSpeed);
        bool isCloseEnough = (pos - otherPos).sqrMagnitude < CloseEnoughDistance * CloseEnoughDistance;

        // Rotate into right direction
        Quaternion rot = itemTrans.rotation;
        Quaternion otherRot = Target.rotation;
        rot = Quaternion.Slerp(rot, otherRot, dSpeed);
        bool hasRotatedEnough = Quaternion.Angle(rot, otherRot) < CloseEnoughRotation;

        // Check if close enough and has rotated enough, then terminate the update, and trigger the event
        if (isCloseEnough && hasRotatedEnough)
        {
            mItem.CorrectItemAdded.Invoke();
            UpdateCount();

            mItem = null;
            enabled = false;
        }

        itemTrans.position = pos;
        itemTrans.rotation = rot;
    }
}