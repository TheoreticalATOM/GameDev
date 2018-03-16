using UnityEngine.Events;

public class CountdownEventStart : CountdownEvent
{
	public UnityEvent OnStarted;
    public override void StartTimer()
    {
		OnStarted.Invoke();
        base.StartTimer();
    }
}
