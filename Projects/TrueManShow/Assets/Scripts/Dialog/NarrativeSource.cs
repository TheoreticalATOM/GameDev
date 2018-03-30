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
    public void Play(Segment[] segments, System.Action OnFinished)
    {
        if (mDialogCoroutine != null)
            StopCoroutine(mDialogCoroutine);

        OnPlayed.Invoke();
        mDialogCoroutine = StartCoroutine(DialogSegementsRoutine(segments, OnFinished));
    }
    public void PlayOne(Segment segment, System.Action onFinished)
    {
        if (mDialogCoroutine != null)
            StopCoroutine(mDialogCoroutine);

        mDialogCoroutine = StartCoroutine(DialogBasicSegmentRoutine(segment, onFinished));
    }

    private IEnumerator DialogBasicSegmentRoutine(Segment segment, System.Action onFinished)
    {
        DialogUI ui = DialogUISet.GetFirst<DialogUI>();
        Assert.IsNotNull(ui, "does not have a DialogUI");

        yield return new WaitForSeconds(segment.DelayInSeconds);
        PlayClip(segment.Clip);
        ui.SetText(segment.Text);
        yield return new WaitForSeconds(segment.DurationInSeconds);

        onFinished.TryInvoke();
        ui.SetText("");
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip)
        {
            mSource.clip = clip;
            mSource.Play();
        }
    }

    private IEnumerator DialogSegementsRoutine(Segment[] segments, System.Action OnFinished)
    {
        DialogUI ui = DialogUISet.GetFirst<DialogUI>();
        foreach (Segment segment in segments)
        {
            yield return new WaitForSeconds(segment.DelayInSeconds);
            PlayClip(segment.Clip);

            ui.SetText(segment.Text);
            yield return new WaitForSeconds(segment.DurationInSeconds);
        }

        ui.SetText("");

        if (OnFinished != null)
            OnFinished();

        OnStopped.Invoke();
    }
}
