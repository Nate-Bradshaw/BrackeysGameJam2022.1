using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMix;
    [SerializeField] private Slider slider;

    private void Start()
    {
        float value;
        bool result = audioMix.GetFloat("masterVolume", out value);
        if (result)
            slider.value = value;
        else
            Debug.Log("oops");
    }

    public void SetVolume(float volume)
    {
        audioMix.SetFloat("masterVolume", volume);
    }
}
