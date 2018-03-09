public class ResponseCollectionAny : ResponseCollection
{
    public InventoryResponse Response;
    public override InventoryResponse  GetResponse(InteractiveInventory inventory)
    {
        return Response;
    }
}