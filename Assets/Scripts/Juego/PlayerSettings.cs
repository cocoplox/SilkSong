using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSettings : MonoBehaviour
{
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] TMP_Dropdown resolutionDropDown;

    //DEFAULT SETTINGS
    [SerializeField] float defaultSFXVolume = 1f;
    [SerializeField] float defaultMusicVolume = 1f;
    void Start()
    {
        LoadSettings();
    }
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            sfxSlider.value = defaultSFXVolume;
            PlayerPrefs.SetFloat("SFXVolume", defaultSFXVolume);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            PlayerPrefs.SetFloat("MusicVolume", defaultMusicVolume);
        }
        else
        {
            musicSlider.value = defaultMusicVolume;
        }
        
        
        resolutionDropDown.value = PlayerPrefs.GetInt("Resolution");
    }
    public void SetSFXVolumePref()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        
    }
    public void SetMusicVolumePref()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
    public void SetResPref()
    {
        PlayerPrefs.SetInt("Resolution", resolutionDropDown.value);
    }

    public void ResetDefaultSettings()
    {
        PlayerPrefs.DeleteAll();
        LoadSettings();
    }

}
