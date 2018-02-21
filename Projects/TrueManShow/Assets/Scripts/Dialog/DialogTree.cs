using UnityEngine;

using SDE.Data;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Dialog Tree", menuName = "Dialog/Tree", order = 1)]
public class DialogTree : ScriptableObject
{
    public DialogNode Root;
    public GameEvent OnReachedEnd;

    public DialogNode ActiveNode { get; private set; }

    private LinkedList<DialogNode> mNodeTrace = new LinkedList<DialogNode>();

    private void OnEnable()
    {
        ActiveNode = Root;
    }

    public void NextNode(ScriptableEnum direction)
    {
        ActiveNode = ActiveNode.Next(direction);
    }

    public void Next()
    {
        ActiveNode = ActiveNode.Next();
        // if the active node is null, then raise the tree finished event
        if (!ActiveNode)
        {
            if(OnReachedEnd)
                OnReachedEnd.Raise();
        }
    }

    public bool Play()
    {
        bool isNotPassedLeafNode = ActiveNode != null;

        // if the active node is null (gone passed a leaf node), then play the node
        if (isNotPassedLeafNode)
        {
            mNodeTrace.AddLast(ActiveNode);
            ActiveNode.Play(Next);
        }
        return isNotPassedLeafNode;
    }
}

