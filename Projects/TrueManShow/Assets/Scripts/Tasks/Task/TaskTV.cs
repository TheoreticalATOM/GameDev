using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTV : Task
{
    public MeshRenderer Renderer;
    public Material TVOffMaterial;
    public Material[] ChannelMaterial;

    private int mIndex;

    public override void OnTaskInit()
    {
        SetTVDefault();
    }

    protected override void OnTaskAdded()
    {

    }

    protected override void OnTaskReset()
    {
        SetTVDefault();
    }

    protected override void OnTaskStarted()
    {
		Renderer.material = ChannelMaterial[mIndex];
    }

    protected override void OnTaskUpdate()
    {
		if(Input.GetButtonUp("Jump"))
		{
			if(++mIndex > ChannelMaterial.Length-1)
				CompleteTask();
			else
				Renderer.material = ChannelMaterial[mIndex];
		}
    }

    private void SetTVDefault()
    {
        Renderer.material = TVOffMaterial;
        mIndex = 0;
    }
}
