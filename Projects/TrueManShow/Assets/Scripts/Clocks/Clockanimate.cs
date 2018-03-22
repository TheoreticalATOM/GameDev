using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using System;
public class Clockanimate : Clocks

{

    public Transform hours, minutes;

    private const float hoursDeg = 360f / 12f, minuteDeg = 360f / 60f;

	public override void StartTime(int hr,int min, int dur, System.Action callBack)

    {
		StopAllCoroutines ();
		StartCoroutine(Rotate(hr,min,dur,callBack));

    }

	IEnumerator Rotate(int hrs,int min,int dur,System.Action callBack)

    {
		int durC = 0;
        for (int i = hrs; i <= 11; i++)

        {
            if (i == 11)
            {
                hours.localRotation = Quaternion.Euler(0f, 0f, i * -hoursDeg);
               	i = -1;
            }
            else
                hours.localRotation = Quaternion.Euler(0f, 0f, i * -hoursDeg);

            for (int j = min; j <= 59; j++)

            {
                if(j==59)
                {
                    minutes.localRotation = Quaternion.Euler(0f, 0f, j * -minuteDeg);
                    min = 0;
                }
                else
                    minutes.localRotation = Quaternion.Euler(0f, 0f, j * -minuteDeg);

                    yield return new WaitForSeconds(seconds: 60f);
				durC++;
				if (durC > dur)
					goto endLoop;

            }
        }
		endLoop:;
		if(callBack!=null)
		{
			callBack();
		}
    }
}
