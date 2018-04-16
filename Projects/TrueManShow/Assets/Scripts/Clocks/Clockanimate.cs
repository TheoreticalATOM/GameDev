using System.Collections;
using UnityEngine;

public class Clockanimate : Clock
{
    public Transform hours, minutes;

    private const float hoursDeg = 360f / 12.0f, minuteDeg = 360f / 60f;
	
	public override void StartTime(int hr,int min, int dur)

    {
		StopAllCoroutines ();
		StartCoroutine(Rotate(hr,min,dur));

    }
	
//	private IEnumerator Rotate(int hrs,int min,int dur)
//    {
//		int durC = 0;
//        for (int i = hrs; i <= 23; i++)
//        {
//            if (i == 23)
//            {
//                hours.localRotation = Quaternion.Euler(0f, 0f, i * hoursDeg);
//               	i = -1;
//            }
//            else
//                hours.localRotation = Quaternion.Euler(0f, 0f, i * hoursDeg);
//
//            for (int j = min; j <= 59; j++)
//            {
//                if(j==59)
//                {
//                    minutes.localRotation = Quaternion.Euler(0f, 0f, j * -minuteDeg);
//                    min = 0;
//                }
//                else
//                    minutes.localRotation = Quaternion.Euler(0f, 0f, j * -minuteDeg);
//
//                    yield return new WaitForSeconds(seconds: 60f);
//				durC++;
//				if (durC > dur)
//					goto endLoop;
//
//            }
//        }
//		endLoop:;
//	    ExecuteCallback();
//    }

	private IEnumerator Rotate(int hrs, int min, int dur)
	{
		int curDur = (hrs * 60) + min;
		int endDur = curDur + dur;
		
		while (curDur < endDur)
		{
			hours.localRotation = Quaternion.Euler(0f, 0f, GetHour(curDur) * hoursDeg);
			minutes.localRotation = Quaternion.Euler(0f, 0f, GetMinute(curDur) * minuteDeg);
			yield return new WaitForSeconds(WAIT_TIME_SECONDS);
			curDur++;
		}
		ExecuteCallback();
	}
}
