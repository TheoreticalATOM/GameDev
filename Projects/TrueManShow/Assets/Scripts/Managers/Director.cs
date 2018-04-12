using System.Collections;
using System.Collections.Generic;
using SDE.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using SDE.UI;

public class Director : SerializedMonoBehaviour
{
    // ________________________________________________________ Inspector
    [TextArea(1, 3)] public string LevelName;
    public DayCycle DayNightCycle;
    public string NextLevelName;
    [TabGroup("Suspicion")] public DirectorAwarenessValue CurrentSuspicion;
    [TabGroup("Suspicion")] public Progress SuspicionLoadingBar;
    [TabGroup("Suspicion")] public GameOver GameOver;
    [TabGroup("Suspicion")] public UnityEvent MaxedOnSuspicion;

    [TabGroup("Appearance")] public DirectorAwarenessValue CurrentAppearance;
    [TabGroup("Appearance")] public Progress AppearanceLoadingBar;

    // ________________________________________________________ Methods
    private void Awake()
    {
        CurrentSuspicion.ResetValue();
        CurrentAppearance.ResetValue();

        DayNightCycle.OnAllSegmentsCompleted.AddListener(() => SceneManager.LoadScene(NextLevelName));
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
        SuspicionLoadingBar.UpdateProgress(newValue, CurrentSuspicion.MaxValue);

        // if the supisicon exceeds the max suspicion, then call the event
        if (CurrentSuspicion.IsMaxedOut)
        {
            MaxedOnSuspicion.Invoke();

            string sceneName = (DayNightCycle.IsDayTime) ? SceneManager.GetActiveScene().name : NextLevelName;
            GameOver.ShowGameOver(sceneName, () => CurrentSuspicion.Value = 0.0f);
        }
    }

    private void OnAppearanceChanged(float newValue)
    {
        AppearanceLoadingBar.UpdateProgress(newValue, CurrentAppearance.MaxValue);
    }    

}
