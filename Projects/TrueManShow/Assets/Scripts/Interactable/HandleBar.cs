using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleBar : MonoBehaviour
{
    public float thrust;

    private float fillAmount;

    public Image content;

    private float MaxValue = 750;

    public void UpdateBar()
    {
        if (thrust + 3 <= MaxValue)
        {
            thrust += 3;
            fillAmount = Map( thrust, 0, MaxValue, 0, 1);
            content.fillAmount = fillAmount;

        }
    }

    public void ResetThrust()
    {
        thrust = 0;
        fillAmount = 0;
        content.fillAmount = fillAmount;
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
