using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using SDE.Data;
using SDE;

[CreateAssetMenu(fileName = "Dialog Node Dynamic", menuName = "Dialog/Dynamic Node", order = 0)]
public class DialogNodeDynamic : DialogNodeBase
{
	public int Priority;
    public List<SegmentContained> Segments;

    public override void Play(System.Action onFinishedCallback)
    {
        PlayASyncRecursive(0, onFinishedCallback);
    }

    private void PlayASyncRecursive(int index, System.Action onFinishedCallback)
    {
        if (index > Segments.Count - 1)
        {
            onFinishedCallback.TryInvoke();
            return;
        }

        // get the current segment and increment the index
        SegmentContained segment = Segments[index++];

        /* fetch the narrative source */
        Assert.IsFalse(segment.NarrativeSourceSet.IsEmpty, "Missing an NarrativeSource in the scene");
        NarrativeSource source = segment.NarrativeSourceSet.GetFirst<NarrativeSource>();
        Assert.IsNotNull(source, "The RuntimeSet, is not set by an NarrativeSource");

        // play a basic segment
        source.PlayOne(segment, () => PlayASyncRecursive(index, onFinishedCallback), Priority);
    }


#if UNITY_EDITOR
	[Header("Configuration:")]
    public DialogNode LoadInDialogNode;

	[Button()]
    public void LoadDialogNode()
	{
		if(!LoadInDialogNode) return;

		RuntimeSet set = LoadInDialogNode.NarrativeSourceSet;
		SegmentContained[] newSegments = new SegmentContained[LoadInDialogNode.Segments.Length];
		for (int i = 0; i < newSegments.Length; i++)
		{
			Segment oldSegment = LoadInDialogNode.Segments[i];
			newSegments[i] = new SegmentContained()
			{
				Text = oldSegment.Text,
				DelayInSeconds = oldSegment.DelayInSeconds,
				DurationInSeconds = oldSegment.DurationInSeconds,
				Clip = oldSegment.Clip,
				NarrativeSourceSet = set
			};
		}
		
		Segments.AddRange(newSegments);
		Debug.Log(newSegments.Length + " segments added");
		LoadInDialogNode = null;
	}
#endif
}

[System.Serializable]
public class SegmentContained : Segment
{
    public RuntimeSet NarrativeSourceSet;
}