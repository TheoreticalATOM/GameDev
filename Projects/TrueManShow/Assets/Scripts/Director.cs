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
    [TabGroup("Suspicion")] public DirectorAwarenessValue CurrentSuspicion;
    [TabGroup("Suspicion")] public LoadingBar SuspicionLoadingBar;
    [TabGroup("Suspicion")] public UnityEvent MaxedOnSuspicion;

    [TabGroup("Appearance")] public DirectorAwarenessValue CurrentAppearance;
    [TabGroup("Appearance")] public LoadingBar AppearanceLoadingBar;

    // ________________________________________________________ Methods
    private void Awake()
    {
        CurrentSuspicion.ResetValue();
        CurrentAppearance.ResetValue();
    }

    private void OnEnable()
    {
        CurrentSuspicion.OnValueChangedEvent += OnSuspicionChanged;
        CurrentAppearance.OnValueChangedEvent += OnAppearanceChanged;
    }

    private void OnDisable()
    {
        CurrentSuspicion.OnValueChangedEvent -= OnSuspicionChanged;
        CurrentAppearance.OnValueChangedEvent -= OnAppearanceChanged;
    }

    private void OnSuspicionChanged(float newValue)
    {
        SuspicionLoadingBar.UpdateBar(newValue, CurrentSuspicion.MaxValue);

        // if the supisicon exceeds the max suspicion, then call the event
        if (CurrentSuspicion.IsMaxedOut)
            MaxedOnSuspicion.Invoke();
    }

    private void OnAppearanceChanged(float newValue)
    {
        AppearanceLoadingBar.UpdateBar(newValue, CurrentAppearance.MaxValue);
        
    }    

}
