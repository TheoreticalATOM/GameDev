using UnityEngine;
using UnityEngine.Events;

public class IRGeneric : InventoryResponse
{
	[Header("Events")]
	public UnityEvent OnFailed;
	public UnityEvent OnSuccess;

    protected override void OnFailResponse()
    {
		OnFailed.Invoke();
    }

    protected override void OnSuccessResponse()
    {
        OnSuccess.Invoke();
    }
}