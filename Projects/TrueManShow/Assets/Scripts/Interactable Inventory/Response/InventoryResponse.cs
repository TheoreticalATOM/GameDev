using UnityEngine;

public abstract class InventoryResponse : MonoBehaviour
{
    public InventoryResponse Child;

    public void Respond(bool verificationIsPure)
    {
        if (verificationIsPure)
            OnSuccessResponse();
        else
            OnFailResponse();

        // If there is a child response, then continue down the line
        if (Child)
            Child.Respond(verificationIsPure);
    }

    protected abstract void OnFailResponse();
    protected abstract void OnSuccessResponse();

}
