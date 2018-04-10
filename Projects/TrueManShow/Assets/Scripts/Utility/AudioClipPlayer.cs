using UnityEngine;
using Sirenix.OdinInspector;

using SDE;

[RequireComponent(typeof(AudioSource))]
public class AudioClipPlayer : SerializedMonoBehaviour
{
    [MinMaxSlider(-3.0f, 3.0f, true)] public Vector2 MinMaxPitch = new Vector2(0.8f, 1.0f);
    public AudioSource mSource;

    private void Awake()
    {
        if(!mSource)
            mSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        mSource.clip = clip;
		mSource.pitch = MinMaxPitch.Range();
		mSource.Play();
    }

    public void PlayRandomClip(AudioClip[] clips)
    {
        PlayClip(clips[Random.Range(0, clips.Length)]);
    }
}
