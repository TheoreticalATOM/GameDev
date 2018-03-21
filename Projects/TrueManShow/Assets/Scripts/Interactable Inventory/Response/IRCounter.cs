using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IRCounter : InventoryResponse
{
    public float DurationInSeconds;
    public bool OneUse;
    public UnityEvent OnCounterComplete;

    private float mLastTime = 0.0f;
    private System.Action mUpdateAction;

    private void Start()
    {
		mUpdateAction = InitializeTime;
    }

    protected override void OnFailResponse()
    {

	}

    protected override void OnSuccessResponse()
    {
        mUpdateAction();
    }



    void InitializeTime()
    {
        mLastTime = Time.time;
        mUpdateAction = UpdateTime;
    }

    void UpdateTime()
    {
        if (Time.time - mLastTime > DurationInSeconds)
        {
            OnCounterComplete.Invoke();
            
			if (OneUse) mUpdateAction = () => { };
            else mUpdateAction = InitializeTime;
        }
    }
}
