using System;
using System.Collections;
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
    public bool ClearTextOnCompletion = true;
    
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

    private void OnDestroy()
    {
        Source.Remove(this);
    }

    /// <summary>
    /// Will play the given audio clip at it's location, and invoke a OnPlayed event.
    /// Any current clip will be overwritten
    /// </summary>
    public void Play(Segment[] segments, System.Action onFinished)
    {
        if (mDialogCoroutine != null)
            StopCoroutine(mDialogCoroutine);

        OnPlayed.Invoke();
        
        DialogUI ui = DialogUISet.GetFirst<DialogUI>();
        Assert.IsNotNull(ui, "does not have a DialogUI");
        mDialogCoroutine = StartCoroutine(DialogSegementsRoutine(segments, onFinished));
    }

    public void PlayOne(Segment segment, System.Action onFinished)
    {
        OnPlayed.Invoke();
        if (mDialogCoroutine != null)
            StopCoroutine(mDialogCoroutine);

        mDialogCoroutine = StartCoroutine(DialogSingleSegmentRoutine(segment, onFinished));
    }

    public void PlayOneShotAudioClip(AudioClip clip)
    {
        mSource.PlayOneShot(clip);
    }

    public void ClearText()
    {
        DialogUI ui = DialogUISet.GetFirst<DialogUI>();
        Assert.IsNotNull(ui, "does not have a DialogUI");
        ui.ClearText();
    }

    public void StopDialog()
    {
        StopAllCoroutines();
        mSource.Stop();
    }

    private IEnumerator DialogSingleSegmentRoutine(Segment segment, System.Action onFinished)
    {
        float delay, duration;
        GetDelayAndDuration(segment, out delay, out duration);

        DialogUI ui = DialogUISet.GetFirst<DialogUI>();
        Assert.IsNotNull(ui, "does not have a DialogUI");

        yield return new WaitForSeconds(delay);
        PlayClip(segment.Clip);
        
        ui.SetText(segment.Text);
        yield return new WaitForSeconds(duration);
        
        if(ClearTextOnCompletion)
            ui.ClearText();

        onFinished.TryInvoke();
        
        OnStopped.Invoke();
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

    private IEnumerator DialogSegementsRoutine(Segment[] segments, System.Action onFinished)
    {
        DialogUI ui = DialogUISet.GetFirst<DialogUI>();
        foreach (Segment segment in segments)
        {
            float delay, duration;
            GetDelayAndDuration(segment, out delay, out duration);

            yield return new WaitForSeconds(delay);
            PlayClip(segment.Clip);

            ui.SetText(segment.Text);

            #if UNITY_EDITOR
            if (LogSegments) mLogRoutine = StartCoroutine(PrintAudioStartTimeRoute());
            #endif

            yield return new WaitForSeconds(duration);

            #if UNITY_EDITOR
            if (LogSegments) StopCoroutine(mLogRoutine);
            #endif
        }

        if(ClearTextOnCompletion)
            ui.ClearText();

        if (onFinished != null)
            onFinished();

        OnStopped.Invoke();
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
}