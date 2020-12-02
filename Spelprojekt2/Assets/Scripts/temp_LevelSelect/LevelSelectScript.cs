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
            //Hoping this is the credits scene!
            SceneManager.LoadScene(sceneBuildIndex: SceneManager.sceneCountInBuildSettings - 1);
        }
    }
    public void LevelSelect()
    {
        //Hoping this is the credits scene!
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.GoalReached, OnGoalReached);
    }
}
