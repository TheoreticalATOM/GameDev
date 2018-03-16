using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Animation Bool", menuName = "Animation/Scriptable Animation Bool", order = 0)]
public class ScriptableAnimationBool : ScriptableAnimation
{
    public bool Value;
    public override void SetValue(Animator animator)
    {
        animator.SetBool(VariableName, Value);
    }
}