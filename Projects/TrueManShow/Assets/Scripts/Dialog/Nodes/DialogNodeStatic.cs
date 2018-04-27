using System;
using UnityEngine;
using UnityEngine.Assertions;
using SDE.Data;

[CreateAssetMenu(fileName = "Dialog Node Static", menuName = "Dialog/Static Node", order = 0)]
public class DialogNodeStatic : DialogNodeBase
{
    public int Priority;
	public RuntimeSet NarrativeSourceSet;
    public Segment[] Segments;

    public override void Play(Action onFinishedCallback)
    {
        Assert.IsFalse(NarrativeSourceSet.IsEmpty, "Missing an NarrativeSource in the scene");
        NarrativeSource source = NarrativeSourceSet.GetFirst<NarrativeSource>();
        Assert.IsNotNull(source, "The RuntimeSet, is not set by an NarrativeSource");

        source.Play(Segments, onFinishedCallback, Priority);
    }
}
