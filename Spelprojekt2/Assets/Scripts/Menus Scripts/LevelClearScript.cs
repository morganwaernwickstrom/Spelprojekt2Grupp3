using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelClearScript : MonoBehaviour
{
    [SerializeField] GameObject myButtons = null;
    [SerializeField] GameObject myText = null;

    private void Start()
    {
        myButtons.SetActive(false);
        myText.SetActive(false);
        EventHandler.current.Subscribe(eEventType.GoalReached, OnGoalReached);
    }

    IEnumerator ShowButtons()
    {
        yield return new WaitForSeconds(1.5f);
        myButtons.SetActive(true);
    }

    private bool OnGoalReached(Coord aGoalCoord)
    {
        if (!myButtons.activeSelf)
        {
            myText.SetActive(true);
            StartCoroutine(ShowButtons());

            return true;
        }
        return false;
    }
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
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

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
