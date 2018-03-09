using UnityEngine;

public abstract class InventoryResponse : MonoBehaviour 
{
	public void Respond(bool verificationIsPure)
	{
		if(verificationIsPure)
			OnSuccessResponse();
		else
			OnFailResponse();
	}

	protected abstract void OnFailResponse();
	protected abstract void OnSuccessResponse();

}
