using UnityEngine;
using SDE.Data;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Dialog Node", menuName = "Dialog/Node", order = 0)]
public class DialogNode : SerializedScriptableObject
{
    public RuntimeSet NarrativeSourceSet;
    public int Priority;
    public Segment[] Segments;
    [Space(50.0f)]
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout, IsReadOnly = false, KeyLabel = "key", ValueLabel = "value")]
    public Dictionary<ScriptableEnum, DialogNode> Children = new Dictionary<ScriptableEnum, DialogNode>();

    public ScriptableEnum Direction { get; set; }

    private void OnValidate()
    {
        if(Children.Count > 0)
            Direction = Children.Keys.First();
    }

    /// <summary>
    /// Will execute the dialog node, communicating with its Narrative Source.
    /// The Narrative source tracks completion and pushing the dialog to a given UI
    /// The callback method is called when Narrative Source finishes.
    /// </summary>
    public virtual void Play(System.Action OnFinishedCallback)
    {
        Assert.IsFalse(NarrativeSourceSet.IsEmpty, "Missing an NarrativeSource in the scene");
        NarrativeSource source = NarrativeSourceSet.GetFirst<NarrativeSource>();
        Assert.IsNotNull(source, "The RuntimeSet, is not set by an NarrativeSource");

        source.Play(Segments, OnFinishedCallback, Priority);
    }
    public void Play()
    {
        Play(null);
    }

    #region Child Manipulation
    /// <summary>
    /// This method will return this node's child node in a set direction.
    /// If it contains no nodes, or if it does not contain the direction specified, it will return NULL.
    /// </summary>
    /// <param name="childDirection">the childs direction</param>
    /// <returns>a child node</returns>
    public DialogNode Next(ScriptableEnum childDirection)
    {
        Assert.IsNotNull(childDirection, "The direction cannot be null");
        // if the child collection is non existent or doesnt have the desired direction. return null
        if (Children.Count < 1 || !Children.ContainsKey(childDirection))
            return null;

        // assert to make sure there is no null dialog node in the child collection
        DialogNode node = Children[childDirection];
        Assert.IsNotNull(node, "Requested child node on: \"" + name + "\" in the direction: \"" + childDirection.name + "\" was NULL");
        return node;
    }
    /// <summary>
    /// This method will return this node's child node in the direction specified in SetDirection(ScriptableEnum).
    /// This method, as opposed to Next(ScriptableEnum), relies on an internally stored direction, 
    /// which can be useful for setting the direction way before getting to this current node
    /// </summary>
    /// <returns>a child node</returns>
    public DialogNode Next()
    {     
        if(Children.Count < 1)
            return null;

        return Children[Direction];
    }

    /// <summary>
    /// Setting the direction sets the global direction for this specific node. 
    /// Meaning, that when Next() is called, it will travel in the set direction. 
    /// This allows for setting the direction of the node way before even getting to the node 
    /// </summary>
    /// <param name="direction">Needs to be in the node's child collection and cannot be null</param>
    public void SetDirection(ScriptableEnum direction)
    {
        Assert.IsTrue(Children.ContainsKey(direction), "Set direction: \"" + direction.name + "\" is not in node: \"" + name + "\"");
        Assert.IsNotNull(direction, "the direction passed into: " + name + " was NULL");

        Direction = direction;
    }
    #endregion
}
