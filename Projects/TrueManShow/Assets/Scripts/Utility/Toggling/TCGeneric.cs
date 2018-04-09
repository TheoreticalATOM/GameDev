using UnityEngine.Events;
public class TCGeneric : TogglerComponent
{
    public UnityEvent A;
    public UnityEvent B;
    protected override void OnToggleA() { A.Invoke(); }
    protected override void OnToggleB() { B.Invoke(); }
}
