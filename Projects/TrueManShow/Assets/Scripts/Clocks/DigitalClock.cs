using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DigitalClock : Clock
{
    public Text Text;

    public override void StartTime(int hr, int min, int dur)
    {
        StopAllCoroutines();
        StartCoroutine(Rotate(hr, min, dur));
    }

    private IEnumerator Rotate(int hrs, int min, int dur)
    {
        int curDur = (hrs * 60) + min;
        int endDur = curDur + dur;
		
        while (curDur < endDur)
        {
            Text.text = GetHour(curDur).ToString("00") + ":" + GetMinute(curDur).ToString("00");
            yield return new WaitForSeconds(WAIT_TIME_SECONDS);
            curDur++;
        }
        ExecuteCallback();
    }
}