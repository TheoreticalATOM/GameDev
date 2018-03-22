using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalClock : Clocks
{

    public TextMesh hoursT, minutesT;
	public override void StartTime (int hr,int min, int dur, System.Action callBack)
    {
		StopAllCoroutines ();
		StartCoroutine(Rotate(hr,min,dur,callBack));
    }

	IEnumerator Rotate(int hour,int minutes,int dur,System.Action callBack)
    {
        int durC = 0;
            for (int i = hour; i <= 23 ; i++)
            {
                if (i == 23)
                {
                    hoursT.text = i.ToString(format: "00");
                    i = -1;
                }
                else
                {
                    hoursT.text = i.ToString(format: "00");
                }
                for (int j = minutes; j <= 59; j++)
                {
                    if (j == 59)
                    {
                        minutesT.text = j.ToString(format: "00");
                        minutes = 0;
                    }
                    else
                    {
                        minutesT.text = j.ToString(format: "00");
                    }
                    yield return new WaitForSeconds(seconds: 60f);
                    durC++;
                    if (durC > dur)
                    {
                    goto endLoop;
                    }
                }
            }
    endLoop:;
           	if(callBack!=null)
            {
                callBack();
            }
    }
}
