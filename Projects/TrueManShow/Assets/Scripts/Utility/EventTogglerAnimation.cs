using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTogglerAnimation : EventToggler
{
    public Animator Animator;
    public CinemaDetail AnimationA;
    public CinemaDetail AnimationB;

    protected override void ToggleA()
    {
        AnimationA.SetValue(Animator);
        base.ToggleA();
    }

    protected override void ToggleB()
    {
        AnimationB.SetValue(Animator);
        base.ToggleA();
    }
}
