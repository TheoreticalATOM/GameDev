using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRDirector : InventoryResponse
{
	public float SucpisionAmount;
	public DirectorAwarenessValue Sucpision;
    
	protected override void OnFailResponse()
    {
        Sucpision.UpdateValue(SucpisionAmount);
    }
    protected override void OnSuccessResponse() { }
}
