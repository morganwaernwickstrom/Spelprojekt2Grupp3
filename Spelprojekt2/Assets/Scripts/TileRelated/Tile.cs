using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject myRock = null;
    [SerializeField]
    private GameObject myHole = null;
    [SerializeField]
    private GameObject myFinish = null;
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

    public void PlacePlayer()
    {
        float playerSize = myPlayer.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + playerSize / 2, transform.position.z);

        myCurrent = Instantiate(myPlayer, newPosition, transform.rotation);
        myType = eTileType.Player;
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
