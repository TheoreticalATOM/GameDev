﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable, RequireComponent(typeof(MeshRenderer))]
public class BowlFilling : SerializedMonoBehaviour
{
    [System.Serializable]
    public struct StepDetails
    {
        public Vector3 Offset;
        public Vector3 Scale;
    }

    public StepDetails MaxDetails;
    [MinValue(1)] public int StepCount;
    public float Speed;
    public float CloseEnoughDistance;

    private int mStepTarget;
    [SerializeField, HideInInspector] private StepDetails[] mSteps;
    private Coroutine mUpdateRoutine;

    private Material mMaterial;
    private Color mAvgColour;

    private Vector3 mOrigPos;

    private void Awake()
    {
        mMaterial = GetComponent<MeshRenderer>().material;
        mOrigPos = transform.localPosition;
    }

    private void OnValidate()
    {
        mOrigPos = transform.localPosition;
    }

    private void OnDrawGizmos()
    {
        Vector3 thisPos = transform.position;
        Gizmos.color = Color.green;
        for (int i = 0; i < mSteps.Length; i++)
            Gizmos.DrawWireCube(mSteps[i].Offset + thisPos, mSteps[i].Scale);

        Gizmos.DrawWireCube(MaxDetails.Offset, MaxDetails.Scale);
    }

    [Button]
    public void GenerateSteps()
    {
        Vector3 thisPos = mOrigPos;
        Vector3 size = transform.localScale;

        Vector3 posStep = ((MaxDetails.Offset + thisPos) - thisPos) / StepCount;
        Vector3 scaleStep = (MaxDetails.Scale - size) / StepCount;
        Debug.Log("Pos Step: [" + posStep + "] | Scale Step: [" + scaleStep + "]");

        mSteps = new StepDetails[StepCount];
        Vector3 offset = Vector3.zero;
        for (int i = 0; i < StepCount; i++)
        {
            offset.x += posStep.x;
            offset.y += posStep.y;
            offset.z += posStep.z;

            size.x += scaleStep.x;
            size.y += scaleStep.y;
            size.z += scaleStep.z;

            mSteps[i] = new StepDetails()
            {
                Offset = offset,
                Scale = size
            };
        }
    }

    public void AddToBowl(Color colour)
    {
        if (mUpdateRoutine != null)
            StopCoroutine(mUpdateRoutine);

        mAvgColour = (mAvgColour + colour) / 2.0f;

        mUpdateRoutine = StartCoroutine(UpdateRoutine(mSteps[mStepTarget], mAvgColour));
        mStepTarget = Mathf.Min(mStepTarget + 1, mSteps.Length - 1);
    }

    private IEnumerator UpdateRoutine(StepDetails details, Color colour)
    {
        float distance = 0.0f;
        Vector3 targetPos = details.Offset + mOrigPos;
        do
        {
            float dSpeed = Time.deltaTime * Speed;

            // Set Position
            Vector3 thisPos = transform.localPosition;
            thisPos = Vector3.Slerp(thisPos, targetPos, dSpeed);
            transform.localPosition = thisPos;

            // Set Scale
            Vector3 thisScale = transform.localScale;
            thisScale = Vector3.Slerp(thisScale, details.Scale, dSpeed);
            transform.localScale = thisScale;

            // Set Material colour
            mMaterial.color = Color.Lerp(mMaterial.color, colour, dSpeed);

            // check distance
            distance = (thisPos - targetPos).sqrMagnitude;
            yield return null;
        } while (distance > CloseEnoughDistance * CloseEnoughDistance);
    }
}
