using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDefeat : MonoBehaviour
{
    private Transform playerBase;

    private BaseHealth[] mHealths;
    private int mAliveCount;

    // Use this for initialization
    void Awake()
    {
        playerBase = GetComponent<Transform>();

        mHealths = GetComponentsInChildren<BaseHealth>();
        foreach (BaseHealth health in mHealths)
        {
            health.OnNoHealth += OnUpdateDeathCount;
        }

        mAliveCount = mHealths.Length;
    }

    public void ResetBases()
    {
        mAliveCount = mHealths.Length;
        foreach (BaseHealth health in mHealths)
            health.ResetHealth();
    }


    void OnUpdateDeathCount()
    {
        if (--mAliveCount <= 0)
            MiniGameSpaceInvaders.EndCurrentGame();
    }

    private void OnDestroy()
    {
        foreach (BaseHealth health in mHealths)
        {
            health.OnNoHealth -= OnUpdateDeathCount;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (playerBase.childCount == 0)
            SIGameOver.isPlayerDead = true;
    }
}