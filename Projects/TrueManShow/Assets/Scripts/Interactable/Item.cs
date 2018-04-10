using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(cakeslice.Outline))]
public abstract class Item : MonoBehaviour
{

    public DialogNodeBase[] nodes;
    private bool mHasBeenUsedToday;

    public cakeslice.Outline ItemOutline { get; private set; }
    
    protected virtual void Awake()
    {
        ItemOutline = GetComponent<cakeslice.Outline>();
        ItemOutline.eraseRenderer = false; // clear the renderer on the start
        ItemOutline.enabled = false;
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