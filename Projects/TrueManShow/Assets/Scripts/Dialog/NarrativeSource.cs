using System.Collections;
using SDE.Data;
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

    private IEnumerator DialogSegementsRoutine(Segment[] segments, System.Action OnFinished)
    {
        DialogUI ui = DialogUISet.GetFirst<DialogUI>();
        foreach (Segment segment in segments)
        {
            yield return new WaitForSeconds(segment.DelayInSeconds);
            if (segment.Clip)
            {
                mSource.clip = segment.Clip;
                mSource.Play();
            }

            ui.SetText(segment.Text);
            yield return new WaitForSeconds(segment.DurationInSeconds);
        }

        ui.SetText("");

        if (OnFinished != null)
            OnFinished();

        OnStopped.Invoke();
    }
}
