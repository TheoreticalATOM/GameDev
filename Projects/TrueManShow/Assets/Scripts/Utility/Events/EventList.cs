using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EEventListCompletion
{
    Stop = 0, Repeat, Repeat_Last
}

public class EventList : MonoBehaviour
{
    private delegate bool DelEvaluation(ref int index, UnityEvent[] events);
    private static readonly DelEvaluation[] EVENT_RESPONSE = new DelEvaluation[]
    {
        EvaluationStop, EvaluationRepeat, EvaluationRepeatLast
    };

    public EEventListCompletion OnCompletion;
    public UnityEvent[] Events;

    private int Index;

    public void RaiseEvent()
    {
        if (EVENT_RESPONSE[(int)OnCompletion](ref Index, Events))
            Events[Index].Invoke();
    }

    public void Next()
    {
        Index++;
    }

    public void NextAndRaise()
    {
        Next();
        RaiseEvent();
    }

    public void RaiseAndNext()
    {
        RaiseEvent();
        Next();
    }

    public void SetToLast()
    {
        Index = Events.Length - 1;
    }

    // ______________________________________________________
    // @ Evaluation
    private static bool EvaluationStop(ref int index, UnityEvent[] events)
    {
        return index < events.Length;
    }
    private static bool EvaluationRepeat(ref int index, UnityEvent[] events)
    {
        if (index >= events.Length)
            index = 0;

        return true;
    }
    private static bool EvaluationRepeatLast(ref int index, UnityEvent[] events)
    {
        index = Mathf.Min(index, events.Length - 1);
        return true;
    }
}
