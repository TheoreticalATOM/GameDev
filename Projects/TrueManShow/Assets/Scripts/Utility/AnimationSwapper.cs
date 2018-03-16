using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSwapper : MonoBehaviour 
{
	public Animator Animator;
	public ScriptableAnimation AnimationA;
	public ScriptableAnimation AnimationB;

	private ScriptableAnimation mCurrAnimation;

	private void Reset() 
	{
		if(!Animator)
			Animator = GetComponent<Animator>();	
	}

	private void Awake()
	{
		mCurrAnimation = AnimationA;
	}

	public void AnimateAndSwap()
	{
		mCurrAnimation.SetValue(Animator);
		mCurrAnimation = (mCurrAnimation == AnimationA) ? AnimationB : AnimationA;
	}
}
