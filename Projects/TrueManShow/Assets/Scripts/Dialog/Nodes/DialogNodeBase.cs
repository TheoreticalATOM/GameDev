using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class DialogNodeBase : SerializedScriptableObject 
{
	public abstract void Play(System.Action onFinishedCallback);

	public void Play()
	{
		Play(null);
	}
}

[System.Serializable]
public class Segment
{
    [Header("Segment:")]
    [TextArea(3, 15)]
    public string Text;
    [MinValue(0.0f)] public float DelayInSeconds;
    [MinValue(0.0f)] public float DurationInSeconds;
    public AudioClip Clip;

    [Button]
    public void MatchDurationWithClipLength()
    {
        if(Clip)
            DurationInSeconds = Clip.length;
    }
}