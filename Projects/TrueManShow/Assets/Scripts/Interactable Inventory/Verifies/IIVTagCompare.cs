using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IIVTagCompare : IIVerifier
{
	public string RequiredTag;
	protected override bool OnVerifiying(ItemPhysicsInteract item)
	{
		return item.CompareTag(RequiredTag);
	}
}
