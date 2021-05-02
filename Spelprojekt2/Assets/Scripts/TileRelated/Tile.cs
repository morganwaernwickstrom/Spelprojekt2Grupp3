using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject myRock = null;
    [SerializeField]
    private GameObject myImpassable = null;
    [SerializeField]
    private GameObject mySlidingBlock = null;
    [SerializeField]
    private GameObject myHole = null;
    [SerializeField]
    private GameObject myFinish = null;
    [SerializeField]
    private GameObject myButton = null;
    [SerializeField]
    private GameObject myDoor = null;
    [SerializeField]
    private GameObject myEmitter = null;
    [SerializeField]
    private GameObject myReflector = null;
    [SerializeField]
    private GameObject myReceiver = null;
    [SerializeField]
    private GameObject myPlayer = null;
    [SerializeField]
    private GameObject myRail = null;
    [SerializeField]
    private GameObject myTrain = null;

    private GameObject myCurrent = null;

    private eTileType myType = eTileType.Empty;
    private Coord myCoords;
    //private _Tile myTileData;

    private void Start()
    {
        myCoords = new Coord((int)transform.position.x, (int)transform.position.z);
    }

    public void PlaceRail()
    {
        // Use offset to make sure it's visible on all materials.
        float offset = 0.01f;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        myCurrent = Instantiate(myRail, newPosition, transform.rotation);
        myType = eTileType.Rail;
    }

    public void PlaceTrain()
    {
        // Use offset to make sure it's visible on all materials.
        float rockSize = myTrain.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + rockSize / 2, transform.position.z);

        myCurrent = Instantiate(myTrain, newPosition, transform.rotation);
        myType = eTileType.Train;
    }

    public void PlaceRock()
    {
        float rockSize = myRock.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + rockSize / 2, transform.position.z);

        myCurrent = Instantiate(myRock, newPosition, transform.rotation);
        //myType = eTileType.Rock;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Rock, true);
    }

    public void PlaceImpassable()
    {
        float impassableSize = myImpassable.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + impassableSize / 2, transform.position.z);

        myCurrent = Instantiate(myImpassable, newPosition, transform.rotation);
        //myType = eTileType.Impassable;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Impassable, true);
    }

    public void PlaceSlidingBlock()
    {
        float slidingBlockSize = mySlidingBlock.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.y;
        float tileSize = mySlidingBlock.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 4;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + slidingBlockSize / 2, transform.position.z);

        myCurrent = Instantiate(mySlidingBlock, newPosition, transform.rotation);
        //myType = eTileType.Sliding;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Sliding, true);
    }

    public void PlaceHole()
    {
        // Use offset to make sure it's visible on all materials.
        float offset = 0.01f;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        myCurrent = Instantiate(myHole, newPosition, transform.rotation);
        //myType = eTileType.Hole;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Hole, true);
    }

    public void PlaceFinish()
    {
        // Use offset to make sure it's visible on all materials.
        float offset = 0.01f;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        myCurrent = Instantiate(myFinish, newPosition, transform.rotation);
        //myType = eTileType.Finish;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Finish, true);
    }

    public void PlaceButton()
    {
        float buttonSize = myButton.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + buttonSize / 2, transform.position.z);

        myCurrent = Instantiate(myButton, newPosition, transform.rotation);
        //myType = eTileType.Button;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Button, true);
    }

    public void PlaceDoor()
    {
        float doorSize = myDoor.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop/* + doorSize / 2*/, transform.position.z);

        myCurrent = Instantiate(myDoor, newPosition, transform.rotation);
        //myType = eTileType.Door;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Door, true);
    }

    public void PlacePlayer()
    {
        //float playerSize = myPlayer.transform.Find("body_geo").GetComponent<MeshRenderer>().bounds.size.y;
        float playerSize = 0f;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + playerSize / 2, transform.position.z);

        myCurrent = Instantiate(myPlayer, newPosition, transform.rotation);
        //myType = eTileType.Player;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Player, true);
    }

    public void PlaceLaserEmitter()
    {
        float emitterSizeY = myEmitter.transform.Find("Base").GetComponent<Renderer>().bounds.size.y;

        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + emitterSizeY / 2, transform.position.z);

        myCurrent = Instantiate(myEmitter, newPosition, transform.rotation);
        //myType = eTileType.Emitter;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Emitter, true);
    }

    public void PlaceLaserReflector()
    {
        float reflectorSize = myReflector.transform.Find("Base").GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + reflectorSize / 2, transform.position.z);

        myCurrent = Instantiate(myReflector, newPosition, transform.rotation);
        //myType = eTileType.Reflector;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Reflector, true);
    }

    public void PlaceLaserReceiver()
    {
        //float receiverSize = myReceiver.transform.Find("Base").GetComponent<SkinnedMeshRenderer>().bounds.size.y;
        float receiverSize = 1.0f;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + receiverSize / 2, transform.position.z);

        myCurrent = Instantiate(myReceiver, newPosition, transform.rotation);
        //myType = eTileType.Receiver;
        //myTileData = new _Tile(new Coord((int)(transform.position.x), (int)(transform.position.z)), eTileType.Receiver, true);
    }

    public void RemoveCurrent()
    {
        if (myCurrent != null)
        {
            DestroyImmediate(myCurrent);
        }
    }

    public Coord GetCoords()
    {
        return myCoords;
    }

    public eTileType GetTileType()
    {
        return myType;
    }

    //public _Tile GetTile()
    //{
    //    return myTileData;
    //}
}
