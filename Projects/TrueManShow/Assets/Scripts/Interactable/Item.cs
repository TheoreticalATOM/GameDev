using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Item : MonoBehaviour
{
    public DialogNodeBase[] nodes;
    public bool OneDialogUse = false;
    public UIInteractionValue InteractionResponse;
    
    private bool mHasBeenUsedToday;

    protected virtual void Awake() { }

    public void StartInteract(GameObject Object, GameObject camera)
    {
        if (!mHasBeenUsedToday && nodes.Length > 0)
        {
            int random = Random.Range(0, nodes.Length);
            Assert.IsNotNull(nodes[random], "a node in " + name + " is null");

            nodes[random].Play(null);

            mHasBeenUsedToday = OneDialogUse;
        }
        OnStartInteract(Object, camera);
    }

    protected abstract void OnStartInteract(GameObject Object, GameObject camera);

    public abstract bool InteractUpdate(GameObject Object, GameObject camera);

    public abstract void StopInteract(GameObject Object, GameObject camera);

}