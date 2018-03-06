using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class IVReaction : SerializedMonoBehaviour
{
	public abstract void OnReact(ItemPhysicsInteract item, System.Action onCompletedCallback);
}
