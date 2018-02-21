using UnityEngine;
using UnityEngine.Events;

public class ItemApperance : Item
{
    public float Ratio;
    public DirectorAwarenessValue AppearanceVariable;
    public UnityEvent OnInteracted;

    public override bool InteractUpdate(GameObject Object, GameObject camera) { return false; }
    public override void StopInteract(GameObject Object, GameObject camera) { }

    protected override void OnStartInteract(GameObject Object, GameObject player)
    {
        float objApp = 10f;
        AppearanceVariable.UpdateValue(objApp);
        print(AppearanceVariable.Value);

        OnInteracted.Invoke();
    }

}
