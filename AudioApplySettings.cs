using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioApplySettings : MonoBehaviour
{
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";
    private float backgroundFloat, soundEffectsFloat;
    public AudioSource backgroundAudio;
    public AudioSource soundEffectAudio;
    public Slider backgroundSlider, sfxSlider;

    void Awake()
    {
        ContinueSettings();
    }
    // Start is called before the first frame update
    void Start()
    {
        ContinueSettings();
    }

    // Update is called once per frame
    void ContinueSettings()
    {
        backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
        soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);

        backgroundAudio.volume = backgroundFloat;
        soundEffectAudio.volume = soundEffectsFloat;
        backgroundSlider.value = backgroundFloat;
        sfxSlider.value = soundEffectsFloat;
    }
}
