using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SDE.Data;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Dialog Node Event", menuName = "Dialog/Node Event", order = 0)]
public class DialogNodeEvent : DialogNode 
{
	public GameEvent OnNodeFinished;

	public override void Play(System.Action OnFinishedCallback)
	{
		Assert.IsNotNull(OnNodeFinished, name + " is missing a game event");
		base.Play(() =>
		{
			OnFinishedCallback();
			OnNodeFinished.Raise();
		});
	}
}
