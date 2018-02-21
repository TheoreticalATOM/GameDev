using SDE.Data;
using UnityEngine;

public class RuntimeLoadingBar : MonoBehaviour, IRuntime
{
    public RuntimeSet LoadingBarSet;
	public LoadingBar LoadingBar;

    private void OnEnable()
    {
        LoadingBarSet.Add(this);
    }
    private void OnDisable()
    {
        LoadingBarSet.Remove(this);
    }
}
