public class IVFallbackVerificationTagCompare : IVFallbackVerification
{
	public string RequiredTag;
    public override bool OnVerify(ItemPhysicsInteract item)
    {
		return item.CompareTag(RequiredTag);
    }
}
