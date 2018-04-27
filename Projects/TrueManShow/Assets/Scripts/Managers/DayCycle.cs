using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DayProperty
{
    [TabGroup("General")] public Transform SpawnPoint;
    [TabGroup("General")] public bool IsDayTime;
    [TabGroup("Time"), Range(0, 23)] public int TimeHour;
    [TabGroup("Time"), Range(0, 59)] public int TimeMinute;
    [TabGroup("General")] public BakedLightData LightData;
    [TabGroup("Component Affection")] public DayCycleReactor[] AffectedComponents;
    [TabGroup("Events")] public UnityEvent OnSelected;
    [TabGroup("Events")] public UnityEvent OnDeselected;

    [TabGroup("Component Affection"), Button()]
    public void AffectAllAffectableComponents()
    {
        AffectedComponents = GameObject.FindObjectsOfType<DayCycleReactor>();
    }
}

public class DayCycle : SerializedMonoBehaviour
{
    [TabGroup("Details")] public ImageFade ScreenFade;
    [TabGroup("Details")] public Player Player;
    [TabGroup("Details")] public UnityEvent OnAllSegmentsCompleted;
    [TabGroup("Day Segments")] public DayProperty[] DailyProperties;
    [TabGroup("Clocks")] public Clock[] Clocks;

    private int mSegmentIndex = 0;
    
    public bool IsDayTime { get { return DailyProperties[mSegmentIndex].IsDayTime; } }

    [TabGroup("Clocks"), Button()]
    public void FindClocks()
    {
        Clocks = FindObjectsOfType<Clock>();
    }
    
    
    [Button()]
    public void MoveToNextDay()
    {
        DailyProperties[mSegmentIndex++].OnDeselected.Invoke();
        if (mSegmentIndex > DailyProperties.Length - 1)
            ScreenFade.FadeOut(() =>
            {
                OnAllSegmentsCompleted.Invoke();
            });
        else
        {
            ScreenFade.FadeOut(() =>
            {
                ResetPlayer();
                DailyProperties[mSegmentIndex].OnSelected.Invoke();
                UpdateCycleData();
                ScreenFade.FadeIn();
            });
        }
    }
    
    private void Start()
    {
        foreach (DayProperty dailyProperty in DailyProperties)
            dailyProperty.LightData.ConstructLightMap();
        
        ScreenFade.gameObject.SetActive(true);
        ScreenFade.FadeIn(() => DailyProperties[mSegmentIndex].OnSelected.Invoke());
        UpdateCycleData();
    }

    private void ResetPlayer()
    {
        Player.ResetObject();
    }

    private void UpdateCycleData()
    {
        DayProperty prop = DailyProperties[mSegmentIndex];
         
        // Set Lightmap
        LightmapSettings.lightmaps = prop.LightData.Data;
        

        // Set spawn points
        Transform spawn = prop.SpawnPoint;
        Player.transform.position = spawn.position;
        Player.transform.rotation = spawn.rotation;
        
        foreach (DayCycleReactor affected in prop.AffectedComponents)
            affected.OnReact();

        // Set the callback for only the first clock
        if (Clocks.Length > 0) Clocks[0].DurationMethodReachedCallback += MoveToNextDay;

        // Start Clocks
        foreach (Clock clock in Clocks)
            clock.StartTime(prop.TimeHour, prop.TimeMinute, 60);
    }
}
