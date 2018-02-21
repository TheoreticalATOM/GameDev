using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "DirectorAwarenessValue", menuName = "Data/DirectorAwarenessValue", order = 0)]
public class DirectorAwarenessValue : SerializedScriptableObject
{
    // ____________________________________________ Inspector
    public float MaxValue;
    public float MinValue;
    public float Value;
    public bool ResetOnFirstRun;

    // ____________________________________________ Data
    [HideInInspector] public System.Action<float> OnValueChangedEvent;
    [System.NonSerialized] private bool mHasRunOnce;

    // ____________________________________________ Getters
    /// <summary>
    /// Returns the current suspicion in percentage (0 -> 1)
    /// </summary>
    public float ValueInPercentage { get { return (Value - MinValue) / (MaxValue - MinValue); } }

    /// <summary>
    /// Returns true if the value is equal to the maxvalue (or greater)
    /// </summary>
    public bool IsMaxedOut { get { return Value >= MaxValue; } }

    // ____________________________________________ Controls
    public void ResetValue()
    {
        if (ResetOnFirstRun && !mHasRunOnce)
        {
            Value = MinValue;
            mHasRunOnce = true;
        }
    }

    public void UpdateValue(float valueChange)
    {
        Assert.IsNotNull(OnValueChangedEvent, "OnValueChangedEvent is missing an event");

        float clampedValue = Mathf.Clamp(Value + valueChange, MinValue, MaxValue);
        Value = clampedValue;

        OnValueChangedEvent.Invoke(clampedValue);
    }

    #if UNITY_EDITOR
    [Button]
    public void InitiateGameOver()
    {

        if (OnValueChangedEvent != null)
        {
            Value = 100.0f;
            OnValueChangedEvent(Value);
        }
    }
    #endif
}