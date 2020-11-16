using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectOnTile))]
public class ObjectOnTileGUI : Editor
{
    public override void OnInspectorGUI()
    {
        ObjectOnTile myTileObject = (ObjectOnTile)target;

        if (GUILayout.Button("Remove"))
        {
            myTileObject.Remove();
        }

        if (GUILayout.Button("Rotate"))
        {
            myTileObject.Rotate();
        }
    }
}
