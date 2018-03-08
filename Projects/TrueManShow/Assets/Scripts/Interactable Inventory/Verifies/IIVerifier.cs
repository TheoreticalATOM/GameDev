using Sirenix.OdinInspector;

public abstract class IIVerifier : SerializedMonoBehaviour 
{
	public IIVerifier Child;
	
	public bool Verify(ItemPhysicsInteract item, ref int failCount)
	{
		bool result = OnVerifiying(item);
		if(result) return true;

		failCount++;

		if(Child)
			return Child.Verify(item, ref failCount);
		
		return false;
		//return result || (Child && Child.Verify(item, ref failCount));
	}

	protected abstract bool OnVerifiying(ItemPhysicsInteract item);
}
