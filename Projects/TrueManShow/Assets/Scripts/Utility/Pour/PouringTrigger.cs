using Sirenix.OdinInspector;
using UnityEngine;

public class PouringTrigger : SerializedMonoBehaviour
{
    public ParticleSystem Particles;
    public AudioSource Source;

    public Vector3 ColliderOffset;
    public Vector3 ColliderSize;
    public LayerMask ColliderMask;

    private Collider[] mColliderBuffer = new Collider[1];

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
            Source.Stop();
            return null;
        }

        // Play particle system
        if(!Particles.isPlaying)
            Particles.Play();

        if(!Source.isPlaying)
            Source.Play();

        // Check collision area if it hit a bowl
        Vector3 boxPos = particlePos + ColliderOffset;
        Physics.OverlapBoxNonAlloc(boxPos, ColliderSize, mColliderBuffer, Quaternion.identity, ColliderMask);

        return mColliderBuffer[0];
    }
}
