using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public abstract class DialogNodeBase : SerializedScriptableObject
{
#if UNITY_EDITOR
    public static bool IS_DEBUG = true;
#endif

    public abstract void Play(System.Action onFinishedCallback);

    public void Play()
    {
        Play(null);
    }
}

[System.Serializable]
public class Segment
{
#if UNITY_EDITOR
    public static float DEBUG_DELAY = 0.5f;
    public static float DEBUG_DURATION = 0.5f;
#endif

    [Header("Segment:")]
    [TextArea(3, 15)]
    public string Text;
    [MinValue(0.0f)] public float DelayInSeconds;
    [MinValue(0.0f)] public float DurationInSeconds;
    public AudioClip Clip;

    [Button]
    public void MatchDurationWithClipLength()
    {
        if (Clip)
            DurationInSeconds = Clip.length;
    }
}