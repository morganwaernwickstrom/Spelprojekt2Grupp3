using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        EventHandler.current.Subscribe(eEventType.GoalReached, OnGoalReached);
    }
    private bool OnGoalReached(Coord aGoalCoord)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            return true;
        }
        return false;
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

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.GoalReached, OnGoalReached);
    }
}
