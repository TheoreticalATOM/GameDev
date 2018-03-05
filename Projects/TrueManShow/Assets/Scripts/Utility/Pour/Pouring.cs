using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Pouring : SerializedMonoBehaviour
{
    public ParticleSystem Particles;

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

    void FixedUpdate()
    {
		Vector3 particlePos = Particles.transform.position;

        Vector3 towardsPoint = particlePos - transform.position;
        float angle = Mathf.Atan2(towardsPoint.y, towardsPoint.x);

        if (angle < 0.0f)
		{
            Particles.Play();

			Vector3 boxPos = particlePos + ColliderOffset;
			if(Physics.OverlapBoxNonAlloc(boxPos, ColliderSize, mColliderBuffer, Quaternion.identity, ColliderMask) > 0)
			{
				Debug.Log("Hello");
			}
		}
    }
}
