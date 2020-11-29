using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    bool myCoroutineRunning = false;

    private void Start()
    {
        if (EventHandler.current != null) EventHandler.current.Subscribe(eEventType.PlayerDeath, OnPlayerDeath);
    }

    private IEnumerator OnPlayerDeath()
    {
        Debug.Log("I should die..");
        GetComponentInChildren<Animator>().SetBool("Die", true);
        GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        EventHandler.current.UnSubscribe(eEventType.PlayerDeath, OnPlayerDeath);
    }
}
