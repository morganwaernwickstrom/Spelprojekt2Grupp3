using UnityEngine;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour
{
    public void GoBackToMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
