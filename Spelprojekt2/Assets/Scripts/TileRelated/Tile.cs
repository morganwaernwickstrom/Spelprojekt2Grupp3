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

    private GameObject myCurrent = null;
    private eTileType myType = eTileType.Empty;

    public void PlaceRock()
    {
        float rockSize = myRock.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + rockSize / 2, transform.position.z);

        myCurrent = Instantiate(myRock, newPosition, transform.rotation);
        myType = eTileType.Rock;
    }

    public void PlaceImpassable()
    {
        float impassableSize = myImpassable.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + impassableSize / 2, transform.position.z);

        myCurrent = Instantiate(myImpassable, newPosition, transform.rotation);
        myType = eTileType.Impassable;
    }

    public void PlaceSlidingBlock()
    {
        float slidingBlockSize = mySlidingBlock.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + slidingBlockSize / 2, transform.position.z);

        myCurrent = Instantiate(mySlidingBlock, newPosition, transform.rotation);
        myType = eTileType.Sliding;
    }

    public void PlaceHole()
    {
        // Use offset to make sure it's visible on all materials.
        float offset = 0.01f;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        myCurrent = Instantiate(myHole, newPosition, transform.rotation);
        myType = eTileType.Hole;
    }

    public void PlaceFinish()
    {
        // Use offset to make sure it's visible on all materials.
        float offset = 0.01f;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        myCurrent = Instantiate(myFinish, newPosition, transform.rotation);
        myType = eTileType.Finish;
    }

    public void PlaceButton()
    {
        float buttonSize = myButton.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + buttonSize / 2, transform.position.z);

        myCurrent = Instantiate(myButton, newPosition, transform.rotation);
        myType = eTileType.Button;
    }

    public void PlaceDoor()
    {
        float doorSize = myDoor.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + doorSize / 2, transform.position.z);

        myCurrent = Instantiate(myDoor, newPosition, transform.rotation);
        myType = eTileType.Door;
    }

    public void PlacePlayer()
    {
        float playerSize = myPlayer.transform.Find("Blockout").GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + playerSize / 2, transform.position.z);

        myCurrent = Instantiate(myPlayer, newPosition, transform.rotation);
        myType = eTileType.Player;
    }

    public void PlaceLaserEmitter()
    {
        float emitterSizeY = myEmitter.transform.Find("Base").GetComponent<Renderer>().bounds.size.y;

        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + emitterSizeY / 2, transform.position.z);

        myCurrent = Instantiate(myEmitter, newPosition, transform.rotation);
        myType = eTileType.Emitter;
    }

    public void PlaceLaserReflector()
    {
        float reflectorSize = myReflector.transform.Find("Base").GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + reflectorSize / 2, transform.position.z);

        myCurrent = Instantiate(myReflector, newPosition, transform.rotation);
        myType = eTileType.Reflector;
    }

    public void PlaceLaserReceiver()
    {
        float receiverSize = myReceiver.transform.Find("Base").GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + receiverSize / 2, transform.position.z);

        myCurrent = Instantiate(myReceiver, newPosition, transform.rotation);
        myType = eTileType.Receiver;
    }

    public void RemoveCurrent()
    {
        if (myCurrent != null)
        {
            DestroyImmediate(myCurrent);
        }
    }

    public eTileType GetTileType()
    {
        return myType;
    }
}
