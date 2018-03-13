using Sirenix.OdinInspector;
using UnityEngine;

public class PouringTrigger : SerializedMonoBehaviour
{
    public ParticleSystem Particles;
    //public BowlFilling Bowl;
    // public Color BowlColourChange;
    // public float FillSpeed;

    public Vector3 ColliderOffset;
    public Vector3 ColliderSize;
    public LayerMask ColliderMask;

    private Collider[] mColliderBuffer = new Collider[1];
    //private bool mHasFinishedPouring;
    //private bool mHasStartedToPour;

    // private void Awake()
    // {
    //     mHasFinishedPouring = mHasStartedToPour = false;
    // }

    private void OnDrawGizmos()
    {
        Vector3 particlePos = Particles.transform.position;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(ColliderOffset + particlePos, ColliderSize);
    }

    public Collider CollisionCheck()
    {
        Vector3 particlePos = Particles.transform.position;
        Vector3 towardsPoint = particlePos - transform.position;

        if (Mathf.Atan2(towardsPoint.y, towardsPoint.x) > 0.0f)
        {
            Particles.Stop();
            return null;
        }

        // Play particle system
        if(!Particles.isPlaying)
            Particles.Play();

        // Check collision area if it hit a bowl
        Vector3 boxPos = particlePos + ColliderOffset;
        Physics.OverlapBoxNonAlloc(boxPos, ColliderSize, mColliderBuffer, Quaternion.identity, ColliderMask);

        return mColliderBuffer[0];
    }

    // void FixedUpdate()
    // {
    //     Vector3 particlePos = Particles.transform.position;
    //     Vector3 towardsPoint = particlePos - transform.position;

    //     if (Mathf.Atan2(towardsPoint.y, towardsPoint.x) > 0.0f)
    //     {
    //         Bowl.ClearAddBowlManual();
    //         Particles.Stop();
    //         return;
    //     }

    //     // Play particle system
    //     Particles.Play();

    //     // Check collision area if it hit a bowl
    //     Vector3 boxPos = particlePos + ColliderOffset;
    //     Physics.OverlapBoxNonAlloc(boxPos, ColliderSize, mColliderBuffer, Quaternion.identity, ColliderMask);

    //     if (!mHasFinishedPouring && mColliderBuffer[0])
    //     {
    //         if (!mHasStartedToPour)
    //         {
    //             mHasStartedToPour = true;
    //             Bowl.SetAddBowlManual(BowlColourChange, FillSpeed);
    //         }
    //         else if (Bowl.UpdateAddBowlManual())
    //         {
    //             InventoryVerifier inv = mColliderBuffer[0].GetComponent<InventoryVerifier>();
    //             if (inv) inv.ExternalInsertItemNonReaction(gameObject);

    //             Debug.Log("Hello");
    //             mHasFinishedPouring = true; 
    //             mHasStartedToPour = false;
    //         }
    //     }
    // }
}
