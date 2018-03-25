using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCamera : MonoBehaviour
{
    public Transform Target { get; private set; }
	
    public void SetTarget(Transform NewTarget, bool follow)
    {
		Target = NewTarget;
		enabled = follow;
    }
	
    private void Update()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
		Vector3 displacement = Target.position - transform.position;
		transform.Translate(displacement.x, displacement.y, 0.0f);
    }
}
