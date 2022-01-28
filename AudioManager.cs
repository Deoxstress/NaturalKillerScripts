using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private float backgroundFloat, soundEffectsFloat;
    public Slider backgroundSlider, soundEffectsSlider;
    [SerializeField] private int firstPlayInt;
    public AudioSource backgroundAudio;
    public AudioSource soundEffectAudio;
    public AudioClip clipTestSlider;
    public bool inMenu;
    private bool noSfxLaunchGame;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        noSfxLaunchGame = true;

        if (firstPlayInt == 0)
        {
            backgroundFloat = 0.25f;
            soundEffectsFloat = 0.25f;
            backgroundSlider.value = backgroundFloat;
            soundEffectsSlider.value = soundEffectsFloat;
            PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat);
            PlayerPrefs.SetInt(FirstPlay, 1);
        }
        else
        {
            if (inMenu)
            {
                backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
                soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
                backgroundSlider.value = backgroundFloat;
                soundEffectsSlider.value = soundEffectsFloat;
                Debug.Log("Sound effect value" + soundEffectsFloat + " " + soundEffectsSlider.value);
            }

        }

    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsSlider.value);
        Debug.Log(PlayerPrefs.GetFloat(BackgroundPref) + " " + PlayerPrefs.GetFloat(SoundEffectsPref));
    }

    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            //SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        float tS = Time.timeScale;
        Time.timeScale = 1;
        backgroundAudio.volume = backgroundSlider.value;
        soundEffectAudio.volume = soundEffectsSlider.value;
        SaveSoundSettings();
        Time.timeScale = tS;
    }

    public void PlaySoundSfxUpdate()
    {
        if(!noSfxLaunchGame)
        {
            soundEffectAudio.PlayOneShot(clipTestSlider);           
        }
        else
        {
            noSfxLaunchGame = false;
        }
    }
}
