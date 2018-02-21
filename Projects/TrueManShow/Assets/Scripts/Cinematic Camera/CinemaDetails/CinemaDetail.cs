using UnityEngine;

[System.Flags]
public enum MovementRestriction
{
    PX = 1, PY = 2, PZ = 4
}


[CreateAssetMenu(fileName = "CinemaDetailTrigger", menuName = "Cinema Details/Cinema Detail Trigger", order = 0)]
public class CinemaDetail : ScriptableObject
{
	public MovementRestriction Restriction;
	public string AnimationVariable;
	public Vector3 StartRotation;

	public virtual void SetValue(Animator animator)
	{
		animator.SetTrigger(AnimationVariable);
	}
}