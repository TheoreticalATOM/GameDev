using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;


[CustomEditor(typeof(Task), true)]
public class TaskEditor : OdinEditor
{
    private Task mTarget;
    private GUIStyle mBaseStyle;
    protected override void OnEnable()
    {
        base.OnEnable();
        mTarget = target as Task;

        mBaseStyle = new GUIStyle();
		mBaseStyle.alignment = TextAnchor.MiddleLeft;
		mBaseStyle.fixedWidth = 10.0f;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical(SirenixGUIStyles.BoxContainer);
        GUILayout.BeginHorizontal();
        int barCount = Mathf.RoundToInt(mTarget.CurrentInPercentage * 10.0f);
		int minBarCount = Mathf.RoundToInt(mTarget.MinimumInPercentage * 10.0f);
		for (int i = 0; i < barCount; i++)
		{
			mBaseStyle.normal.textColor = (i < minBarCount) ? Color.red : Color.black;
            GUILayout.Label("❚", mBaseStyle);
		}
        GUILayout.Label(barCount * 10.0f + "%", mBaseStyle);
        GUILayout.EndHorizontal();
		GUILayout.Label(mTarget.CurrentTime.ToString("0.0") + "s / " + mTarget.MaximumTime + "s");
        GUILayout.EndVertical();

        base.OnInspectorGUI();
    }
}