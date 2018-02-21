using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Item : MonoBehaviour {

    public DialogNode[] nodes; 
    private bool mHasBeenUsedToday;

    public void StartInteract(GameObject Object, GameObject camera)
    {
        if(!mHasBeenUsedToday && nodes.Length > 0)
        {
            int random = Random.Range(0, nodes.Length);
            Assert.IsNotNull(nodes[random], "a node in " + name + " is null");
            
            nodes[random].Play(null);
            mHasBeenUsedToday = true;
        }



        OnStartInteract(Object, camera);
    }

    protected abstract void OnStartInteract(GameObject Object, GameObject camera);

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