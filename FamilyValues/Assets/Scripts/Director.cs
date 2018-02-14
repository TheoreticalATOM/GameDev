using System.Collections;
using System.Collections.Generic;
using SDE.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Director : SerializedMonoBehaviour
{
    // ________________________________________________________ Inspector
    [TextArea(1, 3)] public string LevelName;
    
    [Header("Suspicion")]
    public FloatVariable MaxSuspicion;
    public FloatVariable CurrentSuspicion;
    public LoadingBar SuspicionLoadingBar;

    [Header("GameOver Event")]
    public UnityEvent MaxedOnSuspicion;

    // ________________________________________________________ Datamembers
    /* Is used to reset the CurrentSuspicion variable to 0 if the game hasn't run.
    The reason for this reset, is because it is a ScriptableObject, and it serializes itself on exit */
    private static bool HasRunBefore = false;

    // ________________________________________________________ Getters
    /// <summary>
    /// Returns the current suspicion in percentage (0 -> 1)
    /// </summary>
    public float CurrentSuspicionInPercentage { get { return CurrentSuspicion.Value / MaxSuspicion.Value; } }

    // ________________________________________________________ Controls
    #region Suspicion
    /// <summary>
    /// Increases the suspicion meter by a value. If the value exceeds the max allowed suspicion,
    /// then the MaxOnSuspicion Event is calledl. Upon calling this method, the UI is updated appropraitely
    /// </summary>
    public void IncreaseSuspicion(float amount)
    {
        CurrentSuspicion.Value = Mathf.Min(CurrentSuspicion.Value + Mathf.Abs(amount), MaxSuspicion.Value);
        UpdateSuspiciousUI();

        // if the supisicon exceeds the max suspicion, then call the event
        if (CurrentSuspicion.Value >= MaxSuspicion.Value)
            MaxedOnSuspicion.Invoke();
    }

    /// <summary>
    /// Decreases the suspicion meter by a value. The value is clamped to 0.
    /// Upon calling this method, the UI is updated appropraitely
    /// </summary>
    public void DecreaseSuspicion(float amount)
    {
        CurrentSuspicion.Value = Mathf.Max(CurrentSuspicion.Value - Mathf.Abs(amount), 0.0f);
        UpdateSuspiciousUI();        
    }
    #endregion
    // ________________________________________________________ Methods
    private void Awake()
    {
        if(!HasRunBefore)
        {
            HasRunBefore = true;
            CurrentSuspicion.Value = 0;
        }
    }

    private void UpdateSuspiciousUI()
    {
        SuspicionLoadingBar.UpdateBar(CurrentSuspicion.Value, MaxSuspicion.Value);
    }
}
