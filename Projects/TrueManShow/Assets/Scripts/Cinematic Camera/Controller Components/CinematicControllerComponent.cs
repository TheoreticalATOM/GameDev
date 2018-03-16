using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CinematicControllerComponent : MonoBehaviour 
{
	public abstract void Respond(CinemaCam camera, System.Action onCompletionCallback);
}
