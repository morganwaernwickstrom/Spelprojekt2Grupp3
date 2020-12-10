using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    public Slider myMusicSlider;
    public Slider mySFXSlider;
    void Start()
    {
        myMusicSlider.value = SoundManager.myInstance.GetCurrentMusicVolume();
        mySFXSlider.value = SoundManager.myInstance.GetCurrentEffectsVolume();
    }


    void Update()
    {
        SoundManager.myInstance.SetMusicVolume(myMusicSlider.value);
        SoundManager.myInstance.SetEffectsVolume(mySFXSlider.value);
    }
}
