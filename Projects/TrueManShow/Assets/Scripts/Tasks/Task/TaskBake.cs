using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskBake : SubTaskLogger
{
    public float WaitingDurationInSeconds;

    public ItemInteract Door;
    public ItemInteract Knob;
    public GameObject Bowl;
    public GameObject Cake;
    public GameObject CakePlacementTrigger;
    public UnityEvent CakeCompleteEvent;

    private Coroutine mBakingDelayRoutine;

    public bool IsDoorClosed { get; set; }
    public bool HasCake { get; set; }

    public void BakeCake()
    {
        if (!Cake.activeSelf && HasCake && IsDoorClosed)
        {
            if (mBakingDelayRoutine == null)
                mBakingDelayRoutine = StartCoroutine(BakingDelay());
        }
    }

    IEnumerator BakingDelay()
    {
        Door.CanBeInteractedWith = false;
        Knob.CanBeInteractedWith = false;

        HasCake = false;
        Bowl.SetActive(false);
        Cake.SetActive(true);
        CakePlacementTrigger.SetActive(false);

        yield return new WaitForSeconds(WaitingDurationInSeconds);
        CakeCompleteEvent.Invoke();
    }

    public void UnlockOven()
    {
        Door.CanBeInteractedWith = true;
        Knob.CanBeInteractedWith = true;
        Knob.OnInteracted.Invoke();
    }
}
