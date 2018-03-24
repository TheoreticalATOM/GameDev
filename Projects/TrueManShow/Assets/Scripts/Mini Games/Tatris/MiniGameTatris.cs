using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SDE.Input;

public class MiniGameTatris : MiniGame
{
    public readonly float TILE_SIZE = 8.0f;

    [Header("Movement")]
    public float RateInSeconds;
    public float RateSpeedUpInSeconds;
    public Tatromino Test;

    private AxisUp mVerticalAxis;
    private AxisUp mHorizontalAxis;
    private float mLastTime;
    private float mRate;

    private Vector2 mMapSize;
    private Vector2 mMapPos;

    protected override void OnInit()
    {
        mVerticalAxis = new AxisUp();
		mHorizontalAxis = new AxisUp();
        mLastTime = 0.0f;
    }

    protected override void OnPlay()
    {
        mLastTime = Time.time;
    }

    protected override void OnUpdate()
    {
        float vInput = Input.GetAxisRaw("Vertical");
        float hInput = mHorizontalAxis.GetAxisUp(Input.GetAxisRaw("Horizontal"));
        float vInputUp = mVerticalAxis.GetAxisUp(vInput);

		// Rotation
        if (vInputUp > 0.0f)
            Test.Rotate();

		// X Movement
        Test.MoveHorizontal(hInput);

        // Y Movement
        mRate = (vInput < 0.0f) ? RateSpeedUpInSeconds : RateInSeconds;
        if (Time.time - mLastTime > mRate)
        {
            mLastTime = Time.time;
            Test.MoveDown();
        }
    }


    private bool IsWithinGrid(RectTransform tatromino)
    {
        Vector2 pos = tatromino.position;
        Vector3 size = tatromino.sizeDelta;

        return true;
    }
}
