using Sirenix.OdinInspector;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    private delegate float DelProgressPercentage(float value, float maxValue);
    private readonly static DelProgressPercentage[] PROGRESS_METHODS =  { GetPercentage0To100, GetPercentage100To0 };

    public const float NEAR_ENOUGH_FUDGE_FACTOR = 0.4f;

    [System.Flags]
    public enum EBarDir { Horizontal = 1, Vertical = 2 }

    public enum EBarProgressDir { From0To100 = 0, From100To0 };

    public RectTransform Bar;
    public EBarDir Direction;
    public EBarProgressDir ProgressDirection = EBarProgressDir.From0To100;
    public float LerpSpeed;

    private Vector2 mOrigSize;
    private Vector2 mTargetSize;

    // ________________________________________________________ Controls
    /// <summary>
    /// Sets the bar size to be the percentage scale of its original size
    /// example: RectTransform: 200, value: 5, maxValue: 10 | size = RT * (value/maxValue) = 100
    /// </summary>
    public virtual void UpdateBar(float value, float maxValue)
    {
        mTargetSize = mOrigSize;
        float percent = PROGRESS_METHODS[(int)ProgressDirection](value, maxValue);
        SetBarRect(ref mTargetSize, percent);
        enabled = true;
    }

    private static float GetPercentage0To100(float value, float maxValue)
    {
        return value / maxValue;
    }
    private static float GetPercentage100To0(float value, float maxValue)
    {
        return (maxValue - value) / maxValue;
    }
    

    // ________________________________________________________ Methods
    private void Awake()
    {
        Vector2 size = Bar.sizeDelta;
        mOrigSize = size;

        // start by making the bar progress to be 0
        SetBarRect(ref size, 0.0f);
        Bar.sizeDelta = size;

        enabled = false;
    }

    private void LateUpdate()
    {
        // constantly move towards the targeted size, until the size is close enough 
        Vector2 currPos = Bar.sizeDelta;
        Bar.sizeDelta = Vector2.Lerp(currPos, mTargetSize, LerpSpeed * Time.deltaTime);

        // if the it is close enough to the targeted size, then disable the update
        // NOTE: using a near enough value, rather than the exact value due to floating point inaccuracy
        if ((mTargetSize - currPos).magnitude < NEAR_ENOUGH_FUDGE_FACTOR)
            enabled = false;
    }

    private void SetBarRect(ref Vector2 size, float value)
    {
        // update the appropriate size, depending on the binary x/y flags
        if ((Direction & EBarDir.Horizontal) == EBarDir.Horizontal) size.x *= value;
        if ((Direction & EBarDir.Vertical) == EBarDir.Vertical) size.y *= value;
    }
}

