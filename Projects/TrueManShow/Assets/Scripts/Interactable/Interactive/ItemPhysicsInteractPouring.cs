using UnityEngine;

public class ItemPhysicsInteractPouring : ItemPhysicsInteract
{
    public ParticleSystem Particles;
    public BowlFilling Bowl;
    public Vector3 ColliderOffset;
    public Vector3 ColliderSize;
    public LayerMask ColliderMask;

    private Collider[] mColliderBuffer = new Collider[1];
    private bool mIsPouringIntoBowl;

    private void OnDrawGizmos()
    {
        Vector3 particlePos = Particles.transform.position;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(ColliderOffset + particlePos, ColliderSize);
    }

    protected override bool OnInteract(GameObject player)
    {
        Vector3 particlePos = Particles.transform.position;
        Vector3 towardsPoint = particlePos - transform.position;

        if (Mathf.Atan2(towardsPoint.y, towardsPoint.x) > 0.0f)
        {
            Bowl.ClearAddBowlManual();
            Particles.Stop();
            return true;
        }

        // Play particle system
        Particles.Play();

        // Check collision area if it hit a bowl
        Vector3 boxPos = particlePos + ColliderOffset;
        Physics.OverlapBoxNonAlloc(boxPos, ColliderSize, mColliderBuffer, Quaternion.identity, ColliderMask);

        if (mColliderBuffer[0] && !mIsPouringIntoBowl)
        {
            mIsPouringIntoBowl = true;
            Bowl.SetAddBowlManual(Color.red, 1.0f, () =>
            {                
                mIsPouringIntoBowl = false;
            });
        }

        if (mColliderBuffer[0])
        {
            Bowl.UpdateAddBowlManual();
        }


        return true;
    }
}