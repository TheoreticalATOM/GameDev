using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EventTriggerBox : MonoBehaviour
{
    public UnityEvent TriggerEnteredEvent;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnteredEvent.Invoke();
    }
}
