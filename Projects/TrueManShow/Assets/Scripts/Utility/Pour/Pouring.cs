﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Pouring : SerializedMonoBehaviour
{
    public ParticleSystem Particles;
    public BowlFilling Bowl;
	public Color BowlColourChange;
	public float FillSpeed;

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

    void FixedUpdate()
    {
        Vector3 particlePos = Particles.transform.position;
        Vector3 towardsPoint = particlePos - transform.position;

        if (Mathf.Atan2(towardsPoint.y, towardsPoint.x) > 0.0f)
        {
            Bowl.ClearAddBowlManual();
            Particles.Stop();
            return;
        }

        // Play particle system
        Particles.Play();

        // Check collision area if it hit a bowl
        Vector3 boxPos = particlePos + ColliderOffset;
        Physics.OverlapBoxNonAlloc(boxPos, ColliderSize, mColliderBuffer, Quaternion.identity, ColliderMask);

        if (mColliderBuffer[0] && !mIsPouringIntoBowl)
        {
            mIsPouringIntoBowl = true;
            Bowl.SetAddBowlManual(BowlColourChange, FillSpeed, () =>
            { 
				Debug.Log("DONE!");
				mIsPouringIntoBowl = false;
			});
        }
        
		if (mColliderBuffer[0])
        {
            Bowl.UpdateAddBowlManual();
        }
    }
}
