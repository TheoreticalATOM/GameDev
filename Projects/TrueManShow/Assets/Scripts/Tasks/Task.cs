using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class LimitedSerializedMonoBehaviour : Resetable
{
    protected virtual void Start() { }
    protected virtual void Update() { }
}

public abstract class Task : LimitedSerializedMonoBehaviour
{
    // ________________________________________________________ Inspector Content
    public string TaskName;
    [TextArea(3, 15), TabGroup("Details")] public string Description;
    [MinMaxSlider(0.0f, 200.0f, true), TabGroup("Details")] public Vector2 TimeToFinishInSeconds;
    [MinValue(0), TabGroup("Details")] public float SuspicionIncrease = 5.0f;
    [MinValue(0), TabGroup("Details")] public float SuspicionDecrease = 5.0f;

	[TabGroup("Events")] public UnityEvent WrongTask;
	[TabGroup("Events")] public UnityEvent FinishedItTooQuickly;
	[TabGroup("Events")] public UnityEvent FinishedItTooLate;
	[TabGroup("Events")] public UnityEvent FinishedTask;

    // ________________________________________________________ Datamembers	
    private float mStartTime;
    private TaskList mOwner;
    private System.Action mTimerCheckAction;

    // ________________________________________________________ Getters	
    #region Getters
	/// <summary>
	/// Returns the Minimum time to spend on the task
	/// </summary>
    public float MinimumTime { get { return TimeToFinishInSeconds.x; } }
    
	/// <summary>
    /// Returns the Maxmimum time to spend on the task
    /// </summary>
	public float MaximumTime { get { return TimeToFinishInSeconds.y; } }
    
	/// <summary>
	/// Returns the Current time since the task was started
	/// </summary>
	public float CurrentTime { get; private set; }

	/// <summary>
	/// Returns the current time in percentage (between 0 and 1). The scale being between 0 and MaximumTime
	/// </summary>
    public float CurrentInPercentage { get { return CurrentTime / MaximumTime; } }

    /// <summary>
    /// Returns the mimimum time in percentage (between 0 and 1). The scale being between 0 and MaximumTime
    /// </summary>
    /// <returns></returns>
	public float MinimumInPercentage { get { return MinimumTime / MaximumTime; } }
    #endregion

    // ________________________________________________________ Abstract Methods
    #region Abstract Methods
    
	/// <summary>
	/// Consider it as Unity's Start Method.
	/// </summary>
	public virtual void OnTaskInit() {}

	/// <summary>
	/// Gets called when the task has been added to the task list
	/// </summary>
    protected virtual void OnTaskAdded() {}

	/// <summary>
	/// Gets called when the task is started. When the task is started, 
	/// the OnTaskUpdate method is enabled, and the completion timer is started
	/// </summary>
    protected abstract void OnTaskStarted();

	/// <summary>
	/// Gets called every frame after the task has been started. 
	/// Consider this Unity's Update.
	/// This method will stop being called when a task is completed
	/// </summary>
    protected virtual void OnTaskUpdate() {}

	/// <summary>
	/// Gets called everytime the IReset method is called (is used for when the scene is being reset)
	/// </summary>
    protected abstract void OnTaskReset();
    #endregion

    // ________________________________________________________ Controls
    #region Task Controls
	/// <summary>
	/// This stores a reference to the tasklist. Just disregard this method, 
	/// as it is really just here for the TaskList to communicate with the Task  
	/// </summary>
    public void AddTask(TaskList owner)
    {
        mOwner = owner;
        OnTaskAdded();
    }

	/// <summary>
	/// This method will Start the task.When the task is started, 
	/// the OnTaskUpdate method is enabled, and the completion timer is started.
	/// It is from here on out, that the CurrentTime will be tracked.
	/// If the task is not the currently active one, it will increase the suspicion as well as ignore the request to start.
	/// </summary>
    public void StartTask()
    {	
		// if there is no owner, then do nothing
        if (!mOwner)
            return;

		// if the active task is not this current task, then considered it a failure, and leave
        if (mOwner.ActiveTask != this)
        {
            mOwner.RecordFailure(SuspicionIncrease);
            Debug.Log(name + " is the incorrect task to do at this moment!");
			WrongTask.Invoke();
            return;
        }

		/* using a timer check action instead of a boolean, to avoid unessesary coonditional checks */
        mTimerCheckAction = () =>
		{
			/* if the current time exceeds the maximum waiting time, then consider it a failure, 
			and reset the timerCheckAction  */
			if (CurrentTime > MaximumTime)
			{
				FailTaskNotTerminative();
				FinishedItTooLate.Invoke();
				/* this is set to nothing, so that in the update, no longer will the timer update, 
				as there is no point after already exceeding the maximumtime */
				mTimerCheckAction = () => { };
			}
			// update the current time every frame from the start-time's reference point
			CurrentTime = Time.time - mStartTime;

            mOwner.AutoQueue.TimeBar.UpdateBar(CurrentTime, MaximumTime);
		};

		// turn on the update method, and track the start time
        enabled = true;
        mStartTime = Time.time;
        OnTaskStarted();
    }


	/// <summary>
	/// Completing a task will update the task will decrease suspicion and update the tasklist (if the task is on the task list),
	/// making this current task no longer the active one. Additionally, it will terminate this current task,
	/// disabling the OnTaskUpdate method
	/// </summary>
    public void CompleteTask()
    {
		// only consider it all if there is a task list present
        if (mOwner)
        {
			/* If the task is completed before the mimimum time, then complete the task, 
			but still consider it a failure */
            if (CurrentTime < MinimumTime)
            {
				// passing true, telling the list that it is considered completed
                mOwner.RecordFailure(SuspicionIncrease, true);
				FinishedItTooQuickly.Invoke();
                Debug.Log(name + " : task failed because it was done too quickly!");
            }
			/* else, consider the task succesfully completed */
            else
            {
                mOwner.RecordSuccess(SuspicionDecrease);
				FinishedTask.Invoke();
                Debug.Log(name + " : task completed!");
            }
        }

		/* reseting everything will stop the updates from running, and clear any reference to the task list */
        ResetTaskTracking();
        mOwner = null;
    }

	/// <summary>
	/// Will increase the suspicion, but will not stop the task
	/// </summary>
    public void FailTaskNotTerminative()
    {
		// only consider updating the suspicion if there is a task list present
        if (mOwner) mOwner.RecordFailure(SuspicionIncrease);
        Debug.Log(name + " : task failed! (still need to finish it!)");
    }
    #endregion

    // ________________________________________________________ Methods
    protected sealed override void Start()
    {
		/* start by resting all the tracking, 
		preventing the task from starting */
        ResetTaskTracking();
        OnTaskInit();
    }

    protected sealed override void Update()
    {
		/* Will repeatedly check the current time of the task. If the task surpasses the Maximum time,
		the mTimerCheckAction will be changed to a completely empty method call, removing any unecessary conditional checking */
        mTimerCheckAction();
        
		OnTaskUpdate();
    }

    private void ResetTaskTracking()
    {
        enabled = false;
        mStartTime = 0.0f;
        mTimerCheckAction = () => { };
    }

    public override void ResetObject()
    {
        ResetTaskTracking();
        mOwner = null;
        OnTaskReset();
    }
}
