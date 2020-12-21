using UnityEngine;

public class Fairy : MonoBehaviour
{
    [SerializeField]
    private float mySpeed = 35f;
    [SerializeField]
    private float myRadius = 2f;
    [SerializeField]
    private bool myClockwise = true;

    private GameObject myFairy = null;
    
    void Start()
    {
        myFairy = GameObject.Find("Fairy");
        myFairy.transform.position = new Vector3(myFairy.transform.position.x + myRadius, myFairy.transform.position.y, myFairy.transform.position.z);

        if (!myClockwise)
        {
            Debug.Log("HEJ!");
            Vector3 rotation = myFairy.transform.eulerAngles;
            rotation.y = 180;
            myFairy.transform.eulerAngles = rotation;
        }
    }


    void Update()
    {
        if (myClockwise) transform.RotateAround(transform.position, Vector3.up, mySpeed * Time.deltaTime);
        else transform.RotateAround(transform.position, Vector3.up, -mySpeed * Time.deltaTime);
    }
}
