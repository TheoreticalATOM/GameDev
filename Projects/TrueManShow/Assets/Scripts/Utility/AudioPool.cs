using System.Collections.Generic;
using UnityEngine;
using SDE;
using SDE.Data;
using Sirenix.OdinInspector;

public class AudioPool : SerializedMonoBehaviour, IRuntime
{
    public RuntimeSet Set;
    public AudioSource DefaultSource;
    [Range(0.0f, 1.0f)] public float Volume = 1.0f;
    [Range(0.0f, 1.0f)] public float SpactialBlend = 1.0f;
    [MinValue(1)] public int PoolSize;
    public bool IsDefaultAllowed { get; set; }

    Queue<AudioSource> mAudioSources;
    AudioSource mMusicSource;

    private void Awake()
    {
        mMusicSource = gameObject.AddComponent<AudioSource>();
        mMusicSource.spatialBlend = SpactialBlend;
        mMusicSource.volume = Volume;

        mAudioSources = new Queue<AudioSource>();
        for (int i = 0; i < PoolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.spatialBlend = SpactialBlend;
            source.volume = Volume;
            mAudioSources.Enqueue(source);
        }
        Set.Add(this);
    }

    private void Start()
    {
        IsDefaultAllowed = true;
    }

    public void PlaySong(AudioClip clip, bool loopSong)
    {
		if(clip)
		{
			mMusicSource.loop = loopSong;
			mMusicSource.clip = clip;
			mMusicSource.Play();
		}
    }

    public void DeafenSong()
    {
		mMusicSource.volume = 0.0f;
    }
	public void UnDeafenSong()
	{
		mMusicSource.volume = Volume;
	}

    public void StopSong()
    {
		mMusicSource.Stop();
    }

    public void PlayClip(AudioClip clip)
    {
        for (int i = 0; i < mAudioSources.Count; i++)
        {
            AudioSource source = mAudioSources.DequeueAndEnqueueToBack();
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.Play();
                return;
            }
        }
    }

    public void StopAllClips()
    {
        foreach (AudioSource source in mAudioSources)
            source.Stop();
    }

    public void PlayDefaultAndStopAllClips()
    {
        if (!IsDefaultAllowed)
            return;

        StopAllClips();
        if (DefaultSource) DefaultSource.Play();
    }
    public void StopDefault()
    {
        if (DefaultSource) DefaultSource.Stop();
    }

    public void StopAll()
    {
        StopAllClips();
        StopDefault();
    }
}
