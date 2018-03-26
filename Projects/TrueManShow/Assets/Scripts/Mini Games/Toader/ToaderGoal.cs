using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SDE;

[RequireComponent(typeof(BoxCollider2D))]
public class ToaderGoal : MonoBehaviour
{
    public Sprite Unused;
    public Sprite Used;
    public int PointAmount;
		
    public System.Action<int> OnGoalReached;

    private Image mImage;
    private BoxCollider2D mCollider;

    private void Awake()
    {
        mImage = GetComponent<Image>();
        mCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        mImage.sprite = Unused;
        mCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        mImage.sprite = Used;
        OnGoalReached.TryInvoke(PointAmount);
        mCollider.isTrigger = false;

    }
}
