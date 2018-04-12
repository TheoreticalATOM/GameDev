using UnityEngine;

[RequireComponent(typeof(Toggler))]
public abstract class TogglerComponent : MonoBehaviour
{
    public bool IsToggleA { get; private set; }

    public void SetToggleState(bool state)
    {
        IsToggleA = state;
        Toggle();
    }

    public void Toggle()
    {
        if (!IsToggleA) OnToggleA();
        else OnToggleB();
        IsToggleA = !IsToggleA;
    }

    protected abstract void OnToggleA();
    protected abstract void OnToggleB();
}
