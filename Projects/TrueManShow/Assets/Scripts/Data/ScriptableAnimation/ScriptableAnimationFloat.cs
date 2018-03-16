using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Animation Float", menuName = "Animation/Scriptable Animation Float", order = 0)]
public class ScriptableAnimationFloat : ScriptableAnimation
{
    public float Value;
    public override void SetValue(Animator animator)
    {
        animator.SetFloat(VariableName, Value);
    }
}