using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBake : SubTaskLogger
{
    public bool IsOvenOn {get;set;}


    public void ToggleOven()
    {
		IsOvenOn = !IsOvenOn;
    }
}
