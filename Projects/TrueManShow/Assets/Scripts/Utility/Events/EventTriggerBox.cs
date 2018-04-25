using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EventTriggerBox : MonoBehaviour
{
    public bool OneUse = false;
    public UnityEvent TriggerEnteredEvent;

    private Collider mCollider;
    
    private void Awake()
    {
        mCollider = GetComponent<Collider>();
        mCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnteredEvent.Invoke();
        mCollider.enabled = !OneUse;
    }
}
