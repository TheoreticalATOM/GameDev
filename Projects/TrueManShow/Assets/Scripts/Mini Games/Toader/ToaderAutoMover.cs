using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SDE.GamePool;
using SDE;

public class ToaderAutoMover : MonoBehaviour, IPoolable
{
    public float MaxDistance;
    public float Rate;

    public System.Action OnMoved;
    private float mDisplacementX;
    IEnumerator MoveRoutine()
    {
        mDisplacementX = 0.0f;
        while (Mathf.Abs(mDisplacementX) < MaxDistance)
        {
            Vector3 step = -transform.right * MiniGameToader.TILE_STEP;
            mDisplacementX += step.x;
            transform.Translate(step, Space.World);
            OnMoved.TryInvoke();
            yield return new WaitForSeconds(Rate);
        }
        gameObject.SetActive(false);
    }

    public void OnSpawned()
    {
        StartCoroutine(MoveRoutine());
    }
    public void OnCreated()
    {
        MiniGameToader.OnGameEnded += KillObjectPrematurely;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        MiniGameToader.OnGameEnded -= KillObjectPrematurely;
    }

    private void KillObjectPrematurely()
    {
        gameObject.SetActive(false);
    }
}
