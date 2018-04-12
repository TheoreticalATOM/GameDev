using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TogglerComponentBool : TogglerComponent
{
	public bool Inverted;
	private bool mBoolState;
	
	protected abstract void OnToggled(bool value);
	
	protected sealed override void OnToggleA() { ToggleBool(); }
	protected sealed override void OnToggleB() { ToggleBool(); }
	
	private void Awake()
	{
		mBoolState = !Inverted;
		OnToggled(mBoolState);
	}
	
	private void ToggleBool()
	{
		mBoolState = !mBoolState;
		OnToggled(mBoolState);
	}
}
