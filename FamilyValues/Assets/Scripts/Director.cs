using System.Collections;
using System.Collections.Generic;
using SDE.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Director : SerializedMonoBehaviour
{
    // ________________________________________________________ Inspector Content
    [TextArea(1, 3)] public string LevelName;
    
    [Header("Suspicion")]
    public FloatVariable MaxSuspicion;
    public FloatVariable CurrentSuspicion;
    public LoadingBar SuspicionLoadingBar;

    [Header("GameOver Event")]
    public UnityEvent MaxedOnSuspicion;

    // ________________________________________________________ Datamembers

    // ________________________________________________________ Getters
    public float CurrentSuspicionInPercentage { get { return CurrentSuspicion.Value / MaxSuspicion.Value; } }

    // ________________________________________________________ Controls
    public void IncreaseSuspicion(float amount)
    {
        CurrentSuspicion.Value = Mathf.Min(CurrentSuspicion.Value + Mathf.Abs(amount), MaxSuspicion.Value);
        UpdateSuspiciousUI();

        if (CurrentSuspicion.Value >= MaxSuspicion.Value)
            MaxedOnSuspicion.Invoke();
    }
    public void DecreaseSuspicion(float amount)
    {
        CurrentSuspicion.Value = Mathf.Max(CurrentSuspicion.Value - Mathf.Abs(amount), 0.0f);
        UpdateSuspiciousUI();        
    }

    // ________________________________________________________ Methods
    private void UpdateSuspiciousUI()
    {
        SuspicionLoadingBar.UpdateBar(CurrentSuspicion.Value, MaxSuspicion.Value);
    }
}
