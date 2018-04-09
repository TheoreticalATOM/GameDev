using UnityEngine;

public class TCAnimation : TogglerComponent
{
    public Animator Animator;
    public ScriptableAnimation AnimationA;
    public ScriptableAnimation AnimationB;
    protected override void OnToggleA() { AnimationA.SetValue(Animator); }
    protected override void OnToggleB() { AnimationB.SetValue(Animator); }
}
