using UnityEngine;
public class TCEnabler : TogglerComponentBool
{
    public Behaviour Behaviour;   
    protected override void OnToggled(bool value)
    {
        Behaviour.enabled = value;
    }
}
