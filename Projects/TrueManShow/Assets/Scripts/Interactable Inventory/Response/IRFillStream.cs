using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRFillStream : InventoryResponse
{
	public BowlFilling Bowl;
    public Color ColourChange;
    public float FillSpeed;

	public bool CheckDistance {get;private set;}

    protected override void OnFailResponse()
    {

    }

    protected override void OnSuccessResponse()
    {
		CheckDistance = Bowl.BowlManualInsertion(ColourChange, FillSpeed, null);
    }
}
