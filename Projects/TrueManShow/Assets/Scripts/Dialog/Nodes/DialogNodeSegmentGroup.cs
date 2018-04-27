using System;
using System.Text.RegularExpressions;
using SDE;
using SDE.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class SegmentGroup
{
    [InlineEditor(InlineEditorModes.FullEditor)] public DialogNodeBase Node;
    public GameEvent OnCompletedGroup;
}

[CreateAssetMenu(fileName = "Dialog Node Segment Group", menuName = "Dialog/Dialog Segment Group Node", order = 0)]
public class DialogNodeSegmentGroup : DialogNodeBase
{
    public SegmentGroup[] Groups;
    
    public override void Play(Action onFinishedCallback)
    {      
        Assert.IsTrue(Groups.Length > 0, "Missing Segments in the group");
        PlayRecursiveASync(onFinishedCallback, 0);
    }

    private void PlayRecursiveASync(Action onFinishedCallback, int index)
    {
        if (index >= Groups.Length)
        {
            onFinishedCallback.TryInvoke();
            return;
        }
        
        SegmentGroup group = Groups[index];
        group.Node.Play(() =>
        {
            GameEvent e = group.OnCompletedGroup;
            if(e) e.Raise();
            PlayRecursiveASync(onFinishedCallback, ++index);
        });
        
    }
    
}
