using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clocks : MonoBehaviour {

	// Use this for initialization
	public abstract void StartTime (int hr, int min, int dur, System.Action callBack );
}
