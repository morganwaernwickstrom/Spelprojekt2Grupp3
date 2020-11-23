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

        if (GUILayout.Button("Place Wall"))
        {
            myTile.PlaceImpassable();
        }

        if (GUILayout.Button("Place Sliding Block"))
        {
            myTile.PlaceSlidingBlock();
        }

        if (GUILayout.Button("Place Hole"))
        {
            myTile.PlaceHole();
        }

        if (GUILayout.Button("Place Finish"))
        {
            myTile.PlaceFinish();
        }

        if (GUILayout.Button("Place Button"))
        {
            myTile.PlaceButton();
        }

        if (GUILayout.Button("Place Door"))
        {
            myTile.PlaceDoor();
        }

        if (GUILayout.Button("Place Laser Emitter"))
        {
            myTile.PlaceLaserEmitter();
        }
        if (GUILayout.Button("Place Laser Reflector"))
        {
            myTile.PlaceLaserReflector();
        }
        if (GUILayout.Button("Place Laser Receiver"))
        {
            myTile.PlaceLaserReceiver();
        }
        if (GUILayout.Button("Place Rail"))
        {
            myTile.PlaceRail();
        }

        if (GUILayout.Button("Place Train"))
        {
            myTile.PlaceTrain();
        }

        if (GUILayout.Button("Remove"))
        {
            myTile.RemoveCurrent();
        }

        tileInstance.ApplyModifiedProperties();
    }
}
