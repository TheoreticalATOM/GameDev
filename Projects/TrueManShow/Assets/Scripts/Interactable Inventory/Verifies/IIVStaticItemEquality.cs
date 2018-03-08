using UnityEngine;
using SDE;

public class IIVStaticItemEquality : IIVerifier
{
	[Header("Static Items")]
	public ItemPhysicsInteract[] Items;

    protected override bool OnVerifiying(ItemPhysicsInteract item)
    {
      foreach (ItemPhysicsInteract i in Items)
      {
          if(item == i)
            return true;
      }
      return false;
    }
}
