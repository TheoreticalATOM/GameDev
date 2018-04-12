public class DCRToggler : DayCycleReactor
{
    public Toggler Toggler;
    public bool ResetValue;

    private void Reset()
    {
        if (!Toggler)
            Toggler = GetComponent<Toggler>();
    }

    public override void OnReact()
    {
        Toggler.SetToggleState(ResetValue);
    }
}
