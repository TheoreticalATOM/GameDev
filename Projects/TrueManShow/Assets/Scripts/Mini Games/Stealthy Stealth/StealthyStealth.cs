using UnityEngine;

using SDE;

public class StealthyStealth : MiniGame
{
    public StealthyStealthPlayer Player;
    public StealthyStealthLevel[] Levels;
    public MiniGame.GameView PlayingView;

    public GameObject MenuScreen;
    public GameObject GameOverScreen;

    private int mCurrentLevel = 0;
    private System.Action mMenuAction;

    private bool mIsLocked = false;

    public void NextLevel()
    {
        Levels[mCurrentLevel].End();
        if (++mCurrentLevel > Levels.Length - 1)
        {
            GameOverScreen.SetActive(true);
            Camera.SetTarget(View);

            Player.gameObject.SetActive(false);
            mMenuAction = OnPlay;
            enabled = !mIsLocked;
            CompleteGame();
        }
        else
            Levels[mCurrentLevel].Play(Player);

        Player.LockPlayer(mIsLocked);
    }

    protected override void OnInit()
    {
        Player.OnDied += OnPlayerDied;
    }

    protected override void OnPlay()
    {
        // disable all the levels
        Levels.Set(level => level.End());

        // renable the first one
        mCurrentLevel = 0;

        GameOverScreen.SetActive(false);
        MenuScreen.SetActive(true);

        Player.gameObject.SetActive(false);

        enabled = !mIsLocked;
        mMenuAction = StartGameFromMainMenu;
    }

    public override void LockControls(bool state)
    {
        Player.LockPlayer(state);
        mIsLocked = state;

        if (GameOverScreen.activeSelf || MenuScreen.activeSelf)
            enabled = !state;
    }

    private void StartGameFromMainMenu()
    {
        MenuScreen.SetActive(false);
        Player.gameObject.SetActive(true);
        Levels[mCurrentLevel].Play(Player);
        Camera.SetTarget(PlayingView);
    }

    protected override void OnUpdate()
    {
        if (Input.GetButtonUp("MiniGameStart"))
        {
            enabled = false;
            mMenuAction();
        }
    }

    private void OnDestroy()
    {
        Player.OnDied -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        Debug.Log("Died");
        Camera.SetTarget(View);
        OnPlay();
    }
}