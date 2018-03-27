using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConsole : MonoBehaviour
{
    private MiniGame mCurrentGame;
	
    public void SetCurrentGame(MiniGame game)
    {
        mCurrentGame = game;
    }

    public void StartGame()
    {
        if (mCurrentGame)
            mCurrentGame.Play();
    }

    public void PauseGame(bool state)
    {
        if (mCurrentGame)
            mCurrentGame.Pause(state);
    }

    public void EndGame()
    {
        if (mCurrentGame)
            mCurrentGame.End();
        mCurrentGame = null;
    }
}
