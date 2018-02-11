using System.Collections;
using System.Collections.Generic;
using SDE.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Director : SerializedMonoBehaviour, IRuntime
{
    // ________________________________________________________ Inspector Content
    [Required] public RuntimeSet DirectorSet;
    [MinValue(1)] public float MaxSuspicion;

    [Header("UI")]
    public RuntimeSet SuspicionLoadingBar;
    
    public UnityEvent MaxedOnSuspicion;

    // ________________________________________________________ Datamembers

    // ________________________________________________________ Getters
    public float CurrentSuspicion { get; private set; }
    public float CurrentSuspicionInPercentage { get { return CurrentSuspicion / MaxSuspicion; } }

    // ________________________________________________________ Controls
    public void IncreaseSuspicion(float amount)
    {
        CurrentSuspicion = Mathf.Min(CurrentSuspicion + Mathf.Abs(amount), MaxSuspicion);
        UpdateSuspiciousUI();
        
        if (CurrentSuspicion >= MaxSuspicion)
            MaxedOnSuspicion.Invoke();
    }
    public void DecreaseSuspicion(float amount)
    {
        CurrentSuspicion = Mathf.Max(CurrentSuspicion - Mathf.Abs(amount), 0.0f);
        UpdateSuspiciousUI();
    }

    // ________________________________________________________ Methods
    private void Awake()
    {
        DirectorSet.Add(this);
        CurrentSuspicion = 0.0f;
        DontDestroyOnLoad(this);

        UpdateSuspiciousUI();        
    }

    private void UpdateSuspiciousUI()
    {
        if(!SuspicionLoadingBar.IsEmpty)
            SuspicionLoadingBar.GetFirst<RuntimeLoadingBar>().LoadingBar.UpdateBar(CurrentSuspicion, MaxSuspicion);
    }
}
