public class TCGameObjectActive : TogglerComponentBool
{
    protected override void OnToggled(bool value)
    {
        gameObject.SetActive(value);
    }
}