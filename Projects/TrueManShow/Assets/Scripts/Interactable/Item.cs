using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    public abstract void StartInteract(GameObject Object, GameObject camera);

    public abstract bool InteractUpdate(GameObject Object, GameObject camera);

    public abstract void StopInteract(GameObject Object, GameObject camera);

}


/*
		int random = Random.Range(0, nodes.Length);
		nodes[random].Node.Play(()=> {});

        [System.Serializable]
public class DialogItemPiece
{
	public bool HasBeenUsedToday;
	public DialogNode Node;
}

public DialogItemPiece[] nodes;

 */