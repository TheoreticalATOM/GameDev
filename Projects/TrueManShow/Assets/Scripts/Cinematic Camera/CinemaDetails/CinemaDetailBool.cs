using UnityEngine;

[CreateAssetMenu(fileName = "CinemaDetailBool", menuName = "Cinema Details/Cinema Detail Boolean", order = 0)]
public class CinemaDetailBool : CinemaDetail
{
	public bool Value;

	public override void SetValue(Animator animator)
	{
		animator.SetBool(AnimationVariable, Value);
	}	
}