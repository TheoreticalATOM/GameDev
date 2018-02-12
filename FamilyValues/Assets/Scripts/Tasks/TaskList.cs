using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;
using Sirenix.OdinInspector;
using SDE;
using UnityEngine.Events;
using SDE.Data;

public class TaskList : SerializedMonoBehaviour
{
    // ________________________________________________________ Inspector Content
    [Required, TabGroup("Details")] public Director Director;
    [Required, TabGroup("Details")] public Task SpecificTask;
    [TabGroup("Details")] public UnityEvent FinishedEveryTask;
    
    [MinValue(0), TabGroup("Random Tasks")] public int NumberOfRandomTasks;
    [TabGroup("Random Tasks")] public Task[] RandomTasks;

    void OnValidate()
    {
        NumberOfRandomTasks = Mathf.Min(NumberOfRandomTasks, RandomTasks.Length);
    }

    [Button]
    public void FindRandomTasks()
    {
        // needs a specific task in order to find the randoms
        if (SpecificTask == null)
        {
            Debug.LogError(name + " does not contain a specific task");
            return;
        }

        Task[] foundTasks = GameObject.FindObjectsOfType<Task>();

        // reduce the task count by one to allow for ignoring the specific task (because the specific task has to be set manually)
        int taskCount = foundTasks.Length - 1;

        RandomTasks = new Task[taskCount];
        for (int i = 0, j = 0; i < foundTasks.Length; i++)
        {
            // if the found task is equal to the specific task, then skip it
            if (foundTasks[i] == SpecificTask)
                continue;
            RandomTasks[j++] = foundTasks[i];
        }
    }

    // ________________________________________________________ Datamembers	
    private Queue<Task> mTaskList;
    private System.Random mRandomizer;

#if UNITY_EDITOR
    public class EditorTaskName
    {
        public bool Complete;
        public string Name;
    }
    [HideInInspector] public Dictionary<Task, EditorTaskName> TaskListNames = new Dictionary<Task, EditorTaskName>();
#endif

    // ________________________________________________________ Getters
    #region Getters
    /// <summary>
    /// Returns the currently active task. This value changes when the current active task is completed
    /// </summary>
    public Task ActiveTask { get { return mTaskList.Peek(); } }

    /// <summary>
    /// Returns the number of tasks that are left on the task list
    /// </summary>
    public int TasksLeft { get { return mTaskList.Count; } }
    #endregion

    // ________________________________________________________ Controls
    #region Controls
    /// <summary>
    /// Will generate a random list of tasks. Amongst these tasks is the specific one as well. 
    /// The placement of the specific one is equally as random as all the random tasks added  
    /// </summary>
    public void PopulateTaskList()
    {
        // make sure that there is a designated specific task
        Assert.IsNotNull(SpecificTask, name + "(Task List): is missing a specific task");

        // clear the current list of tasks
        mTaskList.Clear();

#if UNITY_EDITOR
        TaskListNames.Clear();
#endif

        // if there are no random tasks, then just simply add the specific task
        if (NumberOfRandomTasks < 1)
        {
            AddTaskToList(SpecificTask);
            return;
        }

        // shuffle the task list, to give it a different order everytime
        RandomTasks.Shuffle(mRandomizer);

        // loop through the number of tasks desired + add the specific one
        bool hasAddedSpecific = false;
        for (int i = 0; i < NumberOfRandomTasks;)
        {
            // only add the specific item once
            if (!hasAddedSpecific)
            {
                // the chance will increase as there is less and less random values left to pick from
                float random = Random.Range(0.0f, 1.0f);
                float chance = (i + 1.0f) / NumberOfRandomTasks;

                hasAddedSpecific = chance >= random;
                // if found, then add it and skip the random addition
                if (hasAddedSpecific)
                {
                    AddTaskToList(SpecificTask);
                    continue;
                }
            }

            // add a random task to the task queue
            Task randTask = RandomTasks[i++];
            Assert.IsNotNull(randTask, name + "(Task List): has an empty random task");
            AddTaskToList(randTask);
        }
    }

    /// <summary>
    /// Will increase the current suspicion. Unlike RecordSuccess It will not tag the task as completed unless the optional "consideredDone" boolean is set to true
    /// </summary>
    public void RecordFailure(float suspicionAmount, bool consideredDone = false)
    {
        Director.IncreaseSuspicion(suspicionAmount);
        if (consideredDone)
            UpdateTheTaskList();
    }

    /// <summary>
    /// Will decrease the current suspicion. 
    /// Additionally, it will update the task list, 
    /// removing the currently active task and setting it to be the proceeding one on the list
    /// </summary>
    public void RecordSuccess(float suspicionAmount)
    {
        Director.DecreaseSuspicion(suspicionAmount);
        UpdateTheTaskList();
    }
    #endregion

    // ________________________________________________________ Methods
    private void Awake()
    {
        mTaskList = new Queue<Task>();
        mRandomizer = new System.Random();

        // Initialize every task
        foreach (Task task in RandomTasks)
            task.OnTaskInit();
        SpecificTask.OnTaskInit();
    }

    private void AddTaskToList(Task task)
    {
        /* gives the task a record of the TaskList (this), 
		so that the task can keep the TaskList updated on when a task is completed and/or failed */
        task.AddTask(this);

        // add it to the queue of required tasks to be done
        mTaskList.Enqueue(task);

#if UNITY_EDITOR
        TaskListNames.Add(task, new EditorTaskName()
        {
            Complete = false,
            Name = task.name
        });
#endif
    }

    private void UpdateTheTaskList()
    {
        /* will reomve the currenlty active task and check if there are any left.
		If none, then fire off an event */

        Task task = mTaskList.Dequeue();
#if UNITY_EDITOR
        if (TaskListNames.ContainsKey(task))
            TaskListNames[task].Complete = true;
#endif

        if (TasksLeft < 1)
            FinishedEveryTask.Invoke();
    }
}
