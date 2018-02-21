using UnityEngine;
using System.Collections;

public class Stat
{
   // public HandleBar bar;

    private float maxVal;

    private float currentVal;

    public float MaxVal
    {

        get
        {
            return maxVal;
        }

        set
        {
            this.maxVal = value;
            //bar.MaxValue = maxVal;
        }
    }

    private float CurrentVal
    {
        get
        {
            return currentVal;
        }
        set
        {
            this.currentVal = value;
            //bar.Value = currentVal;
        }
    }

    
}
