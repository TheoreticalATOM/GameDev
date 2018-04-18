using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SDE.Data;

public class MiniGameToader : MiniGame
{
    // ____________________________________________________
    // @ Static Declaration
    public readonly static Quaternion UP_ROT = Quaternion.Euler(0.0f, 0.0f, -90.0f);
    public readonly static Quaternion RIGHT_ROT = Quaternion.Euler(0.0f, 0.0f, -180.0f);
    public readonly static Quaternion LEFT_ROT = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    public readonly static Quaternion DOWN_ROT = Quaternion.Euler(0.0f, 0.0f, -270.0f);

    public static readonly int TILE_STEP = 40;
    private const string SCORE_FORMAT = "000000";

    // ____________________________________________________
    // @ Events
    public static event System.Action OnGameEnded;

    // ____________________________________________________
    // @ Inspector
    [Header("Health")]
    public ToaderPlayer Player;
    public int MaxLives = 9;
    [Header("Level")]
    public int SpawnPoints = 19;
    public int LevelDurationInSeconds = 60;

    [Header("UI")]
    public Text ScoreLabel;
    public Text LivesLabel;
    public SDE.UI.Progress TimerBar;

    [Space(2.0f)]
    public GameObject GameOverView;
    public Text GameOverScoreLabel;
    public AudioClip GoalReachedClip;
    public AudioClip PlayerDiedClip;

    // ____________________________________________________
    // @ Data
    private int mScore;
    private int mLives;

    private ToaderGoal[] mGoals;
    private int mGoalsReachedCount;

    // ____________________________________________________
    // @ Controllers
    protected override void OnInit()
    {
        mGoals = GetComponentsInChildren<ToaderGoal>();
        mGoalsReachedCount = 0;

        foreach (var goal in mGoals)
            goal.OnGoalReached += OnGoalReached;
        Player.OnKilled += OnDied;
        Player.OnPointsAdded += UpdateScore;
    }

    protected override void OnPlay()
    {
        mScore = 0;
        ScoreLabel.text = mScore.ToString(SCORE_FORMAT);

        mLives = MaxLives;
        LivesLabel.text = mLives.ToString();

        ShowGameOver(false);

        // Reset the goals
        foreach (ToaderGoal goal in mGoals)
            goal.ResetGoal();
        mGoalsReachedCount = 0;

        RandomRespawn();
    }

    protected override void OnEnded()
    {
        StopAllCoroutines();
        OnGameEnded.Invoke();
    }
    protected override void OnUpdate()
    {
        if (Input.GetButtonUp("MiniGameStart"))
        {
            ShowGameOver(false);
            OnPlay();
        }
    }

    public override void LockControls(bool state)
    {
        Player.enabled = !state;
    }

    // ____________________________________________________
    // @ Methods
    private void OnDestroy()
    {
        foreach (var goal in mGoals)
            goal.OnGoalReached -= OnGoalReached;

        Player.OnKilled -= OnDied;
        Player.OnPointsAdded -= UpdateScore;
    }

    private void UpdateScore(int amount)
    {
        mScore += amount;
        ScoreLabel.text = mScore.ToString(SCORE_FORMAT);
    }

    private void RandomRespawn()
    {
        StopAllCoroutines();
        StartCoroutine(AsyncTimeRoutine());

        int spawnPoint = Random.Range(0, SpawnPoints);
        Player.Respawn(spawnPoint);
    }

    private IEnumerator AsyncTimeRoutine()
    {
        int timeCounter = 0;
        do
        {
            TimerBar.UpdateProgress(timeCounter++, LevelDurationInSeconds);
            yield return new WaitForSeconds(1.0f);
        } while (timeCounter < LevelDurationInSeconds);
        Player.Kill();
    }

    // ____________________________________________________
    // @ Events
    private void OnGoalReached(int amount)
    {
        AudioPool pool = null;
        if (AudioPool.TryGetFirst<AudioPool>(ref pool))
            pool.PlayClip(GoalReachedClip);

        UpdateScore(amount);
        if (++mGoalsReachedCount >= mGoals.Length)
        {
            ShowGameOver(true);
            CompleteGame();
        }
        else
            RandomRespawn();
    }
    private void OnDied()
    {
        AudioPool pool = null;
        if (AudioPool.TryGetFirst<AudioPool>(ref pool))
            pool.PlayClip(PlayerDiedClip);

        LivesLabel.text = (--mLives).ToString();
        if (mLives <= 0)
            ShowGameOver(true);
        else
            RandomRespawn();
    }

    private void ShowGameOver(bool state)
    {
        if (state)
        {
            GameOverScoreLabel.text = mScore.ToString(SCORE_FORMAT);
            OnGameEnded();
        }
        GameOverView.SetActive(state);
        enabled = state;
    }
}
