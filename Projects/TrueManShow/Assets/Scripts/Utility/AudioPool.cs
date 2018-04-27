using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using SDE;
using SDE.Data;
using Sirenix.OdinInspector;

public class AudioPool : SerializedMonoBehaviour, IRuntime
{
    public class LoopSource
    {
        private AudioSource mSource;
        
        public LoopSource(AudioSource source, AudioClip clip)
        {
            mSource = source;
            mSource.loop = true;
            mSource.clip = clip;
            mSource.Play();
        }
    }
    
    
    public RuntimeSet Set;
    public AudioSource DefaultSource;
    [Range(0.0f, 1.0f)] public float Volume = 1.0f;
    [Range(0.0f, 1.0f)] public float SpactialBlend = 1.0f;
    [MinValue(1)] public int PoolSize;
    [MinValue(1)] public int LoopedPoolSize;
    public bool IsDefaultAllowed { get; set; }

    private Queue<AudioSource> mLoopSources;
    private Queue<AudioSource> mAudioSources;
    private AudioSource mMusicSource;

    private void Awake()
    {
        mMusicSource = gameObject.AddComponent<AudioSource>();
        mMusicSource.spatialBlend = SpactialBlend;
        mMusicSource.volume = Volume;

        mAudioSources = new Queue<AudioSource>();
        for (int i = 0; i < PoolSize; i++)
            mAudioSources.Enqueue(AddNewDefaultSource());

        mLoopSources = new Queue<AudioSource>();
        for (int i = 0; i < LoopedPoolSize; i++)
        {
            AudioSource source = AddNewDefaultSource();
            source.loop = true;
            mLoopSources.Enqueue(source);
        }
        
        Set.Add(this);
    }

    private void OnDestroy()
    {
        Set.Remove(this);
    }

    private void Start()
    {
        IsDefaultAllowed = true;
    }

    private AudioSource AddNewDefaultSource()
    {
        return gameObject.AddComponent<AudioSource>((source) =>
        {
            source.spatialBlend = SpactialBlend;
            source.volume = Volume;
        });
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
        AudioSource source = FetchSource(mAudioSources);
        if (source)
        {
            source.clip = clip;
            source.Play();
        }
    }

    public AudioSource GetLoop(AudioClip clip)
    {
        AudioSource source = FetchSource(mAudioSources);
        if (source)
        {
            source.clip = clip;
            source.Play();
        }
        return source;
    }

    private AudioSource FetchSource(Queue<AudioSource> sourceCollection)
    {
        for (int i = 0; i < sourceCollection.Count; i++)
        {
            AudioSource source = sourceCollection.DequeueAndEnqueueToBack();
            if (!source.isPlaying)
                return source;
        }
        return null;
    }
    

    public void StopAllClips()
    {
        foreach (AudioSource source in mAudioSources)
            source.Stop();

        foreach (AudioSource source in mLoopSources)
            source.Pause();
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
