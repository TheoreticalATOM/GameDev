using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Animation", menuName = "Animation/Scriptable Animation Trigger", order = 0)]
public class ScriptableAnimation : ScriptableObject 
{
	public string VariableName;

	public virtual void SetValue(Animator animator)
	{
		animator.SetTrigger(VariableName);
	}
}
