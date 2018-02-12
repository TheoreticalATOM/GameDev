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
    public UnityEvent OnSelected;
    public UnityEvent OnDeselected;
}

public class DayCycle : SerializedMonoBehaviour
{
    [TabGroup("Details")] public Light Sun;
    [TabGroup("Details")] public UnityEvent OnAllSegmentsCompleted;
    [TabGroup("Day Segments")] public DayProperty[] DailyProperties;

    private int mSegmentIndex = 0;

    public void MoveToNextDay()
    {
        DailyProperties[mSegmentIndex++].OnDeselected.Invoke();
        if(mSegmentIndex > DailyProperties.Length - 1)
            OnAllSegmentsCompleted.Invoke();
        else
        {
            DailyProperties[mSegmentIndex].OnSelected.Invoke();
            UpdateSun();
        }
    }

    private void Start()
    {
        DailyProperties[mSegmentIndex].OnSelected.Invoke();
        UpdateSun();
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
