using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIFlow))]
public class UIFlowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIFlow flow = (UIFlow)target;

        flow.Columns = EditorGUILayout.IntField("Cols", flow.Columns);
        flow.Rows = EditorGUILayout.IntField("Rows", flow.Rows);

        flow.HorizontalDistance = EditorGUILayout.FloatField("H. Distance", flow.HorizontalDistance);
        flow.VerticalDistance = EditorGUILayout.FloatField("V. Distance", flow.VerticalDistance);
        flow.Alignment = EditorGUILayoutExt.AlignmentChoose("Content Alignment", flow.Alignment);
        flow.Direction = (UI.Direction)EditorGUILayout.EnumPopup("Content Direction", flow.Direction);

        flow.Commit();
    }
}