using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCLightSwitch : TogglerComponentBool
{
	public Light Light;
	protected override void OnToggled(bool value)
	{
		Light.enabled = value;
	}
	
	private void Reset()
	{
		if (!Light) Light = GetComponent<Light>();
	}
}
