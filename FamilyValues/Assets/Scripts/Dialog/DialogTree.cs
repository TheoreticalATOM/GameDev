using UnityEngine;

using SDE.Data;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Dialog Tree", menuName = "Dialog/Tree", order = 1)]
public class DialogTree : ScriptableObject
{
    public DialogNode Root;
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
    }

    public bool Play()
    {
        mNodeTrace.AddLast(ActiveNode);
        ActiveNode.Play(Next);

        return true;
    }
}

