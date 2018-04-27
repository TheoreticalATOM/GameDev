using UnityEngine;
using UnityEngine.Events;

using SDE.Data;

public class GameConsole : MonoBehaviour
{
    public TCMaterial TVMaterialToggler;
    public RuntimeSet Pool;
    public bool GameIsRunning { get { return mCurrentGame != null; } }

    private MiniGame mCurrentGame;
    private MiniGame mPrevGame;

    public void SetCurrentGame(MiniGame game)
    {
        mCurrentGame = game;
    }

    public void StartGame()
    {
        if (mCurrentGame && mCurrentGame != mPrevGame)
        {
            AudioPool pool = Pool.GetFirst<AudioPool>();
            pool.IsDefaultAllowed = false;
            pool.StopDefault();
            if(!TVMaterialToggler.IsToggleA)
                pool.DeafenSong();
            mCurrentGame.Play();
        }

        mPrevGame = mCurrentGame;
    }

    public void LockGame(bool state)
    {
        if (mCurrentGame)
            mCurrentGame.LockControls(state);
    }

    public void EndGame()
    {
        if (mCurrentGame)
        {
            mCurrentGame.End();

            AudioPool pool = Pool.GetFirst<AudioPool>();
            pool.IsDefaultAllowed = true;
            pool.StopSong();

            if(TVMaterialToggler.IsToggleA)
                pool.PlayDefaultAndStopAllClips();
        }
        mCurrentGame = null;
        mPrevGame = null;
    }
}
