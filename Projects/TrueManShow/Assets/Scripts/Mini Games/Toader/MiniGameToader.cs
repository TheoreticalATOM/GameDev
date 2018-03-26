using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameToader : MiniGame
{
    public readonly static Quaternion UP_ROT = Quaternion.Euler(0.0f, 0.0f, -90.0f);
    public readonly static Quaternion RIGHT_ROT = Quaternion.Euler(0.0f, 0.0f, -180.0f);
    public readonly static Quaternion LEFT_ROT = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    public readonly static Quaternion DOWN_ROT = Quaternion.Euler(0.0f, 0.0f, -270.0f);

    public static readonly int TILE_STEP = 40;
    private const string SCORE_FORMAT = "000000";

    [Header("Health")]
    public ToaderPlayer Player;
    public int MaxLives = 9;
    [Header("Level")]
    public int SpawnPoints = 19;

    [Header("UI")]
    public Text ScoreLabel;
    public Text LivesLabel;
    public LoadingBar TimeBar;

    private int mScore;
    private int mLives;

    protected override void OnInit()
    {
        foreach (var goal in GetComponentsInChildren<ToaderGoal>())
            goal.OnGoalReached += OnUpdateScore;

        Player.OnKilled += OnDied;

        Play();
    }

    protected override void OnPlay()
    {
        mScore = 0;
        ScoreLabel.text = mScore.ToString(SCORE_FORMAT);

        mLives = MaxLives;
        LivesLabel.text = mLives.ToString();
    }

    private void OnDestroy()
    {
        foreach (var goal in GetComponentsInChildren<ToaderGoal>())
            goal.OnGoalReached -= OnUpdateScore;

        Player.OnKilled -= OnDied;
    }

    private void OnUpdateScore(int amount)
    {
        mScore += amount;
        ScoreLabel.text = mScore.ToString(SCORE_FORMAT);
        RandomRespawn();
    }

    private void OnDied()
    {
        LivesLabel.text = (--mLives).ToString();
        if (mLives <= 0)
        {
            Debug.Log("GameOver");
        }
        else
            RandomRespawn();
    }

    private void RandomRespawn()
    {
        int spawnPoint = Random.Range(0, SpawnPoints);
        Player.Respawn(spawnPoint);
    }
}
