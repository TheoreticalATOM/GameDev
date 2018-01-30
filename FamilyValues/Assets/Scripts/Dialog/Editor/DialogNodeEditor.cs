//using UnityEngine;
//using UnityEditor;
//using SDE.Data;

//using Sirenix.OdinInspector.Editor;

//[CustomEditor(typeof(DialogNode))]
//public class DialogNodeEditor : Editor
//{
//    private DialogNode mTarget;
//    private DialogNode mCurrNode;
//    private ScriptableEnum mCurrEnum;

//    private void OnEnable()
//    {
//        mTarget = target as DialogNode;
//    }

//    public override void OnInspectorGUI()
//    {
//        //base.OnInspectorGUI();
//        //foreach (var child in mTarget.Children)
//        //{
//        //    EditorGUILayout.BeginHorizontal();
//        //    GUILayout.Label(child.Value.name + " |" + child.Key.name);
//        //    EditorGUILayout.EndHorizontal();
//        //}

//        //GUILayout.Space(80.0f);
//        //GUILayout.Label("Add Child:");
//        //EditorGUILayout.BeginHorizontal();
//        //mCurrNode = EditorGUILayout.ObjectField(mCurrNode, typeof(DialogNode), false) as DialogNode;
//        //mCurrEnum = EditorGUILayout.ObjectField(mCurrEnum, typeof(ScriptableEnum), false) as ScriptableEnum;
//        //if (GUILayout.Button("Add"))
//        //{
//        //    mTarget.Children.Add(mCurrEnum, mCurrNode);
//        //    mCurrNode = null;
//        //    mCurrEnum = null;
//        //}
//        //EditorGUILayout.EndHorizontal();
//    }
//}