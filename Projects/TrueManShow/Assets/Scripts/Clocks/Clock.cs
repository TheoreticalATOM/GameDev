using System;
using System.Security;
using UnityEngine;
using SDE;
using Sirenix.Serialization;

public abstract class Clock : MonoBehaviour
{
    public event System.Action DurationMethodReachedCallback;

    // Use this for initialization
    public abstract void StartTime(int hr, int min, int dur);


    protected void ExecuteCallback()
    {
        DurationMethodReachedCallback.TryInvoke();
    }

    protected const float WAIT_TIME_SECONDS = 60;

    protected static int GetHour(int value)
    {
        return (value / 60) % 24;
    }
    protected static int GetMinute(int value)
    {
        return value % 60;
    }

    private void OnDestroy()
    {
        DurationMethodReachedCallback.RemoveAllListeners();
    }
}