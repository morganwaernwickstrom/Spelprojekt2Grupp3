using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager myInstance { get; private set; }

    [SerializeField] List<int> myLevelsUnlocked;

    private void Awake()
    {
        if(myInstance == null) 
        {
            myInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public bool IsLevelUnlocked(int index) 
    {
        if (myLevelsUnlocked[index] == 0)
            return false;
        else
            return true;
    }

    public void UnlockLevel(int index) 
    {
        myLevelsUnlocked[index] = 1;
        PlayerPrefs.SetInt("myLevelsUnlocked", myLevelsUnlocked.Count);

        for(int i = 0; i < myLevelsUnlocked.Count; i++) 
        {
            PlayerPrefs.SetInt("myLevelsUnlocked" + i, myLevelsUnlocked[i]);
        }
    }
}
