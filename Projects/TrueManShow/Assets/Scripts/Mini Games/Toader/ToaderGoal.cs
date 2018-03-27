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
    public BoxCollider2D Collider;

    public System.Action<int> OnGoalReached;

    private Image mImage;

    private void Awake()
    {
        mImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        ResetGoal();
    }

    public void ResetGoal()
    {
        mImage.sprite = Unused;
        Collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        mImage.sprite = Used;
        OnGoalReached.TryInvoke(PointAmount);
        Collider.enabled = true;
    }
}
