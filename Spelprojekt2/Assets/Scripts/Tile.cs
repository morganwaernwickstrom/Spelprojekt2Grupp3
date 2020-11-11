using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject myRock = null;
    [SerializeField]
    private GameObject myHole = null;
    [SerializeField]
    private GameObject myPlayer = null;

    private GameObject myCurrent = null;

    private static bool ourHasPlayer = false;

    public void PlaceRock()
    {
        float rockSize = myRock.GetComponent<Renderer>().bounds.size.y;
        float tileSize = GetComponent<Renderer>().bounds.size.y;

        float tileTop = transform.position.y + tileSize / 2;
        Vector3 newPosition = new Vector3(transform.position.x, tileTop + rockSize / 2, transform.position.z);

        myCurrent = Instantiate(myRock, newPosition, transform.rotation);
    }

    public void PlaceHole()
    {
        // Use offset to make sure it's visible on all materials.
        float offset = 0.01f;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        myCurrent = Instantiate(myHole, newPosition, transform.rotation);
    }

    public void PlacePlayer()
    {
        if (!ourHasPlayer)
        {
            ourHasPlayer = true;
            float playerSize = myPlayer.GetComponent<Renderer>().bounds.size.y;
            float tileSize = GetComponent<Renderer>().bounds.size.y;

            float tileTop = transform.position.y + tileSize / 2;
            Vector3 newPosition = new Vector3(transform.position.x, tileTop + playerSize / 2, transform.position.z);

            myCurrent = Instantiate(myPlayer, newPosition, transform.rotation);
        }
    }

    public void RemoveCurrent()
    {
        if (myCurrent != null)
        {
            DestroyImmediate(myCurrent);
        }
    }
}
