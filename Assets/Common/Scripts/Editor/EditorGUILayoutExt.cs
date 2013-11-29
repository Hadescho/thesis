using UnityEditor;
using UnityEngine;

class EditorGUILayoutExt
{
    public static UI.Alignment AlignmentChoose(string label, UI.Alignment value)
    {
        UI.Alignment result = value;

        GUILayout.BeginHorizontal();
            GUILayout.Label("Content Anchor");

            GUILayout.BeginVertical(GUILayout.MaxWidth(80.0f));
                GUILayout.BeginHorizontal();
                    result = _AlignmentFieldToggle(result, UI.Alignment.TopLeft);
                    result = _AlignmentFieldToggle(result, UI.Alignment.TopCenter);
                    result = _AlignmentFieldToggle(result, UI.Alignment.TopRight);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    result = _AlignmentFieldToggle(result, UI.Alignment.MiddleLeft);
                    result = _AlignmentFieldToggle(result, UI.Alignment.MiddleCenter);
                    result = _AlignmentFieldToggle(result, UI.Alignment.MiddleRight);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    result = _AlignmentFieldToggle(result, UI.Alignment.BottomLeft);
                    result = _AlignmentFieldToggle(result, UI.Alignment.BottomCenter);
                    result = _AlignmentFieldToggle(result, UI.Alignment.BottomRight);
                GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        return(result);
    }

    private static UI.Alignment _AlignmentFieldToggle(UI.Alignment valueGroup, UI.Alignment value)
    {
        if(EditorGUILayout.Toggle(valueGroup == value))
        {
            return(value);
        }

        return(valueGroup);
    }
}