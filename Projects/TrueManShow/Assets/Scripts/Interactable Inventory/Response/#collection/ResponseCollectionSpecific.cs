using Sirenix.OdinInspector;
using System.Collections.Generic;

public class ResponseCollectionSpecific : ResponseCollection
{
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout, IsReadOnly = false, KeyLabel = "key", ValueLabel = "value")]
    public Dictionary<InteractiveInventory, ResponseDetails> Response;

    public override ResponseDetails GetResponseDetails(InteractiveInventory inventory)
    {
        return Response[inventory];
    }
}