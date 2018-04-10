using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCLightSwitch : TogglerComponent
{
	public Light Light;	
    protected override void OnToggleA()
    {
		Light.enabled = !Light.enabled;
    }

    protected override void OnToggleB()
    {
		Light.enabled = !Light.enabled;
    }
}
