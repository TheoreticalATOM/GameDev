using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthyStealthCamera : MonoBehaviour
{
    public float MaxRotation;
    public float Speed;
    public LayerMask CollisionMask;
    public float DamageAmount;

    private StealthyStealthPlayer mPlayer;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, MaxRotation * Mathf.Sin(Time.time * Speed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        mPlayer = other.GetComponent<StealthyStealthPlayer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Vector3 thisPos = transform.position;
        Vector3 otherPos = other.transform.position;
        Vector3 towards = otherPos - thisPos;
		
		RaycastHit2D visibleCollider = Physics2D.Raycast(thisPos, towards.normalized, towards.sqrMagnitude, CollisionMask);
		if(visibleCollider.collider && visibleCollider.collider.gameObject == mPlayer.gameObject)
        	mPlayer.Damage(DamageAmount * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        mPlayer = null;
    }
}

