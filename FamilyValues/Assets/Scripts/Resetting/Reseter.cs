using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Reseter : SerializedMonoBehaviour
{
    public Resetable[] ResetList;

    [Button]
    public void FindAllResetablesInTheScene()
    {
        ResetList = FindObjectsOfType<Resetable>();
    }

    public void ResetScene()
    {
        foreach (Resetable resetObj in ResetList)
            resetObj.ResetObject();
    }

    public void UpdateResetObjects()
    {
        foreach (Resetable resetObj in ResetList)
            resetObj.UpdateResetObject();
    }
}
