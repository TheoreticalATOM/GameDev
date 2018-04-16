using UnityEngine;
using UnityEngine.UI;
using SDE;

public class MiniGameSpaceInvaders : MiniGame
{
    public Player_Movement Player;
    public EnemyController Enemies;
    public BaseDefeat BaseHealths;
    public GameObject GameOverScreen;
    public Text ScoreLabel; 
    
    public static event System.Action OnGameOver;
    public static event System.Action<int> OnScoreAdded;

    private bool mIsLocked;
    private int mScrore;
    
    protected override void OnInit()
    {
        OnGameOver += PlayerGameOver;
        EnemyController.OnNoEnemiesLeft += PlayerGameOver;
        OnScoreAdded += OnUpdateScore;
    }

    void OnUpdateScore(int amount)
    {
        mScrore += amount;
        ScoreLabel.text = "Score: " + mScrore.ToString("00");
    }

    protected override void OnPlay()
    {
        GameOverScreen.SetActive(false);
        Enemies.Respawn();
        BaseHealths.ResetBases();
        Player.ResetPlayer();
        SIGameOver.isPlayerDead = false;
        
        PlayerScore.playerScore = 0;
        enabled = false;
        
        Enemies.StartMoveEnemy();
    }

    void PlayerGameOver()
    {
        GameOverScreen.SetActive(true);
        enabled = !mIsLocked;
        
        Enemies.StopMoveEnemy();
        
    }

    public static void AddScore(int amount)
    {
        OnScoreAdded.TryInvoke(amount);
    }
    

    private void OnDestroy()
    {
        OnGameOver -= PlayerGameOver;
        EnemyController.OnNoEnemiesLeft -= PlayerGameOver;
        OnScoreAdded -= OnUpdateScore;
    }

    public static void EndCurrentGame()
    {
        OnGameOver.TryInvoke();
    }

    protected override void OnUpdate()
    {
        if (Input.GetButton("MiniGameStart"))
            OnPlay();
    }
    
    public override void LockControls(bool state)
    {
        Player.enabled = !state;
        mIsLocked = state;
    }
    
}
