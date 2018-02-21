using UnityEngine;

[CreateAssetMenu(fileName = "CinemaDetailFloat", menuName = "Cinema Details/Cinema Detail Float", order = 0)]
public class CinemaDetailFloat : CinemaDetail
{
    public float Value;

    public override void SetValue(Animator animator)
    {
        animator.SetFloat(AnimationVariable, Value);
    }
}