using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileEditor))]
public class TileEditorGUI : Editor
{
    public override void OnInspectorGUI()
    {
        TileEditor myTileEditor = (TileEditor)target;
        SerializedObject editorInstance = new SerializedObject(myTileEditor);

        //myTileEditor.GetComponent<Transform>().hideFlags = HideFlags.HideInInspector;

        editorInstance.Update();

        EditorGUILayout.HelpBox("Generate a tilemap. if you already have an active tilemap, that one will be overwritten", MessageType.Info);
        myTileEditor.myWidth = EditorGUILayout.IntField("Width", myTileEditor.myWidth);
        myTileEditor.myHeight = EditorGUILayout.IntField("Height", myTileEditor.myHeight);

        if (GUILayout.Button("Generate Level"))
        {
            myTileEditor.GenerateTiles();
        }

        EditorGUILayout.HelpBox("Clear all existing tiles", MessageType.Info);
        if (GUILayout.Button("Clear Level"))
        {
            myTileEditor.ClearTiles();
        }

        editorInstance.ApplyModifiedProperties();
    }
}
