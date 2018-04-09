using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parenter : MonoBehaviour
{
    private Transform mDefaultParent;

    private void Awake()
    {
        mDefaultParent = transform.parent;
    }

	public void SetParent(Transform parent)
	{
		SetParentAndReset(parent);
	}

	public void ClearParent()
	{
		SetParentAndReset(mDefaultParent);
	}

	private void SetParentAndReset(Transform parent)
	{
		transform.SetParent(parent);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}
}
