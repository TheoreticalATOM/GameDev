using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IRFillStream : InventoryResponse
{
	public BowlFilling Bowl;
    public Color ColourChange;
    public float FillSpeed;

	public bool CheckDistance {get;private set;}
    public UnityEvent OnFilledEnough;

    protected override void OnFailResponse()
    {
    }

    protected override void OnSuccessResponse()
    {
        if(Bowl.BowlManualInsertion(ColourChange, FillSpeed, null))
            OnFilledEnough.Invoke();
    }
}
