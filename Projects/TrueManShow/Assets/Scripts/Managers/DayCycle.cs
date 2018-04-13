using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DayProperty
{
    [TabGroup("General")] public Color Colour;
    [TabGroup("General")] public bool IsDayTime;
    [TabGroup("General")] public Texture2D[] LightMaps;
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


    private int mSegmentIndex = 0;
    
    public bool IsDayTime { get { return DailyProperties[mSegmentIndex].IsDayTime; } }
    
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

        LightmapData[] lightmapDatas = LightmapSettings.lightmaps;
        for (int i = 0; i < prop.LightMaps.Length; i++)
            lightmapDatas[i].lightmapColor = prop.LightMaps[i];
        LightmapSettings.lightmaps = lightmapDatas;
        
        foreach (DayCycleReactor affected in prop.AffectedComponents) 
            affected.OnReact();
    }
}
