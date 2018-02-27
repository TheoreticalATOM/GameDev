using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlEffector : MonoBehaviour {

	public Color ColourChange;
	public BowlFilling Bowl;

	public void AddToBowl()
	{
		Bowl.AddToBowl(ColourChange);
	}
}
