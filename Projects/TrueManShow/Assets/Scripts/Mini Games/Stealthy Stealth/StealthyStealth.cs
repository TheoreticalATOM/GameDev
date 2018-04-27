using UnityEngine;

using SDE;
using SDE.Data;
using UnityEngine.Events;

public class StealthyStealth : MiniGame
{
    public StealthyStealthPlayer Player;
    public StealthyStealthLevel[] Levels;
    public MiniGame.GameView PlayingView;

    public GameObject MenuScreen;
    public GameObject GameOverScreen;

    public RuntimeSet AudioPoolSet;
    public AudioClip OnDeathClip;
    
    [Header("Reactions")] 
    public int HostRemarkDeathCount;
    public DialogNodeBase DialogMattDiesXAmount;
    public DialogNodeBase[] DialogMattDies;
    public DialogNodeBase DialogFinishedLevel;
    
    [Header("Special Events")]
    public UnityEvent OnGameStartedOnce;
    
    public int DeathCount { get; private set; }
    
    private int mCurrentLevel = 0;
    private System.Action mMenuAction;
    private bool mHasBeenStartedOnce;
    
    public bool HasFinishedOnce { get; set; }
    
    private bool mIsLocked = false;

    public void NextLevel()
    {
        Levels[mCurrentLevel].End();
        if (++mCurrentLevel >= Levels.Length)
        {
            GameOverScreen.SetActive(true);
            Camera.SetTarget(View);

            Player.gameObject.SetActive(false);
            mMenuAction = OnPlay;
            enabled = !mIsLocked;
            CompleteGame();
        }
        else
        {
            Levels[mCurrentLevel].Play(Player);
            DialogFinishedLevel.Play();
        }
        

        Player.LockPlayer(mIsLocked);
    }

    protected override void OnInit()
    {
        Player.OnDied += OnPlayerDied;
        mHasBeenStartedOnce = false;
        HasFinishedOnce = false;
    }

    protected override void OnPlay()
    {
        // disable all the levels
        Levels.SetAll(level => level.End());

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
        if (!HasFinishedOnce && Input.GetButtonUp("MiniGameStart"))
        {
            enabled = false;
            mMenuAction();

            if (!mHasBeenStartedOnce)
            {
                OnGameStartedOnce.Invoke();
                mHasBeenStartedOnce = true;
            }
        }
    }

    private void OnDestroy()
    {
        Player.OnDied -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        AudioPoolSet.GetFirst<AudioPool>().PlayClip(OnDeathClip);
        
        if(++DeathCount == HostRemarkDeathCount)
            DialogMattDiesXAmount.Play();
        else
            DialogMattDies.RandomValue().Play();
        
        Camera.SetTarget(View);
        OnPlay();
    }
}