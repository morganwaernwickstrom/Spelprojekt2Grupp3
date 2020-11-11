using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileEditor))]
public class TileEditorGUI : Editor
{
    public override void OnInspectorGUI()
    {
        TileEditor myTileEditor = (TileEditor)target;
        SerializedObject editorInstance = new SerializedObject(myTileEditor);

        editorInstance.Update();

        myTileEditor.MyWidth = EditorGUILayout.IntField("Width", myTileEditor.MyWidth);
        myTileEditor.MyHeight = EditorGUILayout.IntField("Height", myTileEditor.MyHeight);

        if (GUILayout.Button("Generate Level"))
        {
            myTileEditor.GenerateTiles();
        }

        if (GUILayout.Button("Clear Level"))
        {
            myTileEditor.ClearTiles();
        }

        editorInstance.ApplyModifiedProperties();
    }
}
