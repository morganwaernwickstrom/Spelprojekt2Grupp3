using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Vector3 myDesiredPosition;
    private Vector3 myMiddlePosition;
    private Vector3 myPosition;
    private Vector3 myDesiredRotation;
    private Vector3 myRotation;
    private bool myFirstIsFinished = false;
    private bool mySecondIsFinished = false;
    private float mySpeed = 0.4f;
    private float myRotationSpeed = 0.4f;

    private void Start()
    {
        myPosition = transform.position;
        myMiddlePosition = new Vector3(myPosition.x, myPosition.y + 1.5f, myPosition.z - 0.5f);
        myRotation = transform.eulerAngles;
        myDesiredPosition = new Vector3(3f, 13f, -4f);
        myDesiredRotation = new Vector3(60f, 0f, 0f);
    }
    private void Update()
    {
        //FirstZoom();
        SecondZoom();
    }

    private void FirstZoom()
    {
        myPosition.x = Mathf.Lerp(myPosition.x, myMiddlePosition.x, mySpeed * Time.deltaTime);
        myPosition.y = Mathf.Lerp(myPosition.y, myMiddlePosition.y, mySpeed * Time.deltaTime);
        myPosition.z = Mathf.Lerp(myPosition.z, myMiddlePosition.z, mySpeed * Time.deltaTime);
        transform.position = myPosition;
    }

    private void SecondZoom()
    {
        myPosition.x = Mathf.Lerp(myPosition.x, myDesiredPosition.x, mySpeed * Time.deltaTime);
        myPosition.y = Mathf.Lerp(myPosition.y, myDesiredPosition.y, mySpeed * Time.deltaTime);
        myPosition.z = Mathf.Lerp(myPosition.z, myDesiredPosition.z, mySpeed * Time.deltaTime);
        myRotation.x = Mathf.Lerp(myRotation.x, myDesiredRotation.x, myRotationSpeed * Time.deltaTime);
        myRotation.y = Mathf.Lerp(myRotation.y, myDesiredRotation.y, myRotationSpeed * Time.deltaTime);
        myRotation.z = Mathf.Lerp(myRotation.z, myDesiredRotation.z, myRotationSpeed * Time.deltaTime);
        transform.position = myPosition;
        transform.eulerAngles = myRotation;
    }
}
