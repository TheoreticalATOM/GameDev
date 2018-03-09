public class ResponseCollectionAny : ResponseCollection
{
    public ResponseDetails Details;

    public override ResponseDetails GetResponseDetails(InteractiveInventory inventory)
    {
        return Details;
    }
}