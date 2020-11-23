using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public void ActivateLevelClear()
    {
        gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            //Load Credits
        }
    }
    public void LevelSelect()
    {
        //Load the scene the Level Select is on
    }
}
