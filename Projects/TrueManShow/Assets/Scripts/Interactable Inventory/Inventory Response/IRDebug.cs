using UnityEngine;

public class IRDebug : InventoryResponse
{
    protected override void OnFailResponse()
	{
		Debug.Log(name + " response: FAILED!" );
	}
    protected override void OnSuccessResponse()
	{
		Debug.Log(name + " response: SUCCESS!");
	}
}
