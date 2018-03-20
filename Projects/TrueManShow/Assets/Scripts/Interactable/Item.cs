using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(cakeslice.Outline))]
public abstract class Item : MonoBehaviour
{

    public DialogNode[] nodes;
    public bool CanBePickedUp = true;
    private bool mHasBeenUsedToday;

    public cakeslice.Outline ItemOutline { get; private set; }

    public void CanPickup(bool value)
    {
        CanBePickedUp = value;
    }

    protected virtual void Awake()
    {
        ItemOutline = GetComponent<cakeslice.Outline>();
        ItemOutline.eraseRenderer = true; // clear the renderer on the start
    }

    public void StartInteract(GameObject Object, GameObject camera)
    {
        if (!mHasBeenUsedToday && nodes.Length > 0)
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
