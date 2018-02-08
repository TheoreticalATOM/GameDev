using System.Collections;
using System.Collections.Generic;
using SDE.Data;
using Sirenix.OdinInspector;
using UnityEngine;

public class Director : SerializedMonoBehaviour, IRuntime
{
    // ________________________________________________________ Inspector Content
    [Required] public RuntimeSet DirectorSet;
    [MinValue(1)] public float MaxSuspicion;

    // ________________________________________________________ Datamembers

    // ________________________________________________________ Getters
    public float CurrentSuspicion { get; private set; }

    // ________________________________________________________ Controls
    public void IncreaseSuspicion(float amount)
    {
        CurrentSuspicion = Mathf.Min(CurrentSuspicion + Mathf.Abs(amount), MaxSuspicion);
    }

    public void DecreaseSuspicion(float amount)
    {
        CurrentSuspicion = Mathf.Max(CurrentSuspicion - Mathf.Abs(amount), 0.0f);
    }

    // ________________________________________________________ Methods
    private void Awake()
    {
        DirectorSet.Add(this);
        CurrentSuspicion = 0.0f;
		DontDestroyOnLoad(this);
    }
}
