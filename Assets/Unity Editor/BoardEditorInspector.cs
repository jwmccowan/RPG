using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardEditor))]
public class BoardEditorInspector : Editor
{
    public BoardEditor current
    {
        get
        {
            return (BoardEditor)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Clear"))
            current.Clear();
        if (GUILayout.Button("Grow"))
            current.Raise();
        if (GUILayout.Button("Shrink"))
            current.Lower();
        if (GUILayout.Button("Grow Area"))
            current.RaiseArea();
        if (GUILayout.Button("Shrink Area"))
            current.LowerArea();
        if (GUILayout.Button("Save"))
            current.Save();
        if (GUILayout.Button("Load"))
            current.Load();
        if (GUI.changed)
            current.UpdateMarker();
    }
}
