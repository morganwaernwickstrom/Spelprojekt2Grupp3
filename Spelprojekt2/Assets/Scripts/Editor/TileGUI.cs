using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tile))]
public class TileGUI : Editor
{
    public override void OnInspectorGUI()
    {
        Tile myTile = (Tile)target;
        SerializedObject tileInstance = new SerializedObject(myTile);

        myTile.GetComponent<Transform>().hideFlags = HideFlags.HideInInspector;
        myTile.GetComponent<Renderer>().hideFlags = HideFlags.HideInInspector;
        myTile.GetComponent<MeshFilter>().hideFlags = HideFlags.HideInInspector;

        tileInstance.Update();

        if (GUILayout.Button("Place Player"))
        {
            myTile.PlacePlayer();
        }

        if (GUILayout.Button("Place Rock"))
        {
            myTile.PlaceRock();
        }

        if (GUILayout.Button("Place Hole"))
        {
            myTile.PlaceHole();
        }

        if (GUILayout.Button("Place Finish"))
        {
            myTile.PlaceFinish();
        }

        if (GUILayout.Button("Remove"))
        {
            myTile.RemoveCurrent();
        }

        tileInstance.ApplyModifiedProperties();
    }
}
