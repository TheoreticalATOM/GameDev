using UnityEngine;
using SDE;
using UnityEngine.Events;

public class IIVStaticItemEquality : IIVerifier
{
    [Header("Static Items")]
    public ItemPhysicsInteract[] Items;
    public UnityEvent OnAllItemsAdded;

    private int mCount = 0;

    protected override bool OnVerifiying(ItemPhysicsInteract item)
    {
        foreach (ItemPhysicsInteract i in Items)
        {
            if (item == i)
            {
                if (++mCount >= Items.Length)
                    OnAllItemsAdded.Invoke();
                return true;
            }
        }
        return false;
    }
}
