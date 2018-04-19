﻿using System.Collections;
using SDE.Data;
using SDE;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class NarrativeSource : MonoBehaviour, IRuntime
{
    public RuntimeSet Source;
    public RuntimeSet DialogUISet;

#if UNITY_EDITOR
    public bool LogSegments;
    private Coroutine mLogRoutine;
#endif

    public UnityEvent OnPlayed;
    public UnityEvent OnStopped;

    private AudioSource mSource;
    private Coroutine mDialogCoroutine;

    private void Awake()
    {
        mSource = GetComponent<AudioSource>();
        Source.Add(this);
    }

    /// <summary>
    /// Will play the given audio clip at it's location, and invoke a OnPlayed event.
    /// Any current clip will be overwritten
    /// </summary>
    public void Play(Segment[] segments, System.Action onFinished, bool showDialog = true)
    {
        if (mDialogCoroutine != null)
            StopCoroutine(mDialogCoroutine);

        OnPlayed.Invoke();
        mDialogCoroutine = StartCoroutine(DialogSegementsRoutine(segments, onFinished, showDialog));
    }

    public void PlayOne(Segment segment, System.Action onFinished, bool showDialog = true)
    {
        if (mDialogCoroutine != null)
            StopCoroutine(mDialogCoroutine);

        mDialogCoroutine = StartCoroutine(DialogSingleSegmentRoutine(segment, onFinished, showDialog));
    }

    private IEnumerator DialogSingleSegmentRoutine(Segment segment, System.Action onFinished, bool showDialog)
    {
        float delay, duration;
        GetDelayAndDuration(segment, out delay, out duration);
        
        DialogUI ui = DialogUISet.GetFirst<DialogUI>();
        Assert.IsNotNull(ui, "does not have a DialogUI");
        
        yield return new WaitForSeconds(delay);
        PlayClip(segment.Clip);

        if (showDialog) ui.SetText(segment.Text);
        yield return new WaitForSeconds(duration);
        if (showDialog) ui.ClearText();
        
        onFinished.TryInvoke();
    }

    private IEnumerator PrintAudioStartTimeRoute()
    {
        float startTime = Time.time;
        while (true)
        {
            Debug.Log(Time.time - startTime);
            yield return null;
        }
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip)
        {
            mSource.clip = clip;
            mSource.Play();
        }
    }

    private void GetDelayAndDuration(Segment segment, out float delay, out float duration)
    {
        delay = segment.DelayInSeconds;
        duration = segment.DurationInSeconds;

#if UNITY_EDITOR
        if (DialogNodeBase.IS_DEBUG)
        {
            duration = Segment.DEBUG_DURATION;
            delay = Segment.DEBUG_DELAY;
        }
#endif
    }

    private IEnumerator DialogSegementsRoutine(Segment[] segments, System.Action onFinished, bool showDialog)
    {
        float delay, duration;

        DialogUI ui = DialogUISet.GetFirst<DialogUI>();
        foreach (Segment segment in segments)
        {
            GetDelayAndDuration(segment, out delay, out duration);

            yield return new WaitForSeconds(delay);
            PlayClip(segment.Clip);

            if (showDialog) ui.SetText(segment.Text);
            
            #if UNITY_EDITOR
            if (LogSegments) mLogRoutine = StartCoroutine(PrintAudioStartTimeRoute());
            #endif
            
            yield return new WaitForSeconds(duration);

            #if UNITY_EDITOR
            if (LogSegments) StopCoroutine(mLogRoutine);
            #endif            
        }

        if (showDialog) ui.ClearText();

        if (onFinished != null)
            onFinished();

        OnStopped.Invoke();
    }
}