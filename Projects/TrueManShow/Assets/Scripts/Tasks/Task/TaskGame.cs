using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGame : SubTaskLogger
{
	[Header("Screen")]
    public MeshRenderer Screen;
    public Material ScreenOff;
    public Material ScreenOn;

	[Header("Console")]
	public ItemInteract Console;

	[Header("Game")]
    public StealthyStealth Game;

    private bool mTVHasBeenTurnedOnOnce;
	private bool mTVOn;

    public override void OnTaskInit()
    {
        ResetToDefault();
    }
    protected override void OnTaskStarted()
    {
        base.OnTaskStarted();

        // on start check if the TV is already on. If it is, then consider the sub task done
        EvaluateTVAsSubTask();
    }

    protected override void OnTaskReset()
    {
        base.OnTaskReset();
        ResetToDefault();
    }

    public void PlayGame()
    {
        Game.CanControl = true;
    }
    public void PauseGame()
    {
        Game.CanControl = false;
    }

    public void StartGame()
    {
        // when a game has been started, consider that a sub task completed
        ActivateSubTask();
        Game.StartGame();
    }

    public void ToggleTV()
    {
        // Toggle TV ON/OFF
		mTVOn = !mTVOn;
		if(mTVOn) Screen.material = ScreenOn;
		else Screen.material = ScreenOff;

        // evaluate if the tv has been turned on (and only consider it once)
        EvaluateTVAsSubTask();
    }

    private void ResetToDefault()
    {
        Screen.material = ScreenOff;
		mTVOn = false;
        mTVHasBeenTurnedOnOnce = false;
		Console.CanBeInteractedWith = false;
    }

    private void EvaluateTVAsSubTask()
    {
        // check if the tv is on, and if it has already been turned on before
        if (Screen.material == ScreenOn && !mTVHasBeenTurnedOnOnce)
        {
            mTVHasBeenTurnedOnOnce = true;
            ActivateSubTask();
        }
    }
}
