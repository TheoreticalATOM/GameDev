using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DayProperty
{
    [Header("Segment")]
    public Color LightColour;
    public float SunOrientation;
    public bool IsDayTime;
    public UnityEvent OnSelected;
    public UnityEvent OnDeselected;
}

public class DayCycle : SerializedMonoBehaviour
{
    [TabGroup("Details")] public Light Sun;
    [TabGroup("Details")] public ImageFade ScreenFade;
    [TabGroup("Details")] public UnityEvent OnAllSegmentsCompleted;
    [TabGroup("Day Segments")] public DayProperty[] DailyProperties;

    [Header("Player Spawning")]
    public Player Player;

    private int mSegmentIndex = 0;

    public bool IsDayTime { get { return DailyProperties[mSegmentIndex].IsDayTime; } }

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
                UpdateSun();
                ScreenFade.FadeIn();
            });
        }
    }

    private void Start()
    {
        ScreenFade.gameObject.SetActive(true);
        ScreenFade.FadeIn(() => DailyProperties[mSegmentIndex].OnSelected.Invoke());
        UpdateSun();
    }

    private void ResetPlayer()
    {
        Player.ResetObject();
    }

    private void UpdateSun()
    {
        DayProperty prop = DailyProperties[mSegmentIndex];

        // Update the Orientation
        Vector3 rot = Sun.transform.localRotation.eulerAngles;
        rot.x = prop.SunOrientation;
        Sun.transform.rotation = Quaternion.Euler(rot);

        // Update the light colour
        Sun.color = prop.LightColour;
    }
}
