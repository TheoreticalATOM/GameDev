using UnityEngine;
using UnityEngine.Events;

public class ItemApperance : Item
{
    public float objApp = 10f;
    bool itemUsed = false;
    public bool oneUse;
    public DirectorAwarenessValue AppearanceVariable;
    public UnityEvent OnInteracted;

    public override bool InteractUpdate(GameObject Object, GameObject camera) { return false; }
    public override void StopInteract(GameObject Object, GameObject camera) { }

    protected override void OnStartInteract(GameObject Object, GameObject player)
    {
        /*if (itemUsed == false)
        {
            AppearanceVariable.UpdateValue(objApp);
            itemUsed = true;
            if(gameObject.tag == "OneTime")
            {
                gameObject.SetActive(false);// Destroy(gameObject);
            }
            OnInteracted.Invoke();
        }*/

        OnInteracted.Invoke();
        
        if (itemUsed)
            return;

        AppearanceVariable.UpdateValue(objApp);
        gameObject.SetActive(!oneUse);
        itemUsed = true;
    }
}
