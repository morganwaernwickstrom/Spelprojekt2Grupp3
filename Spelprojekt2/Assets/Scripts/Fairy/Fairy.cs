using UnityEngine;

public class Fairy : MonoBehaviour
{
    [SerializeField]
    private float mySpeed = 35f;
    [SerializeField]
    [Range(-0.5f, 0.5f)]
    private float myRadius = 0f;
    [SerializeField]
    private bool myClockwise = true;

    private GameObject myFairy = null;
    
    void Start()
    {
        myFairy = transform.Find("Fairy").gameObject;
        myFairy.transform.position = new Vector3(myFairy.transform.position.x + myRadius, myFairy.transform.position.y, myFairy.transform.position.z);

        if (!myClockwise)
        {
            Vector3 rotation = myFairy.transform.eulerAngles;
            rotation.y = 180f;
            myFairy.transform.eulerAngles = rotation;
        }
    }


    void Update()
    {
        if (myClockwise) transform.RotateAround(transform.position, Vector3.up, mySpeed * Time.deltaTime);
        else transform.RotateAround(transform.position, Vector3.up, -mySpeed * Time.deltaTime);
    }
}
