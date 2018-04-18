using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPathFollower : MonoBehaviour
{
    delegate IEnumerator DelRoutineAction();
    
    public float CloseEnoughDistanceMin;
    public float CloseEnoughDistanceMax;
    public float TransitionSpeedMin;
    public float TransitionSpeedMax;
    public Transform Agent;
    
    private Vector3[] mPoints;
    private int mPointIndex;
    private Vector3 mVelocity;

    private float mTransitionSpeed;
    private float mCloseEnoughDistance;

    private Queue<DelRoutineAction> mQueuedChanges;
    private Vector3 mSpecificTarget;
    private int mRoutineCount;
    
    public void FollowRandomPath()
    {
        mRoutineCount++;
        mQueuedChanges.Enqueue(RandomWalkingRoutine);
        TryRunCoroutine();
    }

    public void GoToSpecificTarget(Transform target)
    {
        mRoutineCount++;
        mSpecificTarget = target.position;
        mQueuedChanges.Enqueue(DirectWalkingRoutine);
        TryRunCoroutine();
    }
    
    private void Awake()
    {
        mQueuedChanges = new Queue<DelRoutineAction>();
        mPoints = new Vector3[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
            mPoints[i++] = child.position;

    }

    private void TryRunCoroutine()
    {
        if (mQueuedChanges.Count > 0)
            StartCoroutine(mQueuedChanges.Dequeue()());
    }
    

    private void UpdateAllRandomness()
    {
        UpdateRandomPointIndex();
        UpdateRandomTransition();
    }

    private void UpdateRandomTransition()
    {
        mTransitionSpeed = Random.Range(TransitionSpeedMin, TransitionSpeedMax);
        mCloseEnoughDistance = Random.Range(CloseEnoughDistanceMin, CloseEnoughDistanceMax);
    }
    
    private void UpdateRandomPointIndex()
    {
        mPointIndex = Random.Range(0, mPoints.Length);
    }
    
    private IEnumerator RandomWalkingRoutine()
    {
        UpdateAllRandomness();
        while (mRoutineCount < 2)
        {
            Vector3 target = mPoints[mPointIndex];
            while (!UpdatePath(target) && mRoutineCount < 2) yield return null;
            UpdateAllRandomness();
        }
        mRoutineCount--;
        TryRunCoroutine();
    }

    private IEnumerator DirectWalkingRoutine()
    {
        UpdateRandomTransition();
        mCloseEnoughDistance = 0.2f;
        while (!UpdatePath(mSpecificTarget)) {yield return null;}

        mRoutineCount--;
        TryRunCoroutine();
    }

    private bool UpdatePath(Vector3 targetPos)
    {
        Vector3 agentPos = Agent.position;
        targetPos.y = agentPos.y;
        
        agentPos = Vector3.SmoothDamp(agentPos, targetPos, ref mVelocity, mTransitionSpeed * Time.deltaTime);
        Agent.position = agentPos;

        return (targetPos - agentPos).sqrMagnitude < mCloseEnoughDistance;
    }
    
    
}
