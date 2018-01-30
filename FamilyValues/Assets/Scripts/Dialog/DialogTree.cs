using UnityEngine;

using SDE.Data;

[CreateAssetMenu(fileName = "Dialog Tree", menuName = "Dialog/Tree", order = 1)]
public class DialogTree : ScriptableObject
{
    public DialogNode Root;
    public DialogNode ActiveNode { get; private set; }

    private void OnEnable()
    {
        ActiveNode = Root;
    }

    public void NextNode(ScriptableEnum direction)
    {
        ActiveNode = Root.Next(direction);
    }
}

