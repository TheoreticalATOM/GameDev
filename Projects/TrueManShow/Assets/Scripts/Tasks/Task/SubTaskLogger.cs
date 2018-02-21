using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTaskLogger : Task
{
    public int NumberOfSubTasks;
    public int ActivatedSubTasks { get; private set; }

    protected override void OnTaskStarted()
    {
        ActivatedSubTasks = 0;
    }

    protected override void OnTaskReset()
    {
        ActivatedSubTasks = 0;
    }

    public void ActivateSubTask()
    {
		// if no sub tasks are actived, then start the task
        if (ActivatedSubTasks < 1)
            StartTask();

        if (++ActivatedSubTasks >= NumberOfSubTasks)
            CompleteTask();
    }
}
