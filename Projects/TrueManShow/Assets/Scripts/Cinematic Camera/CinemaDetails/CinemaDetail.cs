using UnityEngine;

[System.Flags]
public enum MovementRestriction
{
    PX = 1, PY = 2, PZ = 4
}

[CreateAssetMenu(fileName = "CinemaDetailTrigger", menuName = "Cinema Detail", order = 0)]
public class CinemaDetail : ScriptableObject
{
    public MovementRestriction Restriction;
	public Vector3 StartRotation;

    public float CloseEnoughDistance = 0.2f;
    public float RotatedEnough = 0.2f;

    public float Speed;
}