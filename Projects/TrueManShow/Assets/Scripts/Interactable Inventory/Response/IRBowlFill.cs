using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IRBowlFill : InventoryResponse
{
    public Color ColourChange;
    public BowlFilling Bowl;

    protected override void OnFailResponse()
    {
		Bowl.AddToBowl(ColourChange);
    }

    protected override void OnSuccessResponse()
    {
		Bowl.AddToBowl(ColourChange);
    }
}
