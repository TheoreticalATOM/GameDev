using UnityEngine;
using UnityEditor;

using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

[CustomEditor(typeof(TaskList))]
public class TaskListEditor : OdinEditor
{
    private TaskList mTarget;
    protected override void OnEnable()
    {
        base.OnEnable();
        mTarget = target as TaskList;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.BeginVertical(SirenixGUIStyles.BoxContainer);
        GUILayout.Label("Task Order:", SirenixGUIStyles.BoldTitle);

        if (mTarget.TaskListNames.Count < 1)
            GUILayout.Label("No tasks set", SirenixGUIStyles.CenteredGreyMiniLabel);
        else
        {
            GUIStyle style = new GUIStyle();
            int i = 0;
            foreach (var task in mTarget.TaskListNames)
            {
                style.normal.textColor = (task.Value.Complete) ? Color.grey : Color.black;
                GUILayout.Label(++i + ". " + task.Value.Name, style);
            }
        }
        GUILayout.EndVertical();
    }
}