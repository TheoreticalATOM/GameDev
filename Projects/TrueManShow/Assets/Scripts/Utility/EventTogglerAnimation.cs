using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationSwapper))]
public class EventTogglerAnimation : EventToggler
{
    private AnimationSwapper Animations;

    private void Awake()
    {
        Animations = GetComponent<AnimationSwapper>();
    }

    protected override void ToggleA()
    {
        base.ToggleA();
        Animations.AnimateAndSwap();
    }

    protected override void ToggleB()
    {
        base.ToggleB();
        Animations.AnimateAndSwap();
    }
}
