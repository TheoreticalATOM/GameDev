using UnityEngine.UI;

public class LoadingBarWithText : LoadingBar
{
    public Text ProgressText;

    public override void UpdateBar(float value, float maxValue)
    {
		base.UpdateBar(value, maxValue);
        ProgressText.text = (maxValue - value).ToString("00");
    }
}
