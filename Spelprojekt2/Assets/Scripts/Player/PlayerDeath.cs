using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    bool myShouldDie = false;

    private void Start()
    {
        if (EventHandler.current != null) EventHandler.current.Subscribe(eEventType.PlayerDeath, OnPlayerDeath);
    }

    private void Update()
    {
        if (myShouldDie) StartCoroutine(RestartAfterDeath());
    }

    private IEnumerator RestartAfterDeath()
    {
        GetComponentInChildren<Animator>().SetBool("Die", true);
        GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnPlayerDeath()
    {
        myShouldDie = true;
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerDeath, OnPlayerDeath);
    }
}
