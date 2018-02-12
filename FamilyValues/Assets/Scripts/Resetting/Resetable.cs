using Sirenix.OdinInspector;

public abstract class Resetable : SerializedMonoBehaviour 
{
	public abstract void ResetObject();
	public virtual void UpdateResetObject() {}
}
