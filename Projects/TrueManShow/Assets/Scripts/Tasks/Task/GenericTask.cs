using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTask : Task 
{
	public int NumberOfSubTasks;
	public int ActivatedSubTasks {get;private set;}

	public void ActivateSubTask()
	{
		if(ActivatedSubTasks < 1)
			StartTask();

		if(++ActivatedSubTasks >= NumberOfSubTasks)
			CompleteTask();
	}

    protected override void OnTaskReset()
    {
		ActivatedSubTasks = 0;
    }

    protected override void OnTaskStarted()
	{
		ActivatedSubTasks = 0;
    }
}
