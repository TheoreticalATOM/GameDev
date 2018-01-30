using UnityEngine;
using SDE.Data;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Dialog Node", menuName = "Dialog/Node", order = 0)]
public class DialogNode : SerializedScriptableObject
{
    public Segment[] Segments;
    [Space(50.0f)]
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout, IsReadOnly = false, KeyLabel = "key", ValueLabel = "value")]
    public Dictionary<ScriptableEnum, DialogNode> Children = new Dictionary<ScriptableEnum, DialogNode>();
    public DialogNode Next(ScriptableEnum childDirection)
    {
        return Children[childDirection];
    }
}

[System.Serializable]
public class Segment
{
    [Header("Text")]
    [TextArea(3, 15)] public string Text;
    [MinValue(0.0f)] public float TextDelayInSeconds;
    
    [Header("Audio")]
    public AudioClip Clip;
    public RuntimeSet AudioPlayerLocation;
}
